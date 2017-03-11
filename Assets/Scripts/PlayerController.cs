using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 targetPosition;
	public int health;
	public int mana;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			print ("Cleave.");
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			print("Bash.");
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			print("Dash.");
		}

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
	}

	public void damage(int dmg) {
		health = health - dmg;
	}
}