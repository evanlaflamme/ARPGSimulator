using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			PlayerController pc = other.GetComponent<PlayerController> ();

			if (pc.hasKey) {
				GameManager gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager>();
				gm.nextLevel ();
			}
		}
	}
}
