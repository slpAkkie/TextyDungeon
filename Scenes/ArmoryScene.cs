namespace TextyDungeon.Scenes;


/// <summary>
/// Сцена оружейной
/// </summary>
internal class ArmoryScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name { get => "Оружейная"; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  public override bool ContinueCondition => false;


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public ArmoryScene(Game GameInstance) : base(GameInstance) { }


  /// <summary>
  /// Запуск сцены
  /// </summary>
  public override void Start()
  {
    base.Start();

    this.IsPromptDisabled = true;

    this.GameInstance.SelectScene(this.GameInstance.Scenes.Select, delegate () {
      UserInteraction.WriteDungerousLine("Выбор снаряжения пока что не готов, возвращайтесь сюда позже");
      UserInteraction.NewLine(2);
    });
  }


  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput) { }


  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions() { }


  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt() { }
}
