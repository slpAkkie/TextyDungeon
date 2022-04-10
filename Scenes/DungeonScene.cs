namespace TextyDungeon.Scenes;

using TextyDungeon.Creatures;
using TextyDungeon.Creatures.Enemies;
using TextyDungeon.Utils;


/// <summary>
/// Сцена подземелья
/// </summary>
internal class DungeonScene : IScene
{
  private const int BASE_WIN_COST = 10;

  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name => "Подземелье";

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  public override bool ContinueCondition => this.AvailableEnemies.Count > 0;

  /// <summary>
  /// Приостановить сцену (Не менять врагов при запуске)
  /// </summary>
  private bool PreserveEnemyRefill = false;

  /// <summary>
  /// Список для создания противников
  /// </summary>
  private readonly List<Func<IEnemy>> EnemiesCreatorList = new()
  {
    () => new Skeleton(),
    () => new Zombie(),
    () => new Dragon(),
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
    base.Start();

    if (this.PreserveEnemyRefill && this.AvailableEnemies.Count > 0) return;
    this.PreserveEnemyRefill = false;

    this.AvailableEnemies.Clear();
    for (int i = 0; i < 3; i++)
      this.AvailableEnemies.Add(this.EnemiesCreatorList[new Random().Next(0, this.EnemiesCreatorList.Count)]());

    this.WinCost = BASE_WIN_COST;
    this.AvailableEnemies.ForEach(Enemy => this.WinCost += Enemy.WinCost);
  }


  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    Console.Clear();
    int? UserIntInput = Utils.ConvertToInt(UserInput, "Номер врага должен быть числом");

    if (UserIntInput == null) return;
    UserIntInput--;

    if (UserIntInput < 0 || UserIntInput >= this.AvailableEnemies.Count) {
      UserInteraction.WriteDungerousLine($"Вы не можете выбрать {UserIntInput + 1} врага");
      return;
    }

    BattleScene Battle = new(this.GameInstance, this, this.AvailableEnemies[(int)UserIntInput]);
    this.GameInstance.SelectScene(Battle);
    this.PreserveEnemyRefill = true;
    this.CloseScene = true;
  }

  public void RemoveEnemy(IEnemy Enemy)
  {
    this.AvailableEnemies.Remove(Enemy);

    if (this.AvailableEnemies.Count == 0) {
      int? WinCost = null;

      if (this.WinCost != null) {
        WinCost = this.WinCost;
        this.GameInstance.ArmyLeader.ChangeCoins((int)WinCost);
      }

      this.GameInstance.SelectScene(this, delegate() {
        UserInteraction.WriteSuccessLine("Подземелье зачищено");
        Console.Write("Награда за прохождение подземелья: ");
        UserInteraction.WriteSuccess($"{WinCost ?? 0}");
        Console.WriteLine(" монет.");
        this.GameInstance.ArmyLeader.PrintCoins();

        Console.WriteLine("Идем в следующее подземелье");
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
    for (int i = 0; i < this.AvailableEnemies.Count; i++) {
      Console.Write($"{i + 1}. ");
      UserInteraction.WriteInfo(this.AvailableEnemies[i].Name);
      Console.Write(" (HP: ");
      UserInteraction.WriteDungerous(this.AvailableEnemies[i].HP.ToString());
      Console.Write(", Урон: ");
      UserInteraction.WriteDungerous($"{this.AvailableEnemies[i].DamageRange.MinValue}-{this.AvailableEnemies[i].DamageRange.MaxValue}");
      Console.WriteLine(")");
    }
  }


  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    Console.WriteLine("Вы пришли в подземелье и видите перед собой несколько врагов:");
    this.PrintAvailableEnemies();
  }

  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt() => Console.WriteLine("Выберите с кем вы хотите сражаться");
}
