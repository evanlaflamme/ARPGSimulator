using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    public abstract void RarityBonus(string rarity);
}

class Equipment : Item
{
    private int bonus;

    public Equipment()
    {
        bonus = 0;
    }
    public override void RarityBonus(string rarity)
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
    }
    public virtual void Bonus()
    {

    }
}

class Consumable : Item
{
    public override void RarityBonus(string rarity)
    {
    }

    public virtual void UseConsumable()
    {

    }
}

class Key : Item
{
    public override void RarityBonus(string rarity)
    {
        //if boss then key drops
    }
}

class Weapon: Equipment
{
    private int baseBonus, bonus;
    private string rarity;
    public Weapon(int baseBonus, string rarity)
    {
        this.baseBonus = baseBonus;
        this.rarity = rarity;
        this.bonus = baseBonus + Rarity;
    }

    void TotalBonus()
    {
        bonus = (baseb)
    }
    
}
