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
	public float maxSpeed;

	private const int DASH_LAG = 45;
	private int dashCtr;
	public bool isCleave = false;
	public bool isBash = false;
	public bool isDash = false;
	public bool isMoving = false;
	public bool isDead = false;
	public bool isFacingRight = true;

	// Update is called once per frame
	void Update () {
		targetPosition = transform.position;
		//speed = this.GetComponent<Rigidbody2D> ().velocity.magnitude;
		//isMoving = (speed == 0) ? false : true;
		 
		if (Input.GetKey (KeyCode.Mouse0) && dashCtr == 0) {
			targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			isMoving = true;
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 5);
		}
		if (Input.GetKeyDown (KeyCode.Mouse1)  && dashCtr == 0) {
			print ("Cleave.");
			anim.Play ("Cleave");
			isCleave = true;
		}
		if (Input.GetKeyDown (KeyCode.Q)  && dashCtr == 0) {
			print ("Bash.");
			anim.Play ("HeavyAttack");
			isBash = true;
		}
		if (Input.GetKeyDown (KeyCode.W)  && dashCtr == 0 && mana >= 100) {
			print ("Dash.");
			isDash = true;
			dashCtr = DASH_LAG;
		}

		if (dashCtr > 0) {
			dashCtr--;
		}
			
		if (mana < MAX_MANA)
			mana++;
		
		updateAnimator();
	}

	void FixedUpdate() {
		vel = targetPosition.x - trans.position.x;
		//print (vel);

		if (isDash) {
			//StartCoroutine("dashAttack");
			anim.Play ("Dash");
			//targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//transform.position = Vector3.LerpUnclamped (transform.position, targetPosition, 1 - Mathf.Exp(-40 * Time.deltaTime));

			Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
			mouseDir = mouseDir.normalized;

			rb.AddForce (mouseDir * 1500);
			mana -= 100;
			isDash = false;
		}

		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
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

	/*private IEnumerator dashAttack() {
		anim.Play ("Dash");
		targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 15);

		isDash = false;
		yield return new WaitForSeconds(1.5f);
	}*/
}