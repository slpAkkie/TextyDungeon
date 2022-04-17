namespace TextyDungeon.Objects.Equipment.Weapon;

using TextyDungeon.Utils;


/// <summary>
/// Варваркий меч
/// </summary>
internal class BarbarianSword : IWeapon
{
  public BarbarianSword() : base("Варварский меч", "Тяжелый меч со сколами. Наносит больше урона за счет рваных ран", new SRange(30, 40), 50) { }
}
