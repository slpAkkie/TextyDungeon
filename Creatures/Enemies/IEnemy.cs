namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


/// <summary>
/// Базовый класс врагов
/// </summary>
internal abstract class IEnemy : ICreature
{
  /// <summary>
  /// Значение урона по умолчанию
  /// </summary>
  private readonly SRange DEFAULT_DAMAGE_RANGE = new SRange(10, 25);

  /// <summary>
  /// Базовая награда за победу
  /// </summary>
  private const int BASE_WIN_COST = 10;

  /// <summary>
  /// Значение урона
  /// </summary>
  public readonly SRange DamageRange;

  /// <summary>
  /// Уровень врага
  /// </summary>
  public int Level { get; protected set; }

  /// <summary>
  /// Урон
  /// </summary>
  public virtual int Damage { get => this.DamageRange.Random; }

  /// <summary>
  /// Средний урон
  /// </summary>
  private int AvgDamage { get => (this.DamageRange.MinValue + this.DamageRange.MaxValue) / 2; }

  /// <summary>
  /// Сколько монет будет получено за победу
  /// </summary>
  public int WinCost { get => this.AvgDamage + BASE_WIN_COST * this.Level; }


  /// <summary>
  /// Инициализация противника с указанием диапазона урона
  /// </summary>
  public IEnemy(string Name, string Description, SRange? DamageRange = null, int Level = 1) : base(Name, Description)
  {
    this.DamageRange = DamageRange ?? this.DEFAULT_DAMAGE_RANGE;
    this.Level = Level;
  }

  /// <summary>
  /// Получить урон
  /// </summary>
  /// <param name="Damage">Урон</param>
  /// <returns>Остлася ли противник жив</returns>
  public bool TakeDamage(double Damage)
  {
    this.HP -= Damage;

    return this.IsAlive;
  }
}
