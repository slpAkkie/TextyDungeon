namespace TextyDungeon.Scenes;

using TextyDungeon.Utils;


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
  public override bool ContinueCondition => true;


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
    Console.Clear();
    int? UserIntInput = Utils.ConvertToInt(UserInput, "Номер сцены должен быть числом");

    if (UserIntInput == null) return;

    this.GameInstance.SelectScene(this.GameInstance.Scenes.GetSceneByNumber((int)UserIntInput));
    this.CloseScene = true;
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
  public override void Prompt() => Console.WriteLine("Выберите локацию для перехода");
}
