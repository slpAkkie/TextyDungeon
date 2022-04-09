namespace TextyDungeon.Objects.Equipment.Armor;


/// <summary>
/// Базовый класс для брони
/// </summary>
internal abstract class IArmor : IEquipment
{
  /// <summary>
  /// Показатель защиты брони
  /// </summary>
  public int ProtectionRate { get; private set; }

  
  /// <summary>
  /// Инициализация объекта брони
  /// </summary>
  /// <param name="Name">Название брони</param>
  /// <param name="Description">Описание брони</param>
  /// <param name="ProtectionRate">Показатель зашиты брони</param>
  public IArmor(string Name, string Description, int ProtectionRate) : base(Name, Description)
  {
    this.ProtectionRate = ProtectionRate;
  }
}
