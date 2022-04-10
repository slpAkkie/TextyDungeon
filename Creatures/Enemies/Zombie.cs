namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


/// <summary>
/// Зомби
/// </summary>
internal class Zombie : IEnemy
{
  public Zombie() : base("Зомби", "", new SRange(20, 35)) { }
}
