﻿namespace TextyDungeon;

using TextyDungeon.Creatures.Warriors;
using TextyDungeon.Objects.Equipment.Armor;
using TextyDungeon.Objects.Equipment.Weapon;
using TextyDungeon.Scenes;
using TextyDungeon.Utils;


/// <summary>
/// Основной класс игры, полностью контролирующий ее процесс
/// </summary>
internal class Game
{
  /// <summary>
  /// Список доступных сцен
  /// </summary>
  public readonly SceneList Scenes;

  /// <summary>
  /// Текущая выбранная сцена
  /// </summary>
  public IScene? CurrentScene { get; private set; }

  /// <summary>
  /// Следующая сцена
  /// </summary>
  private IScene? NextScene;

  /// <summary>
  /// Callback который необходимо выполнить после завершения сцены, но до начала следующей
  /// </summary>
  private Action? BeforeNextScene;

  /// <summary>
  /// Список войнов гарнизона
  /// </summary>
  public readonly WarriorsList Army;

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
    this.Army = new(new List<IWarrior>() {
      new CommonWarrior(StringGenerator.GetRandomHumanName(), new CommonSword(), new LightweightBreastplate(), 80),
      new CommonWarrior(StringGenerator.GetRandomHumanName(), new CommonSword(), new CommonBreastplate(), 100),
      new CommonWarrior(StringGenerator.GetRandomHumanName(), new CommonSword(), new HeavyweightBreastplate(), 125),
    });

    // Инициализация списка сцен
    this.Scenes = new(this);

    // Запуск стартовой сцены
    this.CurrentScene = this.Scenes.Start;

    // Установим начальное количество монет
    this.ArmyLeader.ChangeCoins(15);
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
    if (Name != null)
      this.ArmyLeader.Name = Name;
  }

  /// <summary>
  /// Проверить, есть ли воин под таким индексом
  /// </summary>
  /// <param name="WarriorIndex">Индекс воина</param>
  /// <returns>Есть ли такой воин</returns>
  public bool IsWarrior(int WarriorIndex) => WarriorIndex >= 0 && WarriorIndex < this.Army.Count;

  /// <summary>
  /// Проверить ввод пользователя на вхождение в игровое меню
  /// </summary>
  /// <param name="UserInput"></param>
  /// <returns></returns>
  public bool CheckUserInput(string UserInput)
  {
    if (UserInput.Trim() == "")
    {
      this.SelectScene(this.Scenes.Select);
    }
    else if (UserInput.ToUpper() == "Q")
    {
      this.SelectScene(null);
    }
    else
      return false;

    return true;
  }

  /// <summary>
  /// Запустить игру на текущей сцене
  /// </summary>
  public void StartGame()
  {
    // Запуск текущей сцены
    while (true)
    {
      Console.Clear();

      // Хук до запуска следующей сцены
      if (this.BeforeNextScene != null)
        this.BeforeNextScene();

      this.CurrentScene.Start();

      // Жизненный цикл сцены
      do
      {
        this.CurrentScene.PrintAcions();

        if (!this.CurrentScene.IsMenuDisabled)
        {
          UserInteraction.NewLine();
          UserInteraction.NewLine();
          Console.WriteLine(">>> Нажмите Enter, чтобы выбрать локацию");
          Console.WriteLine(">>> Введите Q, чтобы выйти из игры");
        }

        string UserInput = "";

        if (!this.CurrentScene.IsPromptDisabled)
        {
          UserInteraction.NewLine(2);
          this.CurrentScene.Prompt();

          UserInteraction.PromptWelcome();
          UserInput = Console.ReadLine() ?? "";

          if (!this.CurrentScene.IsMenuDisabled && this.CheckUserInput(UserInput))
            break;
        }

        this.CurrentScene.Update(UserInput);
      } while (this.CurrentScene.IsContinue);

      // Хук до закрытия текущей сцены
      this.CurrentScene.Closing();

      if (this.NextScene == null)
        break;

      this.CurrentScene = this.NextScene;
      this.NextScene = null;
    }

    // По окончанию работы сцен, вывести сообщение об окончании игры
    UserInteraction.EndGameMessage(this);
  }
}
