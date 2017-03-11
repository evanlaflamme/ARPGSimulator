using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
}

class Equipment : Item
{
    private int bonus;

    public Equipment()
    {
        bonus = 0;
    }
    public virtual int RarityBonus(string rarity)
    {
        switch (rarity)
        {
            case "Green":
                bonus += 1;
                break;
            case "Blue":
                bonus += 5;
                break;
            case "Purple":
                bonus += 10;
                break;
            default:
                break;
        }
        return bonus;
    }
    public virtual void Bonus()
    {

    }
}

class Consumable : Item
{

    public virtual void UseConsumable()
    {

    }
}

class Key : Item
{

}

class Weapon: Equipment
{
    private int baseBonus, bonus;
    private string rarity;
    public Weapon(int baseBonus, string rarity)
    {
        this.baseBonus = baseBonus;
        this.rarity = rarity;
        this.bonus = baseBonus + RarityBonus(rarity);
    }
}

class Armor: Equipment
{
    private int baseBonus, bonus;
    private string rarity;
    public Armor(int baseBonus, string rarity)
    {
        this.baseBonus = baseBonus;
        this.rarity = rarity;
        this.bonus = baseBonus + RarityBonus(rarity);
    }
}

class HealthPot: Consumable
{
    private int baseHeal, levelBonus, totalHeal;
    public HealthPot(int levelBonus)
    {
        this.totalHeal = baseHeal * levelBonus;
    }
}

class ManaPot : Consumable
{
    private int baseHeal, levelBonus, totalHeal;
    public ManaPot(int levelBonus)
    {
        this.totalHeal = baseHeal * levelBonus;
    }
}
