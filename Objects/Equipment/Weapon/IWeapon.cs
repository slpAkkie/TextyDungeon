namespace TextyDungeon.Objects.Equipment.Weapon;

using TextyDungeon.Utils;


/// <summary>
/// Базовый класс для оружия
/// </summary>
internal abstract class IWeapon : IEquipment
{
  /// <summary>
  /// Диапазон возможного урона оружия
  /// </summary>
  public SRange DamageRange;


  /// <summary>
  /// Инициализация объекта оружия
  /// </summary>
  /// <param name="Name">Название оружия</param>
  /// <param name="Description">Описание оружия</param>
  /// <param name="DamageRange">Диапазон возможного урона</param>
  public IWeapon(string Name, string Description, SRange DamageRange) : base(Name, Description) => this.DamageRange = DamageRange;
}
