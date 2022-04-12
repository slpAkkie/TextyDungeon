namespace TextyDungeon.Creatures.Warriors;

using TextyDungeon.Objects.Equipment.Armor;
using TextyDungeon.Objects.Equipment.Weapon;


/// <summary>
/// Воин в легких доспехах
/// </summary>
internal class LightweightWarrior : IWarrior
{
  public LightweightWarrior() : base("Воин в легких доспехах", "", new CommonSword(), 100) => this.BodyArmor = new LightweightBreastplate();
}
