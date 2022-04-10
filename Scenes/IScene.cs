namespace TextyDungeon.Scenes;


/// <summary>
/// Базовый класс сцены
/// </summary>
internal abstract class IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public abstract string Name { get; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  protected abstract bool ContinueCondition { get; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться (с учетом немедленного закрытия)
  /// </summary>
  public bool IsContinue { get => this.ContinueCondition && !this.CloseScene; }

  /// <summary>
  /// Должна ли сцена завершиться вне зависимости от других условий
  /// </summary>
  protected bool CloseScene;

  /// <summary>
  /// Блокировка вывода игрового меню (Не меню сцены)
  /// </summary>
  public bool IsMenuDisabled = false;

  /// <summary>
  /// Блокировка вывода игрового меню (Не меню сцены)
  /// </summary>
  public bool IsPromptDisabled = false;

  /// <summary>
  /// Объект игры
  /// </summary>
  protected Game GameInstance;


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public IScene(Game GameInstance) => this.GameInstance = GameInstance;


  /// <summary>
  /// Запуск сцены
  /// </summary>
  public abstract void Start();

  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  /// <param name="UserInput">Строка, введенная пользователем</param>
  public abstract void Update(string UserInput);

  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public abstract void PrintAcions();

  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public abstract void Prompt();

  /// <summary>
  /// Выход со сцены
  /// </summary>
  public virtual void Closing() => this.CloseScene = false;
  }
