using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			anim.SetBool ("open", true);
			//anim.Play("Open");
		}
		GameManager gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager>();
		gm.nextLevel ();
	}
}
