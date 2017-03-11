using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
}

class Equipment : Item
{
    private int baseBonus, bonus;
    private float levelBonus;
    private string rarity;

    public Equipment(int baseBonus, string rarity, int levelBonus)
    {
        this.baseBonus = baseBonus;
        this.rarity = rarity;
        this.levelBonus = levelBonus;
        this.bonus = (baseBonus + RarityBonus(rarity)) * levelBonus;
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
    
    
}

class Consumable : Item
{

    private int baseHeal, levelBonus, totalHeal;
    public Consumable (int levelBonus)
    {
        this.totalHeal = baseHeal * levelBonus;
    }
}

class Key : Item
{

}

class Weapon: Equipment
{
    private int baseBonus, bonus;
    private float levelBonus;
    private string rarity;

    public Weapon(int baseBonus, string rarity, int levelBonus) : base(baseBonus, rarity, levelBonus)
    {
    }
}

class Armor: Equipment
{
    private int baseBonus, bonus;
    private string rarity;

    public Armor(int baseBonus, string rarity, int levelBonus) : base(baseBonus, rarity, levelBonus)
    {
    }
}

class HealthPot: Consumable
{
    private int baseHeal, levelBonus, totalHeal;
    public HealthPot(int levelBonus) : base(levelBonus)
    {
    }
}

class ManaPot : Consumable
{
    private int baseHeal, levelBonus, totalHeal;
    public ManaPot(int levelBonus) : base(levelBonus)
    {
    }
}
