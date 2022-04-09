namespace TextyDungeon.Objects.Equipment;


/// <summary>
/// Базовый класс для снаряжения
/// </summary>
internal abstract class IEquipment : IGameObject
{
  /// <summary>
  /// Инициализация объекта снаряжения
  /// </summary>
  /// <param name="Name">Название снаряжения</param>
  /// <param name="Description">Описание снаряжения</param>
  public IEquipment(string Name, string Description) : base(Name, Description) { }
}
