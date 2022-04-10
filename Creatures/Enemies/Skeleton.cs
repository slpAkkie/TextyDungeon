namespace TextyDungeon.Creatures.Enemies;

using TextyDungeon.Utils;


/// <summary>
/// Класс противника. Скелет
/// </summary>
internal class Skeleton : IEnemy
{
  /// <summary>
  /// Инициализация противника
  /// </summary>
  public Skeleton() : base("Скелет", "", new SRange(10, 25)) { }
}
