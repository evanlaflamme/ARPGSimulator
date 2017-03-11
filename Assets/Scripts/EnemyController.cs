using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject player;
	public Animator anim;
	private Vector3 targetPosition;

	private const int MAX_HEALTH = 200;
	public int health = MAX_HEALTH;
	public bool isDead = false;
	public bool isFacingRight = true;
	public float speed;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			targetPosition = player.transform.position;
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 0.15F);
			speed = Mathf.Abs(targetPosition.magnitude - transform.position.magnitude);
		}

		if (player.transform.position.x - gameObject.transform.position.x > 0 && !isFacingRight && !isDead) {
			flip ();
		} else if (player.transform.position.x - gameObject.transform.position.x < 0 && isFacingRight && !isDead) {
			flip ();
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
}
