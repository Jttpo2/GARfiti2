using UnityEngine;
using System.Collections;

public class BluePaint : MonoBehaviour {

	private GameObject target;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("ARCamera");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (target.transform);
	
	}
}
