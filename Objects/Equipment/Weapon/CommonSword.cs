namespace TextyDungeon.Objects.Equipment.Weapon;

using TextyDungeon.Utils;


/// <summary>
/// Обычный меч
/// </summary>
internal class CommonSword : IWeapon
{
  public CommonSword() : base("Обычный меч", "Обычный меч из обычной стали. Обычная атака", new SRange(25, 30), 35) { }
}
