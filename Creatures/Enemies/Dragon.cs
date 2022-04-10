namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


/// <summary>
/// Класс противника. Дракон
/// </summary>
internal class Dragon : IEnemy
{
  /// <summary>
  /// Инициализация противника
  /// </summary>
  public Dragon() : base("Дракон", "", new SRange(55, 85)) { }
}
