﻿namespace TextyDungeon.Scenes;


/// <summary>
/// Начальная сцена игры
/// </summary>
internal class StartScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name { get => "Знакомство"; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  public override bool ContinueCondition => !this.Confirmation;

  private bool Confirmation = false;


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public StartScene(Game GameInstance) : base(GameInstance)
  {
    this.IsMenuDisabled = true;
  }


  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    if (UserInput == "") {
      Console.Clear();
      UserInteraction.WriteErrorTop("Имя Генерала не может быть пустым");
      return;
    }

    Console.Clear();
    Console.WriteLine(String.Format("Имя Генерала будет установлена как: {0}", UserInput));
    UserInteraction.NewLine();
    UserInteraction.WriteInfoLine("Согласны? (Да | Нет)");
    if (!(this.Confirmation = UserInteraction.GetYesNo())) return;


    this.GameInstance.SetArmyLeaderName(UserInput);
    this.GameInstance.SelectScene(this.GameInstance.Scenes.Dungeon);
  }


  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions() { }


  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt()
  {
    UserInteraction.WriteInfoLine("Введите имя своего генерала");
  }
}