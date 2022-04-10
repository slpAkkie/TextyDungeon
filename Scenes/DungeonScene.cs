namespace TextyDungeon.Scenes;

using TextyDungeon.Enemies;
using TextyDungeon.Utils;


/// <summary>
/// Сцена подземелья
/// </summary>
internal class DungeonScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name => "Подземелье";

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  public override bool ContinueCondition => true;

  /// <summary>
  /// Список для создания противников
  /// </summary>
  private readonly List<Func<IEnemy>> EnemiesCreatorList = new() {
    () => new Skeleton(),
    () => new Zombie(),
    () => new Dragon(),
  };

  /// <summary>
  /// Доступные враги
  /// </summary>
  private List<IEnemy> AvailableEnemies = new();


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public DungeonScene(Game GameInstance) : base(GameInstance) { }

  public override void Start()
  {
    this.AvailableEnemies.Clear();
    for (int i = 0; i < 3; i++)
      this.AvailableEnemies.Add(this.EnemiesCreatorList[new Random().Next(0, this.EnemiesCreatorList.Count)]());
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

    BattleScene Battle = new(this.GameInstance, this.AvailableEnemies[(int)UserIntInput]);
    this.GameInstance.SelectScene(Battle);
    this.CloseScene = true;
  }


  /// <summary>
  /// Вывести список доступных сейчас врагов
  /// </summary>
  public void PrintAvailableEnemies()
  {
    for (int i = 0; i < this.AvailableEnemies.Count; i++)
      Console.WriteLine($"{i + 1}. {this.AvailableEnemies[i].Name}");
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
