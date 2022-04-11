namespace TextyDungeon.Extensions;


/// <summary>
/// Класс расширений для List<T>
/// </summary>
internal static class ListExtension
{
  public static bool Empty<T>(this List<T> SomeList) => SomeList.Count == 0;
}
