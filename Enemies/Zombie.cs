namespace TextyDungeon.Enemies;


/// <summary>
/// Класс противника. Зомби
/// </summary>
internal class Zombie : IEnemy
{
  /// <summary>
  /// Инициализация противника
  /// </summary>
  public Zombie() : base("Зомби", 20, 35) { }
}
