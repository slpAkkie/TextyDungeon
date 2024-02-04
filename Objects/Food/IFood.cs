namespace TextyDungeon.Objects.Food;


/// <summary>
/// Базовый класс для еды
/// </summary>
internal abstract class IFood : IBuyableGameObject
{
  /// <summary>
  /// Количество, на которое исцеляет эта еда
  /// </summary>
  public int HillAmount { get; private set; }


  /// <summary>
  /// Инициализация объекта еды
  /// </summary>
  /// <param name="Name">Название еды</param>
  /// <param name="Description">Описание еды</param>
  /// <param name="Cost">Цена еды</param>
  /// <param name="HillAmount">Количество, на которое исцеляет эта еда</param>
  public IFood(string Name, string Description, int HillAmount, int Cost) : base(Name, Description, Cost) => this.HillAmount = HillAmount;

  /// <summary>
  /// Вывести информацию о еде
  /// </summary>
  public void Print()
  {
    UserInteraction.WriteBlue(this.Name.PadRight(32));
    UserInteraction.WriteRed($"{this.HillAmount,6} HP");
    Console.Write(" | ");
    UserInteraction.WriteYellow($"{this.Cost}G");
    Console.WriteLine($" | {this.Description}");
  }
}
