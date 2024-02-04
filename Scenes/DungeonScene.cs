namespace TextyDungeon.Scenes;

using TextyDungeon.Extensions;
using TextyDungeon.Creatures.Enemies;
using TextyDungeon.Utils;


/// <summary>
/// Сцена подземелья
/// </summary>
internal class DungeonScene : IScene
{
  /// <summary>
  /// Базовая цена за зачистку
  /// </summary>
  private const int BASE_WIN_COST = 10;

  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name => "Подземелье";

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  protected override bool ContinueCondition => !this.IsEnemiesDead && !this.GameInstance.Army.IsDead;

  /// <summary>
  /// Приостановить сцену (Не менять врагов при запуске)
  /// </summary>
  public bool PreserveEnemyRefill = false;

  /// <summary>
  /// Мертвы ли все враги
  /// </summary>
  public bool IsEnemiesDead { get => this.AvailableEnemies.Count == 0; }

  /// <summary>
  /// Список для создания противников
  /// </summary>
  private readonly List<Func<IEnemy>> EnemiesCreatorList = new()
  {
    () => new Skeleton(),
    () => new Zombie(),
    () => new Dragon(),
    () => new Slime(),
  };

  /// <summary>
  /// Доступные враги
  /// </summary>
  private List<IEnemy> AvailableEnemies = new();

  /// <summary>
  /// Награда за прохождение подземелья
  /// </summary>
  private int? WinCost;


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public DungeonScene(Game GameInstance) : base(GameInstance) { }


  /// <summary>
  /// Запуск сцены
  /// </summary>
  public override void Start()
  {
    if (!this.PreserveEnemyRefill)
    {
      this.AvailableEnemies.Clear();
      for (int i = 0; i < 3; i++)
        this.AvailableEnemies.Add(this.EnemiesCreatorList[new Random().Next(0, this.EnemiesCreatorList.Count)]());

      this.WinCost = BASE_WIN_COST;
      this.AvailableEnemies.ForEach(Enemy => this.WinCost += Enemy.WinCost);
    }

    this.PreserveEnemyRefill = false;
  }

  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    Console.Clear();
    int? UserIntInput = Utils.ConvertToInt(UserInput, "Номер врага должен быть числом");

    if (UserIntInput == null)
      return;
    UserIntInput--;

    if (UserIntInput < 0 || UserIntInput >= this.AvailableEnemies.Count)
    {
      UserInteraction.WriteRedLine($"Вы не можете выбрать {UserIntInput + 1} врага");
      return;
    }

    BattleScene Battle = new(this.GameInstance, this, this.AvailableEnemies[(int)UserIntInput]);
    this.GameInstance.SelectScene(Battle);
    this.CloseScene = true;
  }

  /// <summary>
  /// Убрать побежденного врага
  /// </summary>
  /// <param name="Enemy">Поверженный враг</param>
  public void RemoveEnemy(IEnemy Enemy)
  {
    this.AvailableEnemies.Remove(Enemy);

    if (this.AvailableEnemies.Empty())
    {
      this.GameInstance.ArmyLeader.ChangeCoins((int)this.WinCost);

      this.GameInstance.SelectScene(this, delegate ()
      {
        UserInteraction.WriteGreenLine("Подземелье зачищено");
        UserInteraction.NewLine();

        Console.Write("Получено: ");
        UserInteraction.WriteYellowLine($"{Enemy.WinCost}G");

        Console.Write("Награда за прохождение подземелья: ");
        UserInteraction.WriteYellowLine($"{this.WinCost}G");

        this.GameInstance.ArmyLeader.PrintCoins();
        UserInteraction.NewLine();

        Console.WriteLine("Идем в следующее подземелье...");
        UserInteraction.NewLine();
      });

      this.CloseScene = true;
    }
  }

  /// <summary>
  /// Вывести список доступных сейчас врагов
  /// </summary>
  public void PrintAvailableEnemies()
  {
    for (int i = 0; i < this.AvailableEnemies.Count; i++)
    {
      Console.Write($"{i + 1,-5}");
      this.AvailableEnemies[i].Print();
    }
  }

  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    Console.WriteLine("Вы пришли в подземелье и видите перед собой несколько врагов:");
    this.PrintAvailableEnemies();

    UserInteraction.NewLine();
    this.GameInstance.Army.PrintList(WithNumber: false);
  }

  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt() => Console.WriteLine("Выберите с кем вы хотите сражаться");
}
