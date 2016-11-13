using UnityEngine;
using System.Collections;

public class Keyboard : MonoBehaviour
{

	public TexturePainter painter;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			painter.resetCanvas ();
		}
	}
}
