using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 targetPosition;
	private Vector3 vel; // = new Vector3(0,0,0); /* values used to calculate velocity */

	public Rigidbody2D rb;
	public Transform trans;

	private const int MAX_HEALTH = 1000;
	private const int MAX_MANA = 200;
	public int health = MAX_HEALTH;
	public int mana = MAX_MANA;
	public float speed;

	public bool isCleave = false;
	public bool isBash = false;
	public bool isDash = false;
	public bool isMoving = false;
	public bool isDead = false;
	public bool isFacingRight = true;

	// Update is called once per frame
	void Update () {
		speed = rb.velocity.magnitude;
		isMoving = (speed == 0) ? false : true;

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			isMoving = true;
			updateAnimator ();
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			print ("Cleave.");
			isCleave = true;
			updateAnimator ();
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			print("Bash.");
			isBash = true;
			updateAnimator ();
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			print("Dash.");
			isDash = true;
			updateAnimator ();
		}

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
	}

	void FixedUpdate() {
		vel = targetPosition - trans.position;

		if (vel.x > 0 && !isFacingRight) {
			flip ();
		} else if (vel.x < 0 && isFacingRight) {
			flip ();
		}
	}		

	public void flip() {
		isFacingRight = !isFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void damage(int dmg) {
		health = health - dmg;

		if (health == 0) {
			isDead = true;
			updateAnimator ();
		}
	}

	private void updateAnimator() {
		
	}
}