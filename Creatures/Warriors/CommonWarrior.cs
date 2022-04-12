namespace TextyDungeon.Creatures.Warriors;

using TextyDungeon.Objects.Equipment.Armor;
using TextyDungeon.Objects.Equipment.Weapon;


/// <summary>
/// Обычный воин
/// </summary>
internal class CommonWarrior : IWarrior
{
  public CommonWarrior() : base("Обычный воин", "", new CommonSword(), 80) => this.BodyArmor = new CommonBreastplate();
}
