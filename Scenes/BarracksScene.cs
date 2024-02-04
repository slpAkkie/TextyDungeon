namespace TextyDungeon.Scenes;

using TextyDungeon.Creatures.Warriors;
using TextyDungeon.Objects.Equipment.Armor;
using TextyDungeon.Objects.Equipment.Weapon;
using TextyDungeon.Utils;


/// <summary>
/// Сцена казармы
/// </summary>
internal class BarracksScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name { get => "Казармы"; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  protected override bool ContinueCondition => true;

  /// <summary>
  /// Список функций создающий воинов
  /// </summary>
  private List<Func<IWarrior>> WarriorsCreator = new() {
    () => new CommonWarrior(StringGenerator.GetRandomHumanName(), new CommonSword(), new LightweightBreastplate(), 80),
    () => new CommonWarrior(StringGenerator.GetRandomHumanName(), new CommonSword(), new CommonBreastplate(), 100),
    () => new CommonWarrior(StringGenerator.GetRandomHumanName(), new CommonSword(), new HeavyweightBreastplate(), 125),
  };

  /// <summary>
  /// Доступные войны для найма
  /// </summary>
  private WarriorsList AvailableWarriors = new();


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public BarracksScene(Game GameInstance) : base(GameInstance) => this.RefillWarriors();


  /// <summary>
  /// Заполнить список войнов, доступных для найма
  /// </summary>
  private void RefillWarriors()
  {
    this.AvailableWarriors.Clear();

    for (int _ = 0; _ < 3; _++)
      this.AvailableWarriors.Add(this.WarriorsCreator[new Random().Next(0, this.WarriorsCreator.Count)]());
  }

  /// <summary>
  /// Запуск сцены
  /// </summary>
  public override void Start()
  {
    Console.Write($"Приветствую вас, Генерал ");
    UserInteraction.WriteBlueLine(this.GameInstance.ArmyLeader.Name);
    UserInteraction.NewLine();

    if (this.AvailableWarriors.Empty)
      this.RefillWarriors();
  }

  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    Console.Clear();
    int? UserIntInput = Utils.ConvertToInt(UserInput, "Номер воина должен быть числом");

    if (UserIntInput == null)
      return;
    UserIntInput--;

    if (UserIntInput < 0 || UserIntInput >= this.AvailableWarriors.Count)
    {
      UserInteraction.WriteErrorTop("Боюсь у меня нет такого бойца");
      return;
    }

    IWarrior WarriorToBuy = this.AvailableWarriors[(int)UserIntInput];

    if (WarriorToBuy.HireCost > this.GameInstance.ArmyLeader.Coins)
    {
      UserInteraction.WriteErrorTop("Сэр, вам не хватает золотых монет для найма этого бойца, возвращайтесь позже, когда собирете больше");
      return;
    }

    this.GameInstance.ArmyLeader.ChangeCoins(-WarriorToBuy.HireCost);
    this.GameInstance.Army.Add(WarriorToBuy);
    this.AvailableWarriors.Remove(WarriorToBuy);
  }

  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    if (this.AvailableWarriors.Count == 0)
    {
      Console.WriteLine("Сейчас у меня нет воинов, которые подошли бы твоему гарнизону");
      return;
    }

    Console.WriteLine("Сейчас у меня есть эти бойцы:");
    this.AvailableWarriors.PrintCostList();

    UserInteraction.NewLine();
    this.GameInstance.Army.PrintList(WithNumber: false);
  }

  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt()
  {
    this.GameInstance.ArmyLeader.PrintCoins();
    UserInteraction.NewLine();
    UserInteraction.WriteBlueLine("Какого бойца хотите нанять?");
  }
}
