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
  /// <param name="Cost">Цена оружия</param>
  public IWeapon(string Name, string Description, SRange DamageRange, int Cost) : base(Name, Description, Cost) => this.DamageRange = DamageRange;
}
