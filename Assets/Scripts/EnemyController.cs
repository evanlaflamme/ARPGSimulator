using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject player;
	public Animator anim;


	private const int MAX_HEALTH = 200;
	public int health = MAX_HEALTH;
	public bool isDead = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, Time.deltaTime * 0.15F);
		}

		updateAnimator ();
	}

	void damage(int dmg) {
		health = health - dmg;
		if (health <= 0)
			isDead = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Cleave")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController>();
			damage (source.doDamage(1.0f));
		}
	}

	private void updateAnimator() {
		//anim.SetFloat ("Speed", Mathf.Abs(rb.velocity.magnitude));
		anim.SetBool("isDead", isDead);
	}
}
