namespace TextyDungeon.Warriors;


internal abstract class IWarrior
{
  /// <summary>
  /// Значние брони по умолчанию
  /// </summary>
  private const double DEFAULT_ARRMOR = 1.0;

  /// <summary>
  /// Значние здоровья по умолчанию
  /// </summary>
  private const double MAX_HP = 100.0;

  /// <summary>
  /// Описание воина по умолчанию
  /// </summary>
  private const string DEFAULT_WARRIOR_DESCRIPTION = "Безымянный воин";


  /// <summary>
  /// Описание воина
  /// </summary>
  public string Description;

  /// <summary>
  /// (Внутренний) Показатель брони воина
  /// </summary>
  private double _HP;

  /// <summary>
  /// Показатель здоровья воина
  /// </summary>
  public double HP
  {
    get => this._HP;
    protected set => this._HP = value < 0 ? 0 : value > 100 ? 100 : value;
  }

  /// <summary>
  /// Показатель брони воина
  /// </summary>
  protected double Armor { get; private set; }


  /// <summary>
  /// Жив ли воин
  /// </summary>
  public bool IsAlive { get => this.HP > 0.0; }

  /// <summary>
  /// Погиб ли воин
  /// </summary>
  public bool IsDead { get => !this.IsAlive; }


  /// <summary>
  /// Инициализация воина со значениями по умолчанию
  /// </summary>
  public IWarrior() : this(DEFAULT_WARRIOR_DESCRIPTION, DEFAULT_ARRMOR, MAX_HP) { }


  /// <summary>
  /// Инициализация воина с установленными параметрами
  /// </summary>
  public IWarrior(string Description, double Arrmor, double HP = 100.0)
  {
    this.Armor = Arrmor;
    this.HP = HP;
    this.Description = Description;
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
