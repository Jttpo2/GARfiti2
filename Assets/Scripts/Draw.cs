using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Draw : MonoBehaviour
{

	private const int LEFT_BUTTON = 0;

	public Transform brush;
	public float brushSize = 100f;

	//	public Transform imageTarget1;
	//	public Transform imageTarget2;

	private Transform[] imageTargets;

	private Vector3 brushSizeVector;

	void Start ()
	{
		imageTargets = getImageTargets ();
		brushSizeVector = new Vector3 (brushSize, brushSize, brushSize);
	}

	// Update is called once per frame
	void Update ()
	{
		foreach (Transform imageTarget in imageTargets) {
			paintByMouse (imageTarget);
			paintByTouch (imageTarget);
		}
	}

	private void paintByTouch (Transform imageTarget)
	{
		foreach (Touch touch in Input.touches) {
			Vector3 touchIntersection = getPlaneIntersectionForScreenPoint (touch.position, imageTarget);
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
				paintAt (touchIntersection);

			}
		}
	}

	private void paintByMouse (Transform imageTarget)
	{
		// Paints by adding lots of object copies of the brush
		Vector3 mouseIntersection;
		mouseIntersection = getPlaneIntersectionForScreenPoint (Input.mousePosition, imageTarget);

		if (Input.GetMouseButton (0)) {
			paintAt (mouseIntersection);	
		}
	}

	private Vector3 getPlaneIntersectionForScreenPoint (Vector3 screenPoint, Transform imageTarget)
	{

//		Transform imageTarget = GameObject.Find ("ImageTarget Topgun").transform;
	
		Vector3 planeCenter = imageTarget.position;
		Vector3 planeNormal = imageTarget.up;

		Plane targetPlane = new Plane (planeNormal, planeCenter);

//		DrawPlane (planeNormal, planeCenter);

		//Gets the ray at position where the screen is touched
		Ray ray = Camera.main.ScreenPointToRay (screenPoint);
		//Gets the position of ray along plane
		float dist = 0.0f;
		//Intersects ray with the plane. Sets dist to distance along the ray where intersects
		targetPlane.Raycast (ray, out dist);
		//Returns point dist along the ray.
		Vector3 planePoint = ray.GetPoint (dist);

//		if (Input.GetMouseButton(LEFT_BUTTON)) {
//			Debug.DrawLine(mousePos, planePoint, Color.red, 20f, false);
//			moveBrush (planePoint);
//		}
		return planePoint;
	}

	private void moveBrush (Vector3 position)
	{
		brush.position = position;
	}

	private void paintAt (Vector3 coordinate)
	{
		moveBrush (coordinate);
		paintAtBrushLocation ();
	}

	private void paintAtBrushLocation ()
	{
		Object obj = Instantiate (brush, brush.position, transform.rotation);
		if (obj is Transform) {
			Transform paintDot = (Transform)obj;
			paintDot.localScale += brushSizeVector;
		}
	}

	public static void DrawPlane (Vector3 normal, Vector3 position)
	{

		Vector3 v3; 

		if (normal.normalized != Vector3.forward)
			v3 = Vector3.Cross (normal, Vector3.forward).normalized * normal.magnitude;
		else
			v3 = Vector3.Cross (normal, Vector3.up).normalized * normal.magnitude;
		;

		v3 *= 100f;
		var corner0 = position + v3;
		var corner2 = position - v3;
		var q = Quaternion.AngleAxis (90.0f, normal);
		v3 = q * v3;
		var corner1 = position + v3;
		var corner3 = position - v3;

		Debug.DrawLine (corner0, corner2, Color.green);
		Debug.DrawLine (corner1, corner3, Color.green);
		Debug.DrawLine (corner0, corner1, Color.green);
		Debug.DrawLine (corner1, corner2, Color.green);
		Debug.DrawLine (corner2, corner3, Color.green);
		Debug.DrawLine (corner3, corner0, Color.green);
		Debug.DrawRay (position, normal, Color.red);
	}


	private Transform[] getImageTargets ()
	{
		GameObject[] imageTargetGameObjects = GameObject.FindGameObjectsWithTag ("ImageTarget");
		Transform[] imageTargetTransforms = new Transform[imageTargetGameObjects.Length];
		for (int i = 0; i < imageTargetTransforms.Length; i++) {
			imageTargetTransforms [i] = imageTargetGameObjects [i].transform;
		}
		return imageTargetTransforms;
	}
}