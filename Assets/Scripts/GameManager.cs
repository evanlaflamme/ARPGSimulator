using UnityEngine;
using System.Collections;

using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;
	private static int level = 1;
	public AudioClip[] clips;
	public AudioSource audioSource;

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{
		print (level);
		audioSource.clip = clips [UnityEngine.Random.Range (0, clips.Length - 1)];
		audioSource.Play();
	}

	//Update is called every frame.
	void Update()
	{
		
	}

	public static int getLevel() {
		return level;
	}

	public void nextLevel() {
		level++;
		SceneManager.LoadScene (Application.loadedLevel);
	}
}