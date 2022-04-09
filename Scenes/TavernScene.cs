namespace TextyDungeon.Scenes;

using TextyDungeon.Objects.Food;
using TextyDungeon.Warriors;
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
  public override bool ContinueCondition => true;

  /// <summary>
  /// Список еды, доступной к покупке
  /// </summary>
  private List<IFood> Food;

  private bool WaitWarriorChoose = false;

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
    };
  }


  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    int? UserIntInput = Utils.ConvertToInt(UserInput, $"Номер {(this.WaitWarriorChoose ? "воина" : "товара")} должен быть числом");

    if (this.WaitWarriorChoose) {
      if (this.ChosenFoodIndex == null || UserIntInput == null) {
        UserInteraction.WriteErrorTop("Произошла ошибка и мы не смогли понять, какой товар и воина вы выбрали");
        this.WaitWarriorChoose = false;

        return;
      }

      IWarrior ChosenWarrior = this.GameInstance.Army[(int)UserIntInput - 1];

      if (ChosenWarrior.IsDead) {
        UserInteraction.NewLine();
        UserInteraction.WriteErrorTop("Этот воин уже мертв. никакая выпивка и еда ему не поможет");
        return;
      } else if (ChosenWarrior.HP == 100) {
        UserInteraction.NewLine();
        Console.WriteLine("Этому воину не надо восстанавливать здоровье.");
        UserInteraction.WriteInfoLine($"Вы уверены, что хотите потратить \"{this.Food[(int)this.ChosenFoodIndex].Name}\" на этого воина?");
        if (!UserInteraction.GetYesNo(false)) return;
      }

      this.GameInstance.ArmyLeader.ChangeCoins(-this.Food[(int)this.ChosenFoodIndex].Cost);
      ChosenWarrior.Hill(this.Food[(int)this.ChosenFoodIndex].HillAmount);

      this.WaitWarriorChoose = false;
      Console.Clear();
      UserInteraction.WriteInfoLine($"Здоровья выбранного война увеличено на {this.Food[(int)this.ChosenFoodIndex].HillAmount}");
    } else {

      if (UserIntInput == null) return;

      if (UserIntInput < 1 || UserIntInput > this.Food.Count) {
        UserInteraction.WriteErrorTop("У нас нет такого товара, пожалуйста выберите другой");
        return;
      }

      --UserIntInput;

      if (this.Food[(int)UserIntInput].Cost > this.GameInstance.ArmyLeader.Coins) {
        Console.Clear();
        UserInteraction.WriteErrorTop("У вас не хватает монет на этот товар. Пожалуйста, выберите другой");
        return;
      }

      this.WaitWarriorChoose = true;
      this.ChosenFoodIndex = UserIntInput;
    }
  }


  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    if (this.WaitWarriorChoose) {
      UserInteraction.NewLine();
      Console.WriteLine("Теперь выберите какому войну отдать этот товар");
      this.GameInstance.PrintArmyList();
    }
    else {
      Console.WriteLine("Добро пожаловать в таверну");
      Console.WriteLine("Здесь вы можете восстановить здоровье своих воинов");
      UserInteraction.NewLine();

      for (int i = 0; i < Food.Count; i++) {
        Console.Write($"{i + 1}. ");
        UserInteraction.WriteInfo(this.Food[i].Name);
        Console.Write(" / Восстанавливает ");
        UserInteraction.WriteSuccess($"{this.Food[i].HillAmount} HP");
        Console.Write(", стоимость ");
        UserInteraction.WriteSuccess($"{this.Food[i].Cost} золотых монет");
        Console.WriteLine($". ({this.Food[i].Description})");
      }

      UserInteraction.NewLine();
      Console.WriteLine($"У вас в распоряжении {this.GameInstance.ArmyLeader.Coins} золотых монет");
    }
  }


  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt() => Console.WriteLine(this.WaitWarriorChoose ? "Введите номер воина" : "Выберите какой товар хотите купить");
}
