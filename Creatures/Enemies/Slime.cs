namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


/// <summary>
/// Слайм
/// </summary>
internal class Slime : IEnemy
{
  public Slime() : base("Слайм", "", new SRange(1, 5), 50.0) { }
}
