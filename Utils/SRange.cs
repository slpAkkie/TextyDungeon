namespace TextyDungeon.Utils;


/// <summary>
/// Представляет структуру с диапазон от минимального до максимального значения
/// </summary>
internal struct SRange
{
  /// <summary>
  /// Левый предел диапазона
  /// </summary>
  public int MinValue { get; private set; }

  /// <summary>
  /// Правый предел диапазона
  /// </summary>
  public int MaxValue { get; private set; }


  /// <summary>
  /// Получить случайное число из диапазона
  /// </summary>
  public int Random { get => new Random().Next(MinValue, MaxValue); }

  public SRange(int Min, int Max)
  {
    this.MinValue = Min;
    this.MaxValue = Max;
  }
}
