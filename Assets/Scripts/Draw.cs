using UnityEngine;
using System.Collections;
public class Draw : MonoBehaviour {

	private const int LEFT_BUTTON = 0;

	public Transform brush;
	public float brushSize = 100f;

	public Transform imageTarget1;
	public Transform imageTarget2;

	private Vector3 brushSizeVector;

	void Start () {
		brushSizeVector = new Vector3 (brushSize, brushSize, brushSize);
	}

	// Update is called once per frame
	void Update () {
		paintByMouse (imageTarget1);
		paintByTouch (imageTarget1);

		paintByMouse (imageTarget2);
		paintByTouch (imageTarget2);
	
	}

	private void paintByTouch(Transform imageTarget) {
		foreach (Touch touch in Input.touches) {
			Vector3 touchIntersection = getPlaneIntersectionForScreenPoint (touch.position, imageTarget);
			moveBrush (touchIntersection);
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
				paintAtBrushLocation ();
			}
		}
	}

	private void paintByMouse(Transform imageTarget) {
		Vector3 mouseIntersection;
		mouseIntersection = getPlaneIntersectionForScreenPoint (Input.mousePosition, imageTarget);

		moveBrush(mouseIntersection);
		paintOnClick ();
	}

	private Vector3 getPlaneIntersectionForScreenPoint(Vector3 screenPoint, Transform imageTarget) {

//		Transform imageTarget = GameObject.Find ("ImageTarget Topgun").transform;
	
		Vector3 planeCenter = imageTarget.position;
		Vector3 planeNormal = imageTarget.up;

		Plane targetPlane = new Plane(planeNormal, planeCenter);

//		DrawPlane (planeNormal, planeCenter);

		//Gets the ray at position where the screen is touched
		Ray ray = Camera.main.ScreenPointToRay(screenPoint);
		//Gets the position of ray along plane
		float dist = 0.0f;
		//Intersects ray with the plane. Sets dist to distance along the ray where intersects
		targetPlane.Raycast(ray, out dist);
		//Returns point dist along the ray.
		Vector3 planePoint = ray.GetPoint(dist);

//		if (Input.GetMouseButton(LEFT_BUTTON)) {
//			Debug.DrawLine(mousePos, planePoint, Color.red, 20f, false);
//			moveBrush (planePoint);
//		}
		return planePoint;
	}

	private void moveBrush(Vector3 position) {
		brush.position = position;
	}


	// Paints by adding lots of object copies of the brush
	private void paintOnClick() {
		if (Input.GetMouseButton (0)) {
			paintAtBrushLocation ();	
		}
	}

	private void paintAtBrushLocation() {
		Object obj = Instantiate (brush, brush.position, transform.rotation);
		if (obj is Transform) {
			Transform paintDot = (Transform)obj;
			paintDot.localScale += brushSizeVector;
		}
	}



//	private void dragByTouch() {
//		Plane targetPlane = new Plane(transform.up, transform.position);
//		//message.text = "";
//		foreach (Touch touch in Input.touches) {
//			//Gets the ray at position where the screen is touched
//			Ray ray = Camera.main.ScreenPointToRay(touch.position);
//			//Gets the position of ray along plane
//			float dist = 0.0f;
//			//Intersects ray with the plane. Sets dist to distance along the ray where intersects
//			targetPlane.Raycast(ray, out dist);
//			//Returns point dist along the ray.
//			Vector3 planePoint = ray.GetPoint(dist);
//			//Debug.Log("Point=" + planePoint);
//			//True if finger touch began. If ray intersects collider, set pickedObject to transform of collider object
//			if (touch.phase == TouchPhase.Began) {
//				//Struct used to get info back from a raycast
//				RaycastHit hit = new RaycastHit();
//				if (Physics.Raycast(ray, out hit, 1000)) { //True when Ray intersects colider. If true, hit contains additional info about where collider was hit
//					print("Ray hit target");
//					pickedObject = hit.transform;
//					lastPlanePoint = planePoint;
//				} else {
//					pickedObject = null;
//				}
//
//				// Draw ray
//				Debug.DrawLine(touch.position, Vector3.zero, Color.red, 2f);
//
//
//				//Move Object when finger moves after object selected.
//			} else if (touch.phase == TouchPhase.Moved) {
//				if (pickedObject != null) {
//					print("Moving target");
//					pickedObject.position += planePoint - lastPlanePoint;
//					lastPlanePoint = planePoint;
//				}
//				//Set pickedObject to null after touch ends.
//			} else if (touch.phase == TouchPhase.Ended) {
//				pickedObject = null;
//			}
//		}
//	}

	public static void DrawPlane(Vector3 normal, Vector3 position) {

		Vector3 v3; 

		if (normal.normalized != Vector3.forward)
			v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
		else
			v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude;;

		v3 *= 100f;
		var corner0 = position + v3;
		var corner2 = position - v3;
		var q = Quaternion.AngleAxis(90.0f, normal);
		v3 = q * v3;
		var corner1 = position + v3;
		var corner3 = position - v3;

		Debug.DrawLine(corner0, corner2, Color.green);
		Debug.DrawLine(corner1, corner3, Color.green);
		Debug.DrawLine(corner0, corner1, Color.green);
		Debug.DrawLine(corner1, corner2, Color.green);
		Debug.DrawLine(corner2, corner3, Color.green);
		Debug.DrawLine(corner3, corner0, Color.green);
		Debug.DrawRay(position, normal, Color.red);
	}
}