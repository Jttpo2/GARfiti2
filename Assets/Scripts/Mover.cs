using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = transform.GetComponent<Rigidbody> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void applyForce(Vector3 force) {
		rigidBody.AddForce (force);
	}
}
