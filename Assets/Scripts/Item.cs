using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
}

class Equipment : Item
{
    private int baseStat, bonus;
    private float levelBonus;
    private string rarity;

    public Equipment(int baseStat, string rarity, int levelBonus)
    {
        this.baseStat = baseStat;
        this.rarity = rarity;
        this.levelBonus = levelBonus;
		this.bonus = Convert.ToInt32((baseStat + RarityBonus(rarity)) * (1 + 0.2f * levelBonus));
    }
    public virtual int ApplyBonus(int stat)
    {
        return 0;
    }

    public int getBonusPercent()
    {
        return bonus / baseStat * 100;
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
            case "Orange":
                bonus += 15;
                break;
            default:
                break;
        }
        return bonus;
    }
    
    
}

class Consumable : Item
{

    private int baseHeal, totalHeal;
    private int quantity;
    public int Quantity
    {
        get
        {
            return this.quantity;
        }
        set
        {
            this.quantity = value;
        }
    }
    public Consumable (int baseHeal)
    {
        this.baseHeal = baseHeal;
    }
    

    public void addPot()
    {
        this.quantity++;
    }

    public int usePot(int currentValue)
    {
        quantity--;
        return currentValue + totalHeal;
    }
}

class Key : Item
{
    private bool isKey;
    public bool IsKey
    {
        get;
        set;
    }
    public Key()
    {
        this.isKey = false;
    }
    public bool KeyAcquired()
    {
        return this.isKey;
    }

}

class Weapon: Equipment
{
    private int baseDammage, bonus;
    private float levelBonus;
    private string rarity;

    public Weapon(int baseDammage, string rarity, int levelBonus) : base(baseDammage, rarity, levelBonus)
    {
    }
    public int applyBonus(int stat)
    {
        return stat + bonus;
    }
}

class Armor: Equipment
{
    private int baseDammage, bonus;
    private string rarity;

    public Armor(int baseDammage, string rarity, int levelBonus) : base(baseDammage, rarity, levelBonus)
    {
    }

    public int applyBonus(int stat)
    {
        return stat -bonus;
    }
}

class HealthPots: Consumable
{
    private int baseHeal, totalHeal;
    public HealthPots(int baseHeal) : base(baseHeal)
    {
    }
}

class ManaPots : Consumable
{
    private int baseHeal, totalHeal;
    
    public ManaPots(int baseHeal) : base(baseHeal)
    {
    }
}
