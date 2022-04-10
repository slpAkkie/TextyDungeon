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
  /// <param name="ClearAfter">Нужно ли очищать консоль после ответа</param>
  /// <returns>true если ответ один из списка CONFIRMATION_ANSWERS</returns>
  public static bool GetYesNo(bool ClearAfter = true)
  {
    PromptWelcome();
    string input = Console.ReadLine()?.Trim(' ') ?? "";
    if (ClearAfter) Console.Clear();

    return CONFIRMATION_ANSWERS.Contains(input.ToLower());
  }

  /// <summary>
  /// Сообщение о конце игры
  /// </summary>
  public static void EndGameMessage(Game GameInstance)
  {
    Console.Clear();
    if (GameInstance.IsArmyDead)
      Console.WriteLine(
         GameInstance.IsNecromancy
         ? "Вы бросили путь войны, а ваши войны теперь будут вечно ходить по землям королевства в поисках битвы"
         : "Вы сохранили свою честь и пали в битве вместе со своими войнами"
       );
    WriteGreenLine("Вы были хорошим предводителем. Ваши подчиненные будут помнить вас вечно");
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
  public static void WriteGreenLine(string Str) => WriteColorLine(Str, ConsoleColor.Green);

  /// <summary>
  /// Вывести строку с оформлением успешного действия
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteGreen(string Str) => WriteColorLine(Str, ConsoleColor.Green, false);

  /// <summary>
  /// Вывести строку с оформлением ошибки
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteRedLine(string Str) => WriteColorLine(Str, ConsoleColor.Red);

  /// <summary>
  /// Вывести строку с оформлением ошибки
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteRed(string Str) => WriteColorLine(Str, ConsoleColor.Red, false);

  /// <summary>
  /// Вывести строку с оформлением информационного сообщения
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteBlueLine(string Str) => WriteColorLine(Str, ConsoleColor.Blue);

  /// <summary>
  /// Вывести строку с оформлением информационного сообщения
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteBlue(string Str) => WriteColorLine(Str, ConsoleColor.Blue, false);

  /// <summary>
  /// Вывести строку с оформлением ошибки и двумя пустыми строками снизу
  /// </summary>
  /// <param name="Str">Строка для вывода</param>
  public static void WriteErrorTop(string Str)
  {
    WriteRedLine(Str);
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
  public static void PromptWelcome() => WriteGreen(">> ");
}
