namespace TextyDungeon.Objects.Food;


/// <summary>
/// Базовый класс для еды
/// </summary>
internal abstract class IFood : IGameObject
{
  /// <summary>
  /// Количество, на которое исцеляет эта еда
  /// </summary>
  public int HillAmount { get; private set; }

  /// <summary>
  /// Стоимость еды
  /// </summary>
  public readonly int Cost;


  /// <summary>
  /// Инициализация объекта еды
  /// </summary>
  /// <param name="Name">Название еды</param>
  /// <param name="Description">Описание еды</param>
  /// <param name="HillAmount">Количество, на которое исцеляет эта еда</param>
  public IFood(string Name, string Description, int HillAmount, int Cost) : base(Name, Description)
  {
    this.HillAmount = HillAmount;
    this.Cost = Cost;
  }
}
