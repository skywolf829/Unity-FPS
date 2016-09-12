using UnityEngine;
using System.Collections;

public class Boundaries : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Boundaries started");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision c)
    {
        
        Debug.Log("Hit!");
    }
}
