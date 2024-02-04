namespace TextyDungeon.Utils;

internal class StringGenerator
{
  private static List<string> Forenames = new() {
    "Джек",
    "Джон",
    "Албек",
    "Квин",
    "Робин",
    "Лорк",
    "Нерк",
    "Плотон",
    "Верон",
  };

  private static List<string> Surnames = new() {
    "Рассл",
    "Керк",
    "Аллабай",
    "Джонсон",
    "Гуддс",
    "Норэль",
    "Клобс",
    "Далакарс",
    "Ульгрим",
  };

  public static string GetRandomHumanName() => Forenames[new Random().Next(0, Forenames.Count())] + " " + Surnames[new Random().Next(0, Surnames.Count())];
}
