namespace TextyDungeon.Creatures.Warriors;


/// <summary>
/// Класс гарнизона
/// </summary>
internal class Army
{
  /// <summary>
  /// Объект игры
  /// </summary>
  private Game GameInstance;

  /// <summary>
  /// Список войнов гарнизона
  /// </summary>
  public readonly List<IWarrior> Warriors;

  /// <summary>
  /// Количество мертвых войнов в гарнизоне
  /// </summary>
  public int QuantityOfDead = 0;

  /// <summary>
  /// Пала ли вся армия
  /// </summary>
  public bool IsDead { get => this.QuantityOfDead == this.Warriors.Count; }

  /// <summary>
  /// Количество воинов в гарнизоне
  /// </summary>
  public int Count { get => this.Warriors.Count; }


  /// <summary>
  /// Инициализация гарнизона
  /// </summary>
  public Army(Game GameInstance)
  {
    // Создание первоначального гарнизона
    this.Warriors = new()
    {
      new CommonWarrior(),
      new LightweightWarrior(),
      new HeavyweightWarrior(),
    };

    this.GameInstance = GameInstance;
  }


  /// <summary>
  /// Индексатор к списку воинов
  /// </summary>
  /// <param name="index">Индекс воина</param>
  /// <returns>Выбранного воина</returns>
  public IWarrior this[int index]
  {
    get => this.Warriors[index];
    set => this.Warriors[index] = value;
  }


  /// <summary>
  /// Вывести гарнизон
  /// </summary>
  public void Print()
  {
    for (int i = 0; i < this.Warriors.Count; i++) {
      Console.Write($"{i + 1}. {this.Warriors[i].Name} (");
      UserInteraction.WriteRed($"{this.Warriors[i].HP} HP");
      Console.Write(") : ");
      if (this.Warriors[i].IsAlive || this.GameInstance.IsNecromancy) UserInteraction.WriteGreenLine("Готов к сражению");
      else UserInteraction.WriteRedLine("Умер");
    }
  }
}
