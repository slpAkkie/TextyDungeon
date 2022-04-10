namespace TextyDungeon.Creatures.Warriors;


internal abstract class IWarrior : ICreature
{
  /// <summary>
  /// Значние урона по умолчанию
  /// </summary>
  private const double DEFAULT_DAMAGE = 25.0;

  /// <summary>
  /// Урон воина
  /// </summary>
  public double Damage { get; private set; }

  /// <summary>
  /// Показатель брони воина
  /// </summary>
  protected double Armor { get; private set; }


  /// <summary>
  /// Инициализация воина с установленными параметрами
  /// </summary>
  public IWarrior(string Name, string Description, double Arrmor, double HP = MAX_HP) : base(Name, Description)
  {
    this.Armor = Arrmor;
    this.HP = HP;
    this.Description = Description;

    this.Damage = DEFAULT_DAMAGE;
  }


  /// <summary>
  /// Получить урон
  /// </summary>
  /// <param name="Damage">Уоличество урона</param>
  /// <returns>Остался ли жив воин</returns>
  public bool TakeDamage(double Damage)
  {
    this.HP -= Damage / this.Armor;

    return this.IsAlive;
  }

  public void Hill(int HillAmount) => this.HP += HillAmount;
}
