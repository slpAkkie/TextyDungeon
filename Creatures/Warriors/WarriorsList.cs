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
    if (Warrior.IsAlive)
      Console.Write($"{Warrior.Name,-32}");
    else
      UserInteraction.WriteRed($"{Warrior.Name,-32}");

    if (Warrior.IsAlive)
    {
      UserInteraction.WriteRed($"{Warrior.HP.ToString().Replace(',', '.'),6} HP");
      Console.Write(" | ");
      Console.Write($"{Warrior.Armor.ToString().Replace(',', '.'),4} DEF");
      Console.Write(" | ");
      UserInteraction.WriteBlue($"{Warrior.Weapon.DamageRange.MinValue,2}-{Warrior.Weapon.DamageRange.MaxValue,-2} ATK");
    }

    UserInteraction.NewLine();
  }

  /// <summary>
  /// Вывести информацию о воине со стоимостью найма
  /// </summary>
  /// <param name="Warrior"></param>
  private void PrintWithHireStatus(IWarrior Warrior)
  {
    Console.Write($"{Warrior.Name,-32}");
    Console.Write($"{Warrior.Armor.ToString().Replace(',', '.'),4} DEF");
    Console.Write(" | ");
    UserInteraction.WriteBlue($"{Warrior.Weapon.DamageRange.MinValue,2}-{Warrior.Weapon.DamageRange.MaxValue,-2} ATK");
    Console.Write(" | ");
    UserInteraction.WriteYellowLine($"{Warrior.HireCost}G");
  }

  /// <summary>
  /// Вывести гарнизон со статусом здоровья
  /// </summary>
  public void PrintList(string Header = "Ваш гарнизон:", bool WithNumber = true)
  {
    Console.WriteLine(Header);

    for (int i = 0; i < this.Warriors.Count; i++)
    {
      if (WithNumber)
        Console.Write($"{i + 1,-5}");
      else
        UserInteraction.WriteBlue("> ".PadRight(5));
      this.PrintWithLifeStatus(this.Warriors[i]);
    }
  }

  /// <summary>
  /// Вывести гарнизон с ценами найма
  /// </summary>
  public void PrintCostList()
  {
    for (int i = 0; i < this.Warriors.Count; i++)
    {
      Console.Write($"{i + 1,-5}");
      this.PrintWithHireStatus(this.Warriors[i]);
    }
  }
}
