namespace TextyDungeon.Scenes;

using TextyDungeon.Objects.Food;
using TextyDungeon.Creatures.Warriors;
using TextyDungeon.Utils;


/// <summary>
/// Сцена таверны
/// </summary>
internal class TavernScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name { get => "Таверна"; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  protected override bool ContinueCondition => true;

  /// <summary>
  /// Список еды, доступной к покупке
  /// </summary>
  private List<IFood> Food;

  /// <summary>
  /// Выбранный воин для употребления товара
  /// </summary>
  private bool WaitWarriorChoose { get => this.ChosenFoodIndex != null; }

  /// <summary>
  /// Выбранный инедекс еды
  /// </summary>
  private int? ChosenFoodIndex;


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public TavernScene(Game GameInstance) : base(GameInstance)
  {
    this.Food = new List<IFood>()
    {
      new Beer(),
      new Chowder(),
    };
  }


  /// <summary>
  /// Запуск сцены
  /// </summary>
  public override void Start() { }

  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    Console.Clear();

    int? UserIntInput = Utils.ConvertToInt(UserInput, $"Номер {(this.WaitWarriorChoose ? "воина" : "товара")} должен быть числом");
    if (UserIntInput == null)
      return;

    if (this.WaitWarriorChoose)
      this.ChooseWarrior((int)UserIntInput);
    else
      this.ChooseFood((int)UserIntInput);
  }

  /// <summary>
  /// Сценарий при выборе воина
  /// </summary>
  /// <param name="UserIntInput">Введенное число</param>
  private void ChooseWarrior(int UserIntInput)
  {
    int WarriorIndex = (int)UserIntInput - 1;
    if (!this.GameInstance.IsWarrior(WarriorIndex)) {
      Console.Clear();
      UserInteraction.WriteErrorTop("У вас нет такого война, взгляните заново");
      return;
    }
    IWarrior ChosenWarrior = this.GameInstance.Army[WarriorIndex];

    if (ChosenWarrior.IsDead) {
      Console.Clear();
      UserInteraction.WriteErrorTop("Этот воин уже мертв. Никакая выпивка и еда ему не поможет");
      return;
    } else if (ChosenWarrior.HP == 100) {
      Console.Clear();
      Console.WriteLine("Этому воину не надо восстанавливать здоровье.");
      UserInteraction.NewLine();
      UserInteraction.WriteBlueLine($"Вы уверены, что хотите потратить \"{this.Food[(int)this.ChosenFoodIndex].Name}\" на этого воина?");
      if (!UserInteraction.GetYesNo()) return;
    }

    this.GameInstance.ArmyLeader.ChangeCoins(-this.Food[(int)this.ChosenFoodIndex].Cost);
    ChosenWarrior.Hill(this.Food[(int)this.ChosenFoodIndex].HillAmount);

    Console.Clear();
    UserInteraction.WriteBlueLine($"Здоровья выбранного война увеличено на {this.Food[(int)this.ChosenFoodIndex].HillAmount}");
    UserInteraction.NewLine();
    this.ChosenFoodIndex = null;
  }

  /// <summary>
  /// Сценарий при выборе еды
  /// </summary>
  /// <param name="UserIntInput">Введенное число</param>
  public void ChooseFood(int UserIntInput)
  {
    if (UserIntInput < 1 || UserIntInput > this.Food.Count) {
      Console.Clear();
      UserInteraction.WriteErrorTop("У нас нет такого товара, пожалуйста выберите другой");
      return;
    }

    --UserIntInput;

    if (this.Food[(int)UserIntInput].Cost > this.GameInstance.ArmyLeader.Coins) {
      Console.Clear();
      UserInteraction.WriteErrorTop("У вас не хватает монет на этот товар. Пожалуйста, выберите другой");
      return;
    }

    this.ChosenFoodIndex = UserIntInput;
  }

  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    if (this.WaitWarriorChoose) {
      Console.Write("Вы выбрали ");
      this.Food[(int)this.ChosenFoodIndex].Print();
      UserInteraction.NewLine();
      Console.WriteLine("Теперь выберите какому войну отдать этот товар");
      this.GameInstance.Army.PrintList();
    } else {
      Console.WriteLine("Добро пожаловать в таверну");
      Console.WriteLine("Здесь вы можете восстановить здоровье своих воинов");
      UserInteraction.NewLine();

      for (int i = 0; i < Food.Count; i++) {
        Console.Write($"{i + 1}. ");
        this.Food[i].Print();
      }

      UserInteraction.NewLine();
      Console.WriteLine($"У вас в распоряжении {this.GameInstance.ArmyLeader.Coins} золотых монет");
    }
  }

  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt() => Console.WriteLine(this.WaitWarriorChoose ? "Введите номер воина" : "Выберите какой товар хотите купить");

  /// <summary>
  /// Выход со сцены
  /// </summary>
  public override void Closing()
  {
    base.Closing();

    this.ChosenFoodIndex = null;
  }
}
