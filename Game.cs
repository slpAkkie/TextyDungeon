namespace TextyDungeon;

using TextyDungeon.Warriors;
using TextyDungeon.Enemies;
using TextyDungeon.Scenes;


/// <summary>
/// Основной класс игры, полностью контролирующий ее процесс
/// </summary>
internal class Game
{
  /// <summary>
  /// Закончена ли игра
  /// </summary>
  public bool Gameover = false;


  /// <summary>
  /// Включен ли режим некроманта
  /// </summary>
  public bool IsNecromancy = false;


  /// <summary>
  /// Список доступных сцен
  /// </summary>
  public readonly SceneList Scenes;

  /// <summary>
  /// Текущая выбранная сцена
  /// </summary>
  private IScene? CurrentScene;

  /// <summary>
  /// Следующая сцена
  /// </summary>
  private IScene? NextScene;

  /// <summary>
  /// Callback который необходимо выполнить после завершения сцены, но до начала следующей
  /// </summary>
  private Action? BeforeNextScene;


  /// <summary>
  /// Список противников
  /// </summary>
  public readonly List<Enemy> Enemies;

  /// <summary>
  /// Список войнов гарнизона
  /// </summary>
  public readonly List<IWarrior> Army;

  /// <summary>
  /// Количество мертвых войнов в гарнизоне
  /// </summary>
  public int QuantityOfDeadWarriors = 0;

  public bool IsArmyDead { get => this.QuantityOfDeadWarriors == this.Army.Count; }

  /// <summary>
  /// Генерал гарнизона
  /// </summary>
  public Leader ArmyLeader { get; private set; } = new();


  /// <summary>
  /// Первоначальная инициализация игры
  /// </summary>
  public Game()
  {
    // Создание первоначального гарнизона
    this.Army = new()
    {
      new CommonWarrior(),
      new LightweightWarrior(),
      new HeavyweightWarrior(),
    };

    // Создание списка противников
    this.Enemies = new()
    {
      new Skeleton(),
      new Zombie(),
      new Dragon(),
    };

    // Инициализация списка сцен
    this.Scenes = new(this);

    // Запуск стартовой сцены
    this.CurrentScene = this.Scenes.Start;
  }


  public void PrintArmyList()
  {
    for (int i = 0; i < this.Army.Count; i++) {
      Console.Write($"{i + 1}. {this.Army[i].Description} (");
      UserInteraction.WriteDungerous($"{this.Army[i].HP} HP");
      Console.Write(") : ");
      if (this.Army[i].IsAlive || this.IsNecromancy) UserInteraction.WriteSuccessLine("Готов к сражению");
      else UserInteraction.WriteDungerousLine("Умер");
    }
  }


  /// <summary>
  /// Переключить сцену
  /// </summary>
  public void SelectScene(IScene? NextScene, Action? BeforeNextScene = null)
  {
    this.NextScene = NextScene;
    this.BeforeNextScene = BeforeNextScene;
  }


  /// <summary>
  /// Установить имя Генерала
  /// </summary>
  /// <param name="Name">Имя Генерала</param>
  public void SetArmyLeaderName(string? Name)
  {
    // Если было передано имя, установить его
    if (Name != null) this.ArmyLeader.Name = Name;
  }


  public bool CheckUserInput(string UserInput)
  {
    if (UserInput == "") {
      this.SelectScene(this.Scenes.Select);
    } else if (UserInput == "Q") {
      this.SelectScene(null);
      this.Gameover = true;
    } else return false;

    return true;
  }


  /// <summary>
  /// Запустить игру на текущей сцене
  /// </summary>
  public void StartGame()
  {
    if (this.CurrentScene == null) {
      UserInteraction.WriteDungerousLine("Произошла какая-то ошибка. Мы не смогли запустить игру. Пожалуйста обратитесь к разработчику");
      return;
    }

    // Запуск текущей сцены
    while (true) {
      Console.Clear();
      if (this.BeforeNextScene != null) this.BeforeNextScene();
      this.CurrentScene.Start();

      // Жизненный цикл сцены
      do {
        this.CurrentScene.PrintAcions();

        if (!this.CurrentScene.IsMenuDisabled) {
          UserInteraction.NewLine();
          Console.WriteLine(">>> Нажмите Enter, чтобы выбрать локацию");
          Console.WriteLine(">>> Введите Q, чтобы закрыть приложение");
        }

        string UserInput = "";

        if (!this.CurrentScene.IsPromptDisabled) {
          UserInteraction.NewLine(2);
          this.CurrentScene.Prompt();

          UserInteraction.PromptWelcome();
          UserInput = Console.ReadLine() ?? "";

          if (!this.CurrentScene.IsMenuDisabled && this.CheckUserInput(UserInput))
            break;
        }

        this.CurrentScene.Update(UserInput);
      } while (this.CurrentScene.IsContinue);

      if (this.NextScene == null) break;

      this.CurrentScene = this.NextScene;
      this.NextScene = null;
    }

    // По окончанию работы сцен, вывести сообщение об окончании игры
    UserInteraction.EndGameMessage(this);
  }
}
