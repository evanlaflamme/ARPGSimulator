using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    private Hashtable items = new Hashtable();
    public GameObject[] itemsGO;
    private Item info;

    // Use this for initialization
    void Start () {

	}

    //Generates an existing or new item
    public void GenerateItem()
    {
        int generatedItem = Random.Range(0, (items.Count * 2)+1);
        if(generatedItem > items.Count)
        {
            GenerateNewItem();
        }
        else
        {
            //Instantiate(items, transform.position, transform.rotation);
        }
    }
	
    //Generates a new item
	public void GenerateNewItem()
    {
        int itemType = Random.Range(0, 4);
        GameObject gameItem = itemsGO[itemType];
        Instantiate(gameItem, transform.position, transform.rotation);
        

        switch (gameItem.tag)
        {
            case "Weapon":
                info = new Weapon(Random.Range(0, 6), RarityGenerator(), 1);
                break;
            case "Armor":
                info = new Armor(Random.Range(0, 6), RarityGenerator(), 1);
                break;
            case "HealthPot":
                info = new HealthPots(1);
                break;
            default:
                info = new ManaPots(1);
                break;
        }
        items.Add(gameItem, info);
    }
    string RarityGenerator()
    {
        string rarity = "";
        int itemRarity = Random.Range(0, 101);
        if (itemRarity <= 50)
            rarity = "White";
        else if (itemRarity <= 85 && itemRarity > 50)
            rarity = "Green";
        else if (itemRarity <= 95 && itemRarity > 85)
            rarity = "Blue";
        else if (itemRarity <= 100 && itemRarity > 95)
            rarity = "Purple";
        return rarity;
    }
}
