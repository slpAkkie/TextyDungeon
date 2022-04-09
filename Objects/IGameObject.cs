namespace TextyDungeon.Objects;


/// <summary>
/// Базовый класс для игровых объектов
/// </summary>
internal abstract class IGameObject
{
  /// <summary>
  /// Название объекта
  /// </summary>
  public string Name;

  /// <summary>
  /// Описание объекта
  /// </summary>
  public string Description;


  /// <summary>
  /// Инициализация объекта
  /// </summary>
  /// <param name="Name">Название объекта</param>
  /// <param name="Description">Описание объекта</param>
  public IGameObject(string Name, string Description)
  {
    this.Name = Name;
    this.Description = Description;
  }
}
