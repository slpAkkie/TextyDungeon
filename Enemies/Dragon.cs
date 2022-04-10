namespace TextyDungeon.Enemies;


/// <summary>
/// Класс противника. Дракон
/// </summary>
internal class Dragon : IEnemy
{
  /// <summary>
  /// Инициализация противника
  /// </summary>
  public Dragon() : base("Дракон", 55, 85) { }
}
