namespace TextyDungeon.Utils;


/// <summary>
/// Класс вспомогательных функций
/// </summary>
internal static class Utils
{
  /// <summary>
  /// Конвертировать строку в число, при ошибке вывести сообщение об ошибке
  /// </summary>
  /// <param name="Str">Строка для конвертации</param>
  /// <param name="ErrorMessage">Сообщение в случае ошибки</param>
  /// <returns>Полученное число или null</returns>
  public static int? ConvertToInt(string Str, string ErrorMessage)
  {
    try {
      return Convert.ToInt32(Str);
    } catch (FormatException) {
      UserInteraction.WriteErrorTop(ErrorMessage);
      return null;
    }
  }
}
