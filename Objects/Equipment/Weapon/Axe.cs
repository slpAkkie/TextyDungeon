namespace TextyDungeon.Objects.Equipment.Weapon;

using TextyDungeon.Utils;


/// <summary>
/// Топор
/// </summary>
internal class Axe : IWeapon
{
  public Axe() : base("Топор", "Украденный из лагеря гномов топор, они будут очень недовольны, если увидят его в руках твоего война", new SRange(35, 45), 55) { }
}
