namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


/// <summary>
/// Класс противника. Зомби
/// </summary>
internal class Zombie : IEnemy
{
  /// <summary>
  /// Инициализация противника
  /// </summary>
  public Zombie() : base("Зомби", "", new SRange(20, 35)) { }
}
