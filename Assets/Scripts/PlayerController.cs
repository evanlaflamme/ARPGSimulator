using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 targetPosition;
	private float vel; // = new Vector3(0,0,0); /* values used to calculate velocity */

	private Vector3 dash = new Vector3 ();

	public Rigidbody2D rb;
	public Transform trans;
	public Animator anim;

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
	//public bool takeInput = true;

	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		//speed = this.GetComponent<Rigidbody2D> ().velocity.magnitude;
		//isMoving = (speed == 0) ? false : true;
			if (Input.GetKey (KeyCode.Mouse0)) {
				targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				isMoving = true;
				updateAnimator ();
			}
			if (Input.GetKeyDown (KeyCode.Mouse1)) {
				print ("Cleave.");
				anim.Play ("Cleave");
				isCleave = true;
				updateAnimator ();
			}
			if (Input.GetKeyDown (KeyCode.Q)) {
				print ("Bash.");
				anim.Play ("HeavyAttack");
				isBash = true;
				updateAnimator ();
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				print ("Dash.");
				isDash = true;
				takeInput = false;
				updateAnimator ();
			}
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 5);
=======
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
>>>>>>> 1b095a7b50a1fe448aace657a55c0efdf1436786
	}

	void FixedUpdate() {
		vel = targetPosition.x - trans.position.x;

		if (isDash) {
			/*if ((Camera.main.ScreenToWorldPoint (Input.mousePosition)).x > transform.position.x)
				dash.x = (Camera.main.ScreenToWorldPoint (Input.mousePosition)).x - transform.position.x;
			else
				dash.x = (Camera.main.ScreenToWorldPoint (Input.mousePosition)).x + transform.position.x;
			
			if ((Camera.main.ScreenToWorldPoint (Input.mousePosition)).y > transform.position.y)
				dash.y = (Camera.main.ScreenToWorldPoint (Input.mousePosition)).y - transform.position.y;
			else
				dash.y = (Camera.main.ScreenToWorldPoint (Input.mousePosition)).y + transform.position.y; */

			//dash.x = (1 / (2 *(dash.sqrMagnitude))) * dash.x;
			//dash.y = (1 / (2 *(dash.sqrMagnitude))) * dash.y;

			targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			anim.Play ("Dash");
			transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * 15);

			isDash = false;
		}

		if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - trans.position.x > 0 && !isFacingRight) {
			flip ();
		} else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - trans.position.x < 0 && isFacingRight) {
			flip ();
		}
		updateAnimator ();
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
		anim.SetFloat ("Speed", Mathf.Abs(vel));
	}
}