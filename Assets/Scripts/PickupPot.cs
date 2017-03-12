using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPot : MonoBehaviour {
    private HealthPots hp = new HealthPots(1);
    private ManaPots mp = new ManaPots(1);
	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("HealthPot"))
            {
                hp.addPot();
                Debug.Log(hp.Quantity);
            }
            else if(gameObject.CompareTag("ManaPot"))
            {
                mp.addPot();
            }
            Destroy(gameObject);
        }
        
    }
}
