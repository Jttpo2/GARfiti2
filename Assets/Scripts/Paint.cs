using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour {

	public GameObject bluePaint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			Instantiate (bluePaint, transform.position, transform.rotation);
		}
	}
}