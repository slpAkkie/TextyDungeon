namespace TextyDungeon;


/// <summary>
/// Генерал карнизона
/// </summary>
internal class Leader
{
  /// <summary>
  /// Имя Генерала по умолчанию
  /// </summary>
  private const string DEFAULT_NAME = "Джек";


  /// <summary>
  /// Имя Генерала
  /// </summary>
  public string Name;

  /// <summary>
  /// Золотые монеты Генерала
  /// </summary>
  public int Coins { get; private set; }


  /// <summary>
  /// Инициализация Генерала со значением по умолчанию
  /// </summary>
  public Leader() : this(DEFAULT_NAME) { }

  /// <summary>
  /// Инициализация Генерала с установленным именем
  /// </summary>
  public Leader(string Name)
  {
    this.Name = Name;
    this.Coins = 0;
  }


  /// <summary>
  /// Изменение количества монет
  /// </summary>
  /// <param name="Coins">Дельта изменения</param>
  /// <returns>Новое количество монет</returns>
  public int ChangeCoins(int Coins) => this.Coins += Coins;

  /// <summary>
  /// Вывести количество монет
  /// </summary>
  public void PrintCoins()
  {
    Console.Write("У вас в распоряжении ");
    UserInteraction.WriteSuccess($"{this.Coins} монет");
    Console.WriteLine(".");
  }
}
