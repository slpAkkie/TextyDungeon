namespace TextyDungeon.Objects.Equipment.Weapon;

using TextyDungeon.Utils;


/// <summary>
/// Посох
/// </summary>
internal class MagicStick : IWeapon
{
  public MagicStick() : base("Посох", "Магическая палка делает магию, я не знаю как это работает, так что забирай", new SRange(15, 50), 40) { }
}
