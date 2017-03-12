using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctalpusController : MonoBehaviour {
	private Vector3 targetPosition;
	private const int MAX_HEALTH = 1000;
	public int health;

	private const int ATTACK_LAG = 300;
	private int attackCtr;

	public GameObject player;
	public Animator anim;
	public Rigidbody2D rb;
	public float range;
	public float attackRange;
	public bool isDead = false;


	// Use this for initialization
	void Start () {
		health = MAX_HEALTH;
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			if ((Vector3.Distance (rb.position, player.transform.position)) <= range) {
				if ((Vector3.Distance (rb.position, player.transform.position)) >= attackRange) {
					//Move towards player
					targetPosition = player.transform.position;
					transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 0.20F);
				} else {
					//Attack
					if (attackCtr == 0) {
						anim.Play ("Attack");
						attackCtr = (int)Random.Range (200, 600);
					}

					if (attackCtr > 0) {
						attackCtr--;
					}
				}
			}
		}
		updateAnim ();
	}

	void updateAnim() {
		anim.SetBool ("isDead", isDead);
	}

	void damage(int dmg) {
		health -= dmg;
		if (health <= 0)
			isDead = true;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Cleave")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			damage (source.doDamage (1.0f));
		} else if (other.gameObject.CompareTag ("Bash")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			damage (source.doDamage (1.3f));
		} else if (other.gameObject.CompareTag ("Dash")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			damage (source.doDamage (1.1f));
		} else if (other.gameObject.CompareTag ("Dash1")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			damage (source.doDamage (2.0f));
		} else if (other.gameObject.CompareTag ("Explosion")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			damage (source.doDamage (5.0f));
		}
	}
}
