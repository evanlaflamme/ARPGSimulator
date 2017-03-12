using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject[] items;
    private GameObject obj;
	// Use this for initialization
	void Start () {
        obj = items[Random.Range(0, 4)];
        Instantiate(obj, transform.position, transform.rotation);
        Debug.Log(obj.tag);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
