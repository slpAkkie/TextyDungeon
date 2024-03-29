﻿namespace TextyDungeon.Creatures.Warriors;

using TextyDungeon.Objects.Equipment;
using TextyDungeon.Objects.Equipment.Armor;
using TextyDungeon.Objects.Equipment.Weapon;


/// <summary>
/// Базовый класс воинов
/// </summary>
internal abstract class IWarrior : ICreature
{
  /// <summary>
  /// Значние урона по умолчанию
  /// </summary>
  private const double DEFAULT_DAMAGE = 25.0;

  /// <summary>
  /// Цена наема воина
  /// </summary>
  public int HireCost { get; private set; }

  /// <summary>
  /// Урон воина
  /// </summary>
  public double Damage { get => this.Weapon.DamageRange.Random; }

  /// <summary>
  /// Показатель брони воина
  /// </summary>
  public double Armor { get => this.BodyArmor.ProtectionRate; }

  /// <summary>
  /// Броня на тело
  /// </summary>
  public IArmor BodyArmor { get; protected set; }

  /// <summary>
  /// Оружие
  /// </summary>
  public IWeapon Weapon { get; protected set; }


  /// <summary>
  /// Инициализация воина с установленными параметрами
  /// </summary>
  public IWarrior(string Name, IWeapon Weapon, IArmor BodyArmor, int HireCost) : base(Name, "")
  {
    this.Weapon = Weapon;
    this.BodyArmor = BodyArmor;

    this.HireCost = HireCost;
  }


  /// <summary>
  /// Получить урон
  /// </summary>
  /// <param name="Damage">Уоличество урона</param>
  /// <returns>Остался ли жив воин</returns>
  public bool TakeDamage(double Damage)
  {
    this.HP -= Damage / this.Armor;

    return this.IsAlive;
  }

  /// <summary>
  /// Вылечить воина
  /// </summary>
  /// <param name="HillAmount">Количество HP для восстановления</param>
  public void Hill(int HillAmount) => this.HP += HillAmount;

  /// <summary>
  /// Экипировать снаряжение
  /// </summary>
  /// <param name="Equipment"></param>
  public void Equip(IEquipment Equipment) {
    if (Equipment is IArmor)this.BodyArmor = (IArmor)Equipment;
    else if (Equipment is IWeapon) this.Weapon = (IWeapon)Equipment;
  }
}
