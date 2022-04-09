namespace TextyDungeon.Scenes;


/// <summary>
/// Сцена выбора локации
/// </summary>
internal class SelectScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name { get => "Перепутье"; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  public override bool ContinueCondition => false;


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public SelectScene(Game GameInstance) : base(GameInstance) { }


  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    int UserIntInput;

    try {
      UserIntInput = Convert.ToInt32(UserInput);
    } catch (FormatException) {
      UserInteraction.WriteErrorTop("Номер локации должен быть числом");
      return;
    }

    this.GameInstance.SelectScene(this.GameInstance.Scenes.GetSceneByNumber(UserIntInput));
  }


  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    int SceneNumber = 0;
    foreach (IScene Scene in this.GameInstance.Scenes.List)
      Console.WriteLine($"{++SceneNumber}. {Scene.Name}");
  }


  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt()
  {
    UserInteraction.NewLine(2);
    Console.WriteLine("Выберите локацию для перехода");
  }
}
