namespace TextyDungeon.Objects.Equipment;


/// <summary>
/// Базовый класс для снаряжения
/// </summary>
internal abstract class IEquipment : IBuyableGameObject
{
  /// <summary>
  /// Инициализация объекта снаряжения
  /// </summary>
  /// <param name="Name">Название снаряжения</param>
  /// <param name="Description">Описание снаряжения</param>
  /// <param name="Cost">Цена снаряжения</param>
  public IEquipment(string Name, string Description, int Cost) : base(Name, Description, Cost) { }


  /// <summary>
  /// Напечатать информацию о снаряжении
  /// </summary>
  public abstract void Print();
}
