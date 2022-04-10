namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


internal abstract class IEnemy : ICreature
{
  /// <summary>
  /// Значение урона по умолчанию
  /// </summary>
  private readonly SRange DAMAGE = new SRange(10, 25);


  /// <summary>
  /// Значение урона
  /// </summary>
  public readonly SRange DamageRange;

  /// <summary>
  /// Урон
  /// </summary>
  public virtual int Damage { get => new Random().Next(this.DamageRange.MinValue, this.DamageRange.MaxValue); }

  /// <summary>
  /// Средний урон
  /// </summary>
  private int AvgDamage { get => (this.DamageRange.MinValue + this.DamageRange.MaxValue) / 2; }

  /// <summary>
  /// Сколько монет будет получено за победу
  /// </summary>
  public int WinCost { get => new Random().Next(this.AvgDamage - this.DamageRange.MinValue, this.DamageRange.MaxValue - this.AvgDamage); }


  /// <summary>
  /// Инициализация противника с указанием диапазона урона
  /// </summary>
  public IEnemy(string Name, string Description, SRange? Damage = null) : base(Name, Description)
  {
    this.DamageRange = Damage ?? this.DAMAGE;
  }

  public bool TakeDamage(double Damage)
  {
    this.HP -= Damage;

    return this.IsAlive;
  }
}
