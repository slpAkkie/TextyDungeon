namespace TextyDungeon.Enemies;


internal abstract class Enemy
{
  /// <summary>
  /// Значение минимального урона по умолчанию
  /// </summary>
  private const int MIN_DAMAGE = 10;

  /// <summary>
  /// Значение максимального урона по умолчанию
  /// </summary>
  private const int MAX_DAMAGE = 25;


  /// <summary>
  /// Значение минимального урона
  /// </summary>
  private readonly int MinDamage;

  /// <summary>
  /// Значение максимального урона
  /// </summary>
  private readonly int MaxDamage;


  /// <summary>
  /// Название противника
  /// </summary>
  public virtual string Name { get; private set; }

  /// <summary>
  /// Урон
  /// </summary>
  public virtual int Damage { get => new Random().Next(this.MinDamage, this.MaxDamage); }

  /// <summary>
  /// Средний урон
  /// </summary>
  private int AvgDamage { get => (this.MinDamage + this.MaxDamage) / 2; }

  /// <summary>
  /// Сколько монет будет получено за победу
  /// </summary>
  public int WinCost { get => new Random().Next(this.AvgDamage - this.MinDamage, this.MaxDamage - this.AvgDamage); }


  /// <summary>
  /// Инициализация противника со стандартными значениями диапазона урона
  /// </summary>
  public Enemy(string Name) : this(Name, MIN_DAMAGE, MAX_DAMAGE) { }


  /// <summary>
  /// Инициализация противника с указанием диапазона урона
  /// </summary>
  public Enemy(string Name, int MinDamage, int MaxDamage)
  {
    this.Name = Name;
    this.MinDamage = MinDamage;
    this.MaxDamage = MaxDamage;
  }
}
