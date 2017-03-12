using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		GameManager gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager>();
		gm.nextLevel ();
	}
}
