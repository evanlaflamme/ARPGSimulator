using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//private Vector3 targetPosition;

	public Rigidbody2D rb;
	public Transform trans;
	public Animator anim;

	private const int MAX_HEALTH = 1000;
	private const int MAX_MANA = 200;
	public int health = MAX_HEALTH;
	public float mana = MAX_MANA;
	public float speed;
	public float maxSpeed;

	private const int DASH_LAG = 45;
	private int dashCtr;

	private const int CLEAVE_LAG = 35;
	private int cleaveCtr;

	private const int BASH_LAG = 45;
	private int bashCtr;

	private const int LEAP_LAG = 75;
	private int leapCtr;

	public bool isCleave = false;
	public bool isBash = false;
	public bool isDash = false;
	public bool isMoving = false;
	public bool isDead = false;
	public bool isFacingRight = true;

	// Update is called once per frame
	void Update () {
		//targetPosition = transform.position;
		//speed = this.GetComponent<Rigidbody2D> ().velocity.magnitude;
		//isMoving = (speed == 0) ? false : true;
		 
		if (Input.GetKey (KeyCode.Mouse0) && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && leapCtr == 0) {
			//targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//isMoving = true;
			//transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 5);
			Vector3 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
			rb.AddForce (mouseDir * 50);
		}
		if (Input.GetKey (KeyCode.Mouse1)  && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && leapCtr == 0) {
			print ("Cleave.");
			anim.Play ("Cleave");
			isCleave = true;
			cleaveCtr = CLEAVE_LAG;
		}
		if (Input.GetKey (KeyCode.Q)  && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && mana >= 60 && leapCtr == 0) {
			print ("Bash.");
			anim.Play ("HeavyAttack");
			isBash = true;
			bashCtr = BASH_LAG;
			mana -= 60;
		}
		if (Input.GetKeyDown (KeyCode.W)  && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && mana >= 100 && leapCtr == 0) {
			print ("Dash.");
			isDash = true;
			dashCtr = DASH_LAG;
		}

		if (Input.GetKey (KeyCode.E)  && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && mana >= 200 && leapCtr == 0) {
			print ("Leap.");
			anim.Play ("LeapSlam");
			leapCtr = LEAP_LAG;
			mana -= 200;
		}

		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}

		if (dashCtr > 0) {
			dashCtr--;
		} 

		if (cleaveCtr > 0) {
			cleaveCtr--;
		}

		if (bashCtr > 0) {
			bashCtr--;
		}


		if (leapCtr > 0) {
			leapCtr--;
		}
			
		if (mana < MAX_MANA) {
			mana += 0.75f;
			if (mana > MAX_MANA)
				mana = MAX_MANA;
		}
		
		updateAnimator();
	}

	void FixedUpdate() {
		//print (vel);

		if (isDash) {
			//StartCoroutine("dashAttack");
			anim.Play ("Dash");
			//targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			//transform.position = Vector3.LerpUnclamped (transform.position, targetPosition, 1 - Mathf.Exp(-40 * Time.deltaTime));

			Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
			mouseDir = mouseDir.normalized;

			rb.AddForce (mouseDir * 3000);
			mana -= 100;
			isDash = false;
		}

		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
			
		if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - trans.position.x > 0 && !isFacingRight && dashCtr == 0) {
			flip ();
		} else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - trans.position.x < 0 && isFacingRight && dashCtr == 0) {
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
		anim.SetFloat ("Speed", Mathf.Abs(rb.velocity.magnitude));
		anim.SetBool ("isDead", isDead);
	}

	public int doDamage(float multiplier) {
		return (int)(Random.Range(20, 30) * multiplier);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("OctalpusHit")) {
			StartCoroutine (Paint ());
			damage (100);
		} else if (other.gameObject.CompareTag ("ZombieHit")) {
			StartCoroutine (Paint ());
			damage (20);
		}
	}

	IEnumerator Paint() {
		SpriteRenderer renderer = this.GetComponentInChildren<SpriteRenderer> ();
		renderer.color = new Color(255, 0, 0, 200);
		yield return new WaitForSeconds(0.2F);
		renderer.color = Color.white;
	}
}