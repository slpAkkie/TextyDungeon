namespace TextyDungeon.Objects.Equipment.Weapon;

using TextyDungeon.Utils;


/// <summary>
/// Базовый класс для оружия
/// </summary>
internal abstract class IWeapon : IEquipment
{
  /// <summary>
  /// Диапазон возможного урона оружия
  /// </summary>
  public SRange DamageRange;


  /// <summary>
  /// Инициализация объекта оружия
  /// </summary>
  /// <param name="Name">Название оружия</param>
  /// <param name="Description">Описание оружия</param>
  /// <param name="DamageRange">Диапазон возможного урона</param>
  /// <param name="Cost">Цена оружия</param>
  public IWeapon(string Name, string Description, SRange DamageRange, int Cost) : base(Name, Description, Cost) => this.DamageRange = DamageRange;


  /// <summary>
  /// Напечатать информацию о броне
  /// </summary>
  public override void Print()
  {
    UserInteraction.WriteBlue(this.Name);
    Console.Write(" / Урон ");
    UserInteraction.WriteRed($"{this.DamageRange.MinValue}-{this.DamageRange.MaxValue} HP");
    Console.Write(", стоимость ");
    UserInteraction.WriteGreen($"{this.Cost} золотых монет");
    Console.WriteLine($". ({this.Description})");
  }
}
