using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject player;

	private const int MAX_HEALTH = 1000;
	public int health = MAX_HEALTH;

	private Vector2 targetPosition;
    public Transform spawnLocation;
    public GameObject item;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//targetPosition = player.transform.position;

		//transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 0.35F);

        if (health >0)
            damage(50);
        Dead();
        Debug.Log(health);
	}

	void damage(int dmg) {
		health = health - dmg;
	}

    void Dead()
    {
        if (health <= 0)
        {
            Instantiate(item, spawnLocation.position, spawnLocation.rotation);
        }
    }
}
