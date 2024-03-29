﻿namespace TextyDungeon.Scenes;


/// <summary>
/// Хранит список всех сцен, доступных для смены
/// </summary>
internal class SceneList
{
  /// <summary>
  /// Список сцен, который может быть выведен пользователю как один из вариантов локаций
  /// </summary>
  private List<IScene> EnumerableScenes;

  public IScene Dungeon { get => this.EnumerableScenes[0]; }
  public IScene Tavern { get => this.EnumerableScenes[1]; }
  public IScene Barracks { get => this.EnumerableScenes[2]; }
  public IScene Armory { get => this.EnumerableScenes[3]; }

  public IScene Start { get; private set; }
  public IScene Select { get; private set; }

  /// <summary>
  /// Список достпуных сцен для переключения пользователем
  /// </summary>
  public IEnumerable<IScene> List
  {
    get {
      foreach (IScene Scene in EnumerableScenes)
        yield return Scene;

      yield break;
    }
  }


  /// <summary>
  /// Инициализирует сцены
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public SceneList(Game GameInstance)
  {
    // Инициализация сцен
    this.EnumerableScenes = new List<IScene>
    {
      new DungeonScene(GameInstance),
      new TavernScene(GameInstance),
      new BarracksScene(GameInstance),
      new ArmoryScene(GameInstance),
    };

    this.Start = new StartScene(GameInstance);
    this.Select = new SelectScene(GameInstance);
  }


  /// <summary>
  /// Получить сцену по ее человекопонятному номеру
  /// </summary>
  /// <param name="SceneNumber">Номер сцены от 1 до количества сцен</param>
  /// <returns>Запрошенная сцена</returns>
  public IScene GetSceneByNumber(int SceneNumber) => this.EnumerableScenes[SceneNumber - 1];
}
