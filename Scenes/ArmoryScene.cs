namespace TextyDungeon.Scenes;

using TextyDungeon.Utils;
using TextyDungeon.Creatures.Warriors;
using TextyDungeon.Objects.Equipment;
using TextyDungeon.Objects.Equipment.Armor;
using TextyDungeon.Objects.Equipment.Weapon;


/// <summary>
/// Сцена оружейной
/// </summary>
internal class ArmoryScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name { get => "Оружейная"; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  protected override bool ContinueCondition => true;

  /// <summary>
  /// Список функций создающих снаряжение
  /// </summary>
  private List<Func<IEquipment>> EquipmentCreator = new() {
    () => new CommonSword(),
    () => new BarbarianSword(),

    () => new CommonBreastplate(),
    () => new LightweightBreastplate(),
    () => new HeavyweightBreastplate(),
  };

  /// <summary>
  /// Список доступного оружия
  /// </summary>
  private List<IEquipment> AvailableEquipment = new();

  /// <summary>
  /// Выбранный воин для использования снаряжения
  /// </summary>
  private bool WaitWarriorChoose { get => this.ChosenEquipmentIndex != null; }

  /// <summary>
  /// Выбранный инедекс снаряжения
  /// </summary>
  private int? ChosenEquipmentIndex;


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public ArmoryScene(Game GameInstance) : base(GameInstance) { }


  /// <summary>
  /// Запуск сцены
  /// </summary>
  public override void Start()
  {
    Console.Write($"Приветствую вас, Генерал ");
    UserInteraction.WriteBlueLine(this.GameInstance.ArmyLeader.Name);
    UserInteraction.NewLine();

    this.AvailableEquipment.Clear();

    for (int _ = 0; _ < 5; _++)
      this.AvailableEquipment.Add(this.EquipmentCreator[new Random().Next(0, this.EquipmentCreator.Count)]());
  }

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
      this.ChooseEquipment((int)UserIntInput);
  }

  /// <summary>
  /// Сценарий при выборе воина
  /// </summary>
  /// <param name="UserIntInput">Введенное число</param>
  private void ChooseWarrior(int UserIntInput)
  {
    int WarriorIndex = (int)UserIntInput - 1;
    if (!this.GameInstance.IsWarrior(WarriorIndex))
    {
      Console.Clear();
      UserInteraction.WriteErrorTop("У вас нет такого война, взгляните заново");
      return;
    }
    IWarrior ChosenWarrior = this.GameInstance.Army[WarriorIndex];

    if (ChosenWarrior.IsDead)
    {
      Console.Clear();
      UserInteraction.WriteErrorTop("Этот воин уже мертв. Снаряжение ему больше ни к чему");
      return;
    }

    IEquipment ChosenEquipment = this.AvailableEquipment[(int)this.ChosenEquipmentIndex];

    this.GameInstance.ArmyLeader.ChangeCoins(-ChosenEquipment.Cost);
    ChosenWarrior.Equip(ChosenEquipment);
    this.AvailableEquipment.Remove(ChosenEquipment);

    Console.Clear();
    UserInteraction.WriteBlueLine($"Выбранный воин взял {ChosenEquipment.Name}");
    UserInteraction.NewLine();
    this.ChosenEquipmentIndex = null;
  }

  /// <summary>
  /// Сценарий при выборе снаряжения
  /// </summary>
  /// <param name="UserIntInput">Введенное число</param>
  private void ChooseEquipment(int UserIntInput)
  {
    if (UserIntInput < 1 || UserIntInput > this.AvailableEquipment.Count)
    {
      Console.Clear();
      UserInteraction.WriteErrorTop("У нас нет такого товара, пожалуйста выберите другой");
      return;
    }

    --UserIntInput;

    if (this.AvailableEquipment[(int)UserIntInput].Cost > this.GameInstance.ArmyLeader.Coins)
    {
      Console.Clear();
      UserInteraction.WriteErrorTop("У вас не хватает монет на этот товар. Пожалуйста, выберите другой");
      return;
    }

    this.ChosenEquipmentIndex = UserIntInput;
  }

  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    if (this.WaitWarriorChoose)
    {
      Console.Write("Вы выбрали ");
      this.AvailableEquipment[(int)this.ChosenEquipmentIndex].Print();
      UserInteraction.NewLine();
      Console.WriteLine("Теперь выберите какому войну отдать этот товар");
      this.GameInstance.Army.PrintList();
    }
    else {
      Console.WriteLine("В моем распоряжении следующее снаряжение:");
      for (int i = 0; i < this.AvailableEquipment.Count; i++)
      {
        Console.Write($"{i + 1}. ");
        this.AvailableEquipment[i].Print();
      }

      UserInteraction.NewLine();
      Console.WriteLine($"У вас в распоряжении {this.GameInstance.ArmyLeader.Coins} золотых монет");
    }
  }

  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt() => Console.WriteLine("Выберите что хотите купить");

  /// <summary>
  /// Выход со сцены
  /// </summary>
  public override void Closing()
  {
    base.Closing();

    this.ChosenEquipmentIndex = null;
  }
}
