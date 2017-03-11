using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    private Hashtable items = new Hashtable();
	// Use this for initialization
	void Start () {

	}
	
	public void GenerateNewItem()
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

        Item info;
        GameObject gameItem;
        gameItem.transform.position = transform.position;
        gameItem.transform.rotation = transform.rotation;

        switch (Random.Range(0,4))
        {

            case 0:
                gameItem = (GameObject) Instantiate(GameObject.FindGameObjectWithTag("Weapon"));
                info = new Weapon(Random.Range(0, 6), rarity, 1);
                items.Add(gameItem, info);
                break;
            case 1:
                gameItem = (GameObject)Instantiate(GameObject.FindGameObjectWithTag("Armor"));
                info = new Armor(Random.Range(0, 6), rarity, 1);
                items.Add(gameItem, info);
                break;
            case 2:
                gameItem = (GameObject)Instantiate(GameObject.FindGameObjectWithTag("HealthPot"));
                info = new HealthPot(1);
                items.Add(gameItem, info);
                break;
            case 3:
                gameItem = (GameObject)Instantiate(GameObject.FindGameObjectWithTag("Weapon"));
                info = new ManaPot(1);
                items.Add(gameItem, info);
                break;
        }
        gameItem.transform.position = transform.position;
        gameItem.transform.rotation = transform.rotation;
    }
}
