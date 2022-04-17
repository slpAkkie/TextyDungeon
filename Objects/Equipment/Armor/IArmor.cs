namespace TextyDungeon.Objects.Equipment.Armor;


/// <summary>
/// Базовый класс для брони
/// </summary>
internal abstract class IArmor : IEquipment
{
  /// <summary>
  /// Показатель защиты брони
  /// </summary>
  public double ProtectionRate { get; private set; }


  /// <summary>
  /// Инициализация объекта брони
  /// </summary>
  /// <param name="Name">Название брони</param>
  /// <param name="Description">Описание брони</param>
  /// <param name="ProtectionRate">Показатель зашиты брони</param>
  /// <param name="Cost">Цена брони</param>
  public IArmor(string Name, string Description, double ProtectionRate, int Cost) : base(Name, Description, Cost) => this.ProtectionRate = ProtectionRate;


  /// <summary>
  /// Напечатать информацию о броне
  /// </summary>
  public override void Print()
  {
    UserInteraction.WriteBlue(this.Name);
    Console.Write(" / Защита ");
    UserInteraction.WriteGreen($"{this.ProtectionRate.ToString().Replace(',', '.')}");
    Console.Write(", стоимость ");
    UserInteraction.WriteGreen($"{this.Cost} золотых монет");
    Console.WriteLine($". ({this.Description})");
  }
}
