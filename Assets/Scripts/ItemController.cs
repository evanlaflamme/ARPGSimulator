using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    public GameObject[] itemsGO;
    private Item info;
    private GameObject gameItem;
    private string[] rarityName = { "Common", "Okay I Guess", "Uncommon", "Rare" };
    private string[] bonusName = { "of Might", "of Mass Memes", "of Speedish", "of Elie", "of Boii" };
    private string rn;
    // Use this for initialization
    void Start () {

	}

    //Generates an existing or new item
    public void GenerateItem()
    {
        int generatedItem = Random.Range(0, (GameManager.getItems().Count * 2)+1);
        if(generatedItem > GameManager.getItems().Count)
        {
            GenerateNewItem();
        }
        else
        {
           // Instantiate(gameItem, transform.position, transform.rotation);
        }
    }
	
    //Generates a new item
	public void GenerateNewItem()
    {
        int baseDammage = Random.Range(0, 6);
        Debug.Log("GETS");
        gameItem = itemsGO[Random.Range(0, 4)];
        Instantiate(gameItem, transform.position, transform.rotation);
        

        switch (gameItem.tag)
        {
            case "Weapon":
                info = new Weapon(baseDammage, RarityGenerator(), GameManager.levelBonus());
                break;
            case "Armor":
                info = new Armor(baseDammage, RarityGenerator(), GameManager.levelBonus());
                break;
        }
        GameManager.addItem((rn+ gameItem.tag+bonusName[baseDammage]), info);
    }
    string RarityGenerator()
    {
        string rarity = "";
        int itemRarity = Random.Range(0, 101);
        if (itemRarity <= 50)
        {
            rarity = "White";
            rn = rarityName[0];
        }
            
        else if (itemRarity <= 85 && itemRarity > 50)
        {
            rarity = "Green";
            rn = rarityName[0];
        }
        else if (itemRarity <= 95 && itemRarity > 85)
        {
            rarity = "Blue";
            rn = rarityName[0];
        }
        else if (itemRarity <= 99 && itemRarity > 95)
        {
            rarity = "Purple";
            rn = rarityName[0];
        }
        else if (itemRarity <= 100 && itemRarity > 99)
        {
            rarity = "Orange";
            rn = rarityName[0];
        }
        return rarity;
    }
}
