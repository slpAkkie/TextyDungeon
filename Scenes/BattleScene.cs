namespace TextyDungeon.Scenes;

using TextyDungeon.Creatures.Enemies;
using TextyDungeon.Creatures.Warriors;
using TextyDungeon.Utils;


/// <summary>
/// Сцена битвы
/// </summary>
internal class BattleScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name { get => "Битва"; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  protected override bool ContinueCondition => !this.IsArmyDead() && this.Enemy.IsAlive;

  /// <summary>
  /// Индекс выбранного война в списке
  /// </summary>
  private int IndexOfChosenWarrior;

  /// <summary>
  /// Номер выбранного война в списке
  /// </summary>
  private int NumberOfChosenWarrior;

  /// <summary>
  /// Сцена подземелья, из которой пришел игрок
  /// </summary>
  private DungeonScene Dungeon;

  /// <summary>
  /// Враг с которым будет идти битва
  /// </summary>
  private IEnemy Enemy;

  private bool IsWon { get => this.Enemy.IsDead; }

  /// <summary>
  /// Урон полученный в последней битве
  /// </summary>
  private int DamageTaken;

  /// <summary>
  /// Урон нанесенный в последней битве
  /// </summary>
  private double DamageGiven;

  /// <summary>
  /// Выбранный войн
  /// </summary>
  private IWarrior ChosenWarrior { get => this.GameInstance.Army[this.IndexOfChosenWarrior]; }


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public BattleScene(Game GameInstance, DungeonScene Dungeon, IEnemy Enemy) : base(GameInstance)
  {
    this.Dungeon = Dungeon;
    this.Enemy = Enemy;
  }


  /// <summary>
  /// Запуск сцены
  /// </summary>
  public override void Start() { }

  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    Console.Clear();
    int? UserIntInput = Utils.ConvertToInt(UserInput, "Номер воина должен быть числом");

    if (UserIntInput == null) return;

    this.IndexOfChosenWarrior = (this.NumberOfChosenWarrior = (int)UserIntInput) - 1;

    if (this.IndexOfChosenWarrior < 0)
      UserInteraction.WriteRedLine($"Нельзя выбрать {this.NumberOfChosenWarrior} война");
    else if (this.IndexOfChosenWarrior >= this.GameInstance.Army.Count)
      UserInteraction.WriteRedLine("Ваша армия не настолько большая. Такого война нет");
    else if (this.ChosenWarrior.IsDead)
      UserInteraction.WriteRedLine("К сожалению этот воин уже погиб, он не может больше сражаться. Упокой Господь его душу");
    else
      this.Fight();

    Console.WriteLine();
    Console.WriteLine();
  }

  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    Console.Write("Напротив вас стоит: ");
    UserInteraction.WriteBlue(this.Enemy.Name);
    Console.Write(" (HP: ");
    UserInteraction.WriteRed(this.Enemy.HP.ToString());
    Console.Write(", Урон: ");
    UserInteraction.WriteRed($"{this.Enemy.DamageRange.MinValue}-{this.Enemy.DamageRange.MaxValue}");
    Console.WriteLine(")");

    UserInteraction.NewLine();
    Console.WriteLine("В вашем распоряжении следующие воины:");

    this.GameInstance.Army.PrintList();
  }

  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt() => Console.WriteLine("Выберите война, чтобы отправить его в сражение");

  /// <summary>
  /// Отправить выбранного война в битву
  /// </summary>
  private void Fight()
  {
    this.DamageTaken = this.Enemy.Damage;
    this.DamageGiven = this.ChosenWarrior.Damage;
    bool WarriorDead = !this.ChosenWarrior.TakeDamage(this.DamageTaken);
    this.Enemy.TakeDamage(this.DamageGiven);

    if (WarriorDead)
      this.GameInstance.Army.QuantityOfDead++;

    if (this.IsWon) {
      int WinCost = this.Enemy.WinCost;
      this.GameInstance.ArmyLeader.ChangeCoins(WinCost);
      this.GameInstance.SelectScene(this.GameInstance.Scenes.Dungeon, delegate() {
        UserInteraction.WriteGreen("Враг побежден.");
        Console.Write(" Вы получили ");
        UserInteraction.WriteGreen($"{WinCost} монет");
        Console.Write(".");
        this.GameInstance.ArmyLeader.PrintCoins();
        UserInteraction.NewLine();
      });

      this.Dungeon.RemoveEnemy(this.Enemy);
    }

    PrintFightResult(this.Enemy);
    if ((this.GameInstance.Scenes.Dungeon as DungeonScene).IsEnemiesDead) {
      UserInteraction.NewLine();    
      Console.WriteLine("Все враги в подземелье повержены");
      UserInteraction.WriteBlueLine("Нажмите для продолжения...");
      Console.ReadLine();
    }
  }

  /// <summary>
  /// Выводит информацию о последней битве
  /// </summary>
  /// <param name="TheEnemy">Объект противника</param>
  private void PrintFightResult(IEnemy TheEnemy)
  {
    Console.Write($"Воин номер {this.IndexOfChosenWarrior + 1} сразился с ");
    UserInteraction.WriteRed($"\"{TheEnemy.Name}\"");
    Console.Write(". Нанесено ");
    UserInteraction.WriteRed($"{this.DamageGiven}");
    Console.Write(" урона, получено ");
    UserInteraction.WriteRed($"{this.DamageTaken}");
    Console.WriteLine($" урона.");

    if (this.IsWon)
      UserInteraction.WriteGreenLine("Воин одержал победу");
    else
      UserInteraction.WriteBlueLine("Враг все еще жив. Выберите кто будет атаковать следующим");
  }

  /// <summary>
  /// Проверяет пала ли вся армия и выводит сообщение об этом.
  /// </summary>
  /// <returns>true если армия мертва и продолжение игры не возможно, в противном случае false</returns>
  private bool IsArmyDead()
  {
    if (!this.GameInstance.Army.IsDead) return false;

    UserInteraction.WriteRedLine("Ваша армия пала в сражении с врагом");

    return true;
  }

  /// <summary>
  /// Выход со сцены
  /// </summary>
  public override void Closing()
  {
    if (!this.IsWon || this.IsWon && this.Dungeon.IsEnemiesDead) this.Dungeon.PreserveEnemyRefill = false;
    else this.Dungeon.PreserveEnemyRefill = true;
  }
}
