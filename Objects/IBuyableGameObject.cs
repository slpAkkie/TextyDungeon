namespace TextyDungeon.Objects;


/// <summary>
/// Игровые объекты, которые можно купить
/// </summary>
internal class IBuyableGameObject : IGameObject
{
  /// <summary>
  /// Стоимость объекта
  /// </summary>
  public readonly int Cost;


  /// <summary>
  /// Инициализация объекта
  /// </summary>
  /// <param name="Name">Название объекта</param>
  /// <param name="Description">Описание объекта</param>
  /// <param name="Cost">Стоимость объекта</param>
  public IBuyableGameObject(string Name, string Description, int Cost) : base(Name, Description) => this.Cost = Cost;
}
