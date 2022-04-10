namespace TextyDungeon.Enemies;


/// <summary>
/// Класс противника. Скелет
/// </summary>
internal class Skeleton : IEnemy
{
  /// <summary>
  /// Инициализация противника
  /// </summary>
  public Skeleton() : base("Скелет", 10, 25) { }
}
