using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private GameManager gameManager;

	public Rigidbody2D rb;
	public Transform trans;
	public Animator anim;
	public Transform healthBar;
	public Transform manaBar;
	public Text healthText;
	public Text manaText;
	public Text gameOverDialog;
	public Text hpPotText;
	public Text manaPotText;

    private HealthPots hp = new HealthPots(1);
    private ManaPots mp = new ManaPots(1);

    private const int MAX_HEALTH = 1000;
	private const int MAX_MANA = 200;
	public int health = 1000;
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

	public bool hasKey = false;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			//targetPosition = transform.position;
			//speed = this.GetComponent<Rigidbody2D> ().velocity.magnitude;
			//isMoving = (speed == 0) ? false : true;
		 
			if (Input.GetKey (KeyCode.Mouse0) && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && leapCtr == 0) {
				//targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//isMoving = true;
				//transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 5);
				Vector3 mouseDir = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - gameObject.transform.position).normalized;
				rb.AddForce (mouseDir * 50);
			}
			if (Input.GetKey (KeyCode.Mouse1) && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && leapCtr == 0) {
				print ("Cleave.");
				anim.Play ("Cleave");
				isCleave = true;
				cleaveCtr = CLEAVE_LAG;
			}
			if (Input.GetKey (KeyCode.Q) && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && mana >= 60 && leapCtr == 0) {
				print ("Bash.");
				anim.Play ("HeavyAttack");
				isBash = true;
				bashCtr = BASH_LAG;
				mana -= 60;
			}
			if (Input.GetKeyDown (KeyCode.W) && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && mana >= 100 && leapCtr == 0) {
				print ("Dash.");
				isDash = true;
				dashCtr = DASH_LAG;
			}

			if (Input.GetKey (KeyCode.E) && dashCtr == 0 && cleaveCtr == 0 && bashCtr == 0 && mana >= 200 && leapCtr == 0) {
				print ("Leap.");
				anim.Play ("LeapSlam");
				leapCtr = LEAP_LAG;
				mana -= 200;
			}
			if (Input.GetKey (KeyCode.Alpha1)) {
				if (hp.Quantity > 0)
					health = hp.usePot (MAX_HEALTH);
			}
			if (Input.GetKey (KeyCode.Alpha2)) {
				if (mp.Quantity > 0)
					mana = mp.usePot (MAX_MANA);
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
				mana += 0.1f;
				if (mana > MAX_MANA)
					mana = MAX_MANA;
			}
		} else {
			if (Input.GetKeyDown (KeyCode.R)) {
				gameManager.ResetGame ();
			}
			if (Input.GetKeyDown (KeyCode.Escape)) {
				gameManager.ExitGame ();
			}
		}
		updateHud ();
		updateAnimator ();
	}

	void FixedUpdate() {
		//print (vel);
		if (!isDead) {
			if (isDash) {
				//StartCoroutine("dashAttack");
				anim.Play ("Dash");
				//targetPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//transform.position = Vector3.LerpUnclamped (transform.position, targetPosition, 1 - Mathf.Exp(-40 * Time.deltaTime));

				Vector3 mouseDir = Camera.main.ScreenToWorldPoint (Input.mousePosition) - gameObject.transform.position;
				mouseDir = mouseDir.normalized;

				rb.AddForce (mouseDir * 3000);
				mana -= 100;
				isDash = false;
			}

			if (rb.velocity.magnitude > maxSpeed) {
				rb.velocity = rb.velocity.normalized * maxSpeed;
			}
			
			if (Camera.main.ScreenToWorldPoint (Input.mousePosition).x - gameObject.transform.position.x > 0 && !isFacingRight && dashCtr == 0) {
				flip ();
			} else if (Camera.main.ScreenToWorldPoint (Input.mousePosition).x - gameObject.transform.position.x < 0 && isFacingRight && dashCtr == 0) {
				flip ();
			}
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
			gameOverDialog.text = "GAME OVER\nPress 'R' to restart or 'ESC' to quit.";
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
		
	private void updateHud () {
		Vector3 hpScale = healthBar.localScale;
		hpScale.x = (float)health / (float)MAX_HEALTH;  
		healthBar.localScale = hpScale;
		healthText.text = "Health:\n" + health + "/1000";

		Vector3 manaScale = manaBar.localScale;
		manaScale.x = (float)mana / (float)MAX_MANA;  
		manaBar.localScale = manaScale;
		manaText.text = "Mana: \n" + (int)mana + "/200";

		hpPotText.text = hp.Quantity.ToString ();
		manaPotText.text = mp.Quantity.ToString ();
	}
		
	void OnTriggerEnter2D (Collider2D other) {
		if (!isDead) {
			if (other.gameObject.CompareTag ("OctalpusHit")) {
				StartCoroutine (Paint ());
				damage (100);
			} else if (other.gameObject.CompareTag ("ZombieHit")) {
				StartCoroutine (Paint ());
				damage (20);
			}
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.gameObject.tag;
        switch (name)
        {
            case "HealthPot":
                hp.addPot();
                break;
            case "ManaPot":
                mp.addPot();
                break;
            case "Weapon":
                break;
            case "Armor":
                break;
            case "Key":
                hasKey = true;
                break;
        }
    }

    IEnumerator Paint() {
		SpriteRenderer renderer = this.GetComponentInChildren<SpriteRenderer> ();
		renderer.color = new Color(255, 0, 0, 200);
		yield return new WaitForSeconds(0.2F);
		renderer.color = Color.white;
	}
}