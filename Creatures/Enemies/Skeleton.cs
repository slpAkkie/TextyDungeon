namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


/// <summary>
/// Скелет
/// </summary>
internal class Skeleton : IEnemy
{
  public Skeleton() : base("Скелет", "", new SRange(10, 25), 75) { }
}
