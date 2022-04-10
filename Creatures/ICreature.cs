namespace TextyDungeon.Creatures;

using TextyDungeon.Objects;


/// <summary>
/// Базовый класс живых существ
/// </summary>
internal abstract class ICreature : IGameObject
{
  /// <summary>
  /// Максимальное значение злоровья
  /// </summary>
  protected const double MAX_HP = 100.0;

  /// <summary>
  /// Минимальное значение злоровья
  /// </summary>
  protected const double MIN_HP = 0.0;


  /// <summary>
  /// Жив ли воин
  /// </summary>
  public bool IsAlive { get => this.HP > MIN_HP; }

  /// <summary>
  /// Погиб ли воин
  /// </summary>
  public bool IsDead { get => !this.IsAlive; }

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
    protected set => this._HP = value < MIN_HP ? MIN_HP : value > MAX_HP ? MAX_HP : Math.Round(value, 2);
  }


  /// <summary>
  /// Инициализировать существо
  /// </summary>
  /// <param name="Name">Название существа</param>
  /// <param name="Description">Описание</param>
  public ICreature(string Name, string Description) : base(Name, Description) => this.HP = MAX_HP;
}
