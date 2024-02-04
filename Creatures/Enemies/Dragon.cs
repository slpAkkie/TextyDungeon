namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


/// <summary>
/// Дракон
/// </summary>
internal class Dragon : IEnemy
{
  public Dragon() : base("Дракон", "", new SRange(40, 55)) { }
}
