namespace TextyDungeon.Creatures.Warriors;

using TextyDungeon.Objects.Equipment.Armor;
using TextyDungeon.Objects.Equipment.Weapon;


/// <summary>
/// Воин в тяжелых доспехах
/// </summary>
internal class HeavyweightWarrior : IWarrior
{
  public HeavyweightWarrior() : base("Воин в тяжелых доспехах", "", new CommonSword(), 180) => this.BodyArmor = new HeavyweightBreastplate();
}
