namespace TextyDungeon;

using TextyDungeon.Extensions;


/// <summary>
/// Статический класс для взаимодействия с пользователем
/// </summary>
internal static class UserInteraction
{
  /// <summary>
  /// Варианта положительного ответа на вопрос с ответами "Да" и "Нет"
  /// </summary>
  private static readonly string[] CONFIRMATION_ANSWERS = { "y", "yes", "д", "да" };

  /// <summary>
  /// Получить ответ на вопрос с вариантами "Да" и "Нет"
  /// </summary>
  /// <returns>true если ответ один из списка CONFIRMATION_ANSWERS</returns>
  public static bool GetYesNo()
  {
    PromptWelcome();
    string input = Console.ReadLine()?.Trim(' ') ?? "";
    Console.Clear();

    return CONFIRMATION_ANSWERS.Contains(input.ToLower());
  }

  /// <summary>
  /// Сообщение о конце игры
  /// </summary>
  public static void EndGameMessage()
  {
    Console.Clear();
    WriteSuccessLine("Вы были хорошим предводителем. Ваши подчиненные будут помнить вас вечно");
    Console.ReadLine();
  }

  /// <summary>
  /// Вывести строку с указанным цветом
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  /// <param name="TextColor">Увет в который нужно покрасить строку</param>
  private static void WriteColorLine(string Str, ConsoleColor TextColor, bool NewStringAtTHeEnd = true)
  {
    Console.ForegroundColor = TextColor;
    Console.Write(Str);
    if (NewStringAtTHeEnd) NewLine();
    Console.ResetColor();
  }

  /// <summary>
  /// Вывести строку с оформлением успешного действия
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteSuccessLine(string Str) => WriteColorLine(Str, ConsoleColor.Green);

  /// <summary>
  /// Вывести строку с оформлением успешного действия
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteSuccess(string Str) => WriteColorLine(Str, ConsoleColor.Green, false);

  /// <summary>
  /// Вывести строку с оформлением ошибки
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteDungerousLine(string Str) => WriteColorLine(Str, ConsoleColor.Red);

  /// <summary>
  /// Вывести строку с оформлением ошибки
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteDungerous(string Str) => WriteColorLine(Str, ConsoleColor.Red, false);

  /// <summary>
  /// Вывести строку с оформлением информационного сообщения
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteInfoLine(string Str) => WriteColorLine(Str, ConsoleColor.Blue);

  /// <summary>
  /// Вывести строку с оформлением информационного сообщения
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteInfo(string Str) => WriteColorLine(Str, ConsoleColor.Blue, false);


  /// <summary>
  /// Вывести строку с оформлением ошибки и двумя пустыми строками снизу
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteErrorTop(string Str)
  {
    WriteDungerousLine(Str);
    NewLine(2);
  }

  /// <summary>
  /// Вывести определенное количество пустых строк
  /// </summary>
  /// <param name="amount">Количество пустых строк, которые нужно вывести</param>
  public static void NewLine(int amount = 1) => Console.Write("\n".Repeat(amount));

  /// <summary>
  /// Вывести приглашение к вводу
  /// </summary>
  public static void PromptWelcome() => WriteSuccess(">> ");
}
