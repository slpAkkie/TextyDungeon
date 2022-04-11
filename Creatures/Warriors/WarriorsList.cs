namespace TextyDungeon.Creatures.Warriors;

using TextyDungeon.Extensions;


/// <summary>
/// Класс гарнизона
/// </summary>
internal class WarriorsList
{
  /// <summary>
  /// Список войнов гарнизона
  /// </summary>
  public readonly List<IWarrior> Warriors;

  /// <summary>
  /// Количество мертвых войнов в гарнизоне
  /// </summary>
  public int QuantityOfDead = 0;

  /// <summary>
  /// Пала ли вся армия
  /// </summary>
  public bool IsDead { get => this.QuantityOfDead == this.Warriors.Count; }

  /// <summary>
  /// Количество воинов в гарнизоне
  /// </summary>
  public int Count { get => this.Warriors.Count; }

  public bool Empty { get => this.Warriors.Empty(); }


  /// <summary>
  /// Инициализация гарнизона
  /// </summary>
  public WarriorsList(List<IWarrior>? Warriors = null)
  {
    // Создание первоначального гарнизона
    this.Warriors = Warriors ?? new();
  }


  /// <summary>
  /// Индексатор к списку воинов
  /// </summary>
  /// <param name="index">Индекс воина</param>
  /// <returns>Выбранного воина</returns>
  public IWarrior this[int index]
  {
    get => this.Warriors[index];
    set => this.Warriors[index] = value;
  }


  /// <summary>
  /// Очистить список
  /// </summary>
  public void Clear() => this.Warriors.Clear();

  /// <summary>
  /// Добавить воина в список
  /// </summary>
  /// <param name="Warrior">Воин для вставки</param>
  public void Add(IWarrior Warrior) => this.Warriors.Add(Warrior);

  /// <summary>
  /// Убрать воина из списока
  /// </summary>
  /// <param name="Warrior">Воин, которого нужно удалить</param>
  public void Remove(IWarrior Warrior) => this.Warriors.Remove(Warrior);

  /// <summary>
  /// Вывести информацию о воине вместе с его показателем жизни
  /// </summary>
  /// <param name="Warrior"></param>
  private void PrintWithLifeStatus(IWarrior Warrior)
  {
    Console.Write($"{Warrior.Name} (");
    UserInteraction.WriteRed($"{Warrior.HP} HP");
    Console.Write(") : ");
    if (Warrior.IsAlive) UserInteraction.WriteGreenLine("Готов к сражению");
    else UserInteraction.WriteRedLine("Умер");
  }

  /// <summary>
  /// Вывести информацию о воине вместе с его показателем жизни
  /// </summary>
  /// <param name="Warrior"></param>
  private void PrintWithCostStatus(IWarrior Warrior)
  {
    Console.Write($"{Warrior.Name} : ");
    UserInteraction.WriteGreen($"Показатель брони: {Warrior.Armor.ToString().Replace(',', '.')}");
    Console.Write(" : ");
    UserInteraction.WriteBlueLine($"{Warrior.HireCost} монет");
  }

  /// <summary>
  /// Вывести гарнизон со статусом здоровья
  /// </summary>
  public void PrintList(bool WithNumber = true)
  {
    for (int i = 0; i < this.Warriors.Count; i++) {
      if (WithNumber) Console.Write($"{i + 1}. ");
      else UserInteraction.WriteBlue("> ");
      this.PrintWithLifeStatus(this.Warriors[i]);
    }
  }

  /// <summary>
  /// Вывести гарнизон с ценами найма
  /// </summary>
  public void PrintCostList()
  {
    for (int i = 0; i < this.Warriors.Count; i++) {
      Console.Write($"{i + 1}. ");
      this.PrintWithCostStatus(this.Warriors[i]);
    }
  }
}
