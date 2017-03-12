using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour {

	public GameObject player;
	public Animator anim;
	private Vector3 targetPosition;
    public GameObject[] items;
    public ItemController ic;

	public int health = 200;

	public float range;
	public float attackRange;
	private int attackCtr;

	public bool isDead = false;
	public bool isFacingRight = true;
	public float speed;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		GameManager gm = GameObject.FindWithTag ("GameController").GetComponent<GameManager> ();

		health = Convert.ToInt32(health * (1 + 0.2f * gm.getLevel ()));

	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			if ((Vector3.Distance (gameObject.transform.position, player.transform.position)) <= range) {
				if ((Vector3.Distance (gameObject.transform.position, player.transform.position)) >= attackRange) {
					//Move towards player
					targetPosition = player.transform.position;
					transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 0.20F);
					speed = Mathf.Abs (targetPosition.magnitude - transform.position.magnitude);
				} else {
					//Attack
					if (attackCtr == 0) {
						anim.Play ("EnemyAttack");
						attackCtr = (int)UnityEngine.Random.Range (50, 100);
					}

					if (attackCtr > 0) {
						attackCtr--;
					}
				}

				if (player.transform.position.x - gameObject.transform.position.x > 0 && !isFacingRight) {
					flip ();
				} else if (player.transform.position.x - gameObject.transform.position.x < 0 && isFacingRight) {
					flip ();
				}
			}
		}

		updateAnimator ();
	}

	void damage(int dmg) {
		health = health - dmg;
		if (health <= 0) {
			isDead = true;
			Dead ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Cleave")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			StartCoroutine (Paint ());
			damage (source.doDamage (1.0f));
		} else if (other.gameObject.CompareTag ("Bash")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			StartCoroutine (Paint ());
			damage (source.doDamage (1.5f));
		} else if (other.gameObject.CompareTag ("Dash")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			StartCoroutine (Paint ());
			damage (source.doDamage (1.1f));
		} else if (other.gameObject.CompareTag ("Dash1")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			StartCoroutine (Paint ());
			damage (source.doDamage (2.0f));
		} else if (other.gameObject.CompareTag ("Explosion")) {
			PlayerController source = other.gameObject.GetComponentInParent<PlayerController> ();
			StartCoroutine (Paint ());
			damage (source.doDamage (5.0f));
		}
	}

	private void updateAnimator() {
		anim.SetFloat ("Speed", speed);
		anim.SetBool ("isDead", isDead);
	}

	public void flip() {
		isFacingRight = !isFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

    void Dead()
    {
        if (health <= 0)
        {
            ic.GenerateNewItem();
        }
    }

	IEnumerator Paint() {
		SpriteRenderer renderer = this.GetComponentInChildren<SpriteRenderer> ();
		renderer.color = new Color(255, 0, 0, 200);
		yield return new WaitForSeconds(0.2F);
		renderer.color = Color.white;
	}
}
