namespace TextyDungeon.Extensions;


/// <summary>
/// Класс расшириния строк
/// </summary>
internal static class StringExtension
{
  /// <summary>
  /// Повторить строку несколько раз
  /// </summary>
  /// <param name="Str">Строка для повторения</param>
  /// <param name="amount">Сколько раз повторить</param>
  /// <returns>Измененная строка</returns>
  public static string Repeat(this string Str, int amount)
  {
    for (int i = 1; i < amount; i++)
      Str += Str;

    return Str;
  }
}
