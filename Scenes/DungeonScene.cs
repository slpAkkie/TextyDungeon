namespace TextyDungeon.Scenes;

using TextyDungeon.Warriors;
using TextyDungeon.Enemies;
using TextyDungeon.Utils;


/// <summary>
/// Сцена подземелья
/// </summary>
internal class DungeonScene : IScene
{
  /// <summary>
  /// Название сцены
  /// </summary>
  public override string Name { get => "Подземелье"; }

  /// <summary>
  /// Условие, при котором сцена продолжает обновляться
  /// </summary>
  public override bool ContinueCondition => !this.IsArmyDead() && !this.GameInstance.Gameover;


  /// <summary>
  /// Инициализировать сцену
  /// </summary>
  /// <param name="GameInstance">Объект игры</param>
  public DungeonScene(Game GameInstance) : base(GameInstance) { }


  /// <summary>
  /// Индекс выбранного война в списке
  /// </summary>
  private int IndexOfChosenWarrior;

  /// <summary>
  /// Номер выбранного война в списке
  /// </summary>
  private int NumberOfChosenWarrior;

  /// <summary>
  /// Урон полученный в последней битве
  /// </summary>
  private int DamageTaken;

  /// <summary>
  /// Выбранный войн
  /// </summary>
  private IWarrior ChosenWarrior { get => this.GameInstance.Army[this.IndexOfChosenWarrior]; }


  /// <summary>
  /// Обновление состояния сцены
  /// </summary>
  public override void Update(string UserInput)
  {
    Console.Clear();
    int? UserIntInput = Utils.ConvertToInt(UserInput, "Номер воина должен быть числом");

    if (UserIntInput == null) return;

    this.IndexOfChosenWarrior = (this.NumberOfChosenWarrior = (int)UserIntInput) - 1;

    if (this.NumberOfChosenWarrior == 0) {
      this.GameInstance.SelectScene(null);
      this.GameInstance.Gameover = true;
      return;
    } else if (this.IndexOfChosenWarrior < 0)
      UserInteraction.WriteDungerousLine(String.Format("Нельзя выбрать {0} война", this.NumberOfChosenWarrior));
    else if (this.NumberOfChosenWarrior > this.GameInstance.Army.Count)
      UserInteraction.WriteDungerousLine("Ваша армия не настолько большая. Такого война нет");
    else if (this.ChosenWarrior.IsDead && !this.GameInstance.IsNecromancy)
      UserInteraction.WriteDungerousLine("К сожалению этот воин уже погиб, он не может больше сражаться. Упокой Господь его душу");
    else
      this.Battle();

    Console.WriteLine();
    Console.WriteLine();
  }


  /// <summary>
  /// Вывод информации по сцене и возможных действий
  /// </summary>
  public override void PrintAcions()
  {
    UserInteraction.WriteInfoLine(String.Format("Генерал, {0}, у вас есть армия из {1} войнов:", this.GameInstance.ArmyLeader.Name, this.GameInstance.Army.Count));

    this.GameInstance.PrintArmyList();

    Console.WriteLine();
    Console.WriteLine(String.Format("В вашем распоряжении {0} монет", this.GameInstance.ArmyLeader.Coins));
  }


  /// <summary>
  /// Вывод сообщения (Приглашения пользователя к вводу)
  /// </summary>
  public override void Prompt() => Console.WriteLine("Выберите война, чтобы отправить его в сражение");


  /// <summary>
  /// Выбрать случайного противника из списка
  /// </summary>
  /// <returns>Enemy - Объект противника</returns>
  private Enemy FindRandomEnemy()
  {
    return this.GameInstance.Enemies[new Random().Next(0, this.GameInstance.Enemies.Count)];
  }


  /// <summary>
  /// Отправить выбранного война в битву
  /// </summary>
  private void Battle()
  {
    Enemy RandomEnemy = this.FindRandomEnemy();
    this.DamageTaken = this.GameInstance.IsNecromancy ? 0 : RandomEnemy.Damage;
    bool WarriorDead = !this.ChosenWarrior.TakeDamage(this.DamageTaken);

    if (WarriorDead)
      this.GameInstance.QuantityOfDeadWarriors++;
    else this.GameInstance.ArmyLeader.ChangeCoins(RandomEnemy.WinCost);

    PrintBattleResult(RandomEnemy);
  }


  /// <summary>
  /// Выводит информацию о последней битве
  /// </summary>
  /// <param name="TheEnemy">Объект противника</param>
  private void PrintBattleResult(Enemy TheEnemy)
  {
    Console.Write($"Воин номер {this.IndexOfChosenWarrior + 1} сразился с ");
    UserInteraction.WriteDungerous($"\"{TheEnemy.Name}\"");
    Console.WriteLine($" и получил {this.DamageTaken} урона");

    if (!this.GameInstance.IsNecromancy)
      Console.WriteLine(String.Format(
        "Теперь у война {0} здоровья",
        this.ChosenWarrior.HP
      ));
    else
      Console.WriteLine("Мертвец не получает повреждений");

    if (this.ChosenWarrior.IsDead) {
      if (this.GameInstance.IsNecromancy)
        UserInteraction.WriteSuccessLine("Победа была неминуема");
      else
        UserInteraction.WriteDungerousLine("Воин погиб в сражении");
    } else
      UserInteraction.WriteSuccessLine("Воин одержал победу");
  }


  /// <summary>
  /// Проверяет пала ли вся армия и выводит сообщение об этом.
  /// </summary>
  /// <returns>true если армия мертва и продолжение игры не возможно, в противном случае false</returns>
  private bool IsArmyDead()
  {
    if (!this.GameInstance.IsArmyDead || this.GameInstance.IsNecromancy)
      return false;

    UserInteraction.WriteDungerousLine("Ваша армия пала в сражении с врагом");
    Console.WriteLine("Вы опечалены таким концом");
    Console.WriteLine("Прибегнуть к Некромантии?");

    if (this.GameInstance.IsNecromancy = UserInteraction.GetYesNo()) {
      UserInteraction.WriteInfoLine("Вы прибегли к некромантии, ваша душа осквернена, но теперь ваши войны бессмертны");
      UserInteraction.NewLine();
    }

    return !this.GameInstance.IsNecromancy;
  }
}
