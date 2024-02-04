namespace TextyDungeon.Creatures.Warriors;

using TextyDungeon.Objects.Equipment.Armor;
using TextyDungeon.Objects.Equipment.Weapon;


/// <summary>
/// Обычный воин
/// </summary>
internal class CommonWarrior : IWarrior
{
  public CommonWarrior(string Name, IWeapon Weapon, IArmor BodyArmor, int HireCost) : base(Name, Weapon, BodyArmor, HireCost) { }
}
