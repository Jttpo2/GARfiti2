using UnityEngine;
using System.Collections;
public class DragObject : MonoBehaviour {
	public GUIText message = null;
	private Transform pickedObject = null;
	private Vector3 lastPlanePoint;

	private const int LEFT_BUTTON = 0;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		dragByMouse ();
//		dragByTouch ();
	}

	private void dragByMouse() {
//		GameObject sphere = GameObject.Find ("Sphere");
//		Vector3 mousePos = Input.mousePosition;
////		mousePos.x = 0;
//		sphere.transform.position = mousePos;

		Vector3 mousePos = Input.mousePosition;

		Transform imageTarget = GameObject.Find ("ImageTarget").transform;
		Transform camera = transform;

		Vector3 camToPlane =  imageTarget.position - camera.position;
//		Vector3 camToPlane =  camera.position - imageTarget.position;
		Vector3 planeNormal = camToPlane.normalized;
		Vector3 planeCenter = imageTarget.position;


//		Vector3 planeNormal = transform.up;
//		Vector3 planeCenter = transform.position;
		Plane targetPlane = new Plane(planeNormal, planeCenter);

		DrawPlane (planeNormal, planeCenter);
			
		//Gets the ray at position where the screen is touched
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			//Gets the position of ray along plane
			float dist = 0.0f;
			//Intersects ray with the plane. Sets dist to distance along the ray where intersects
			targetPlane.Raycast(ray, out dist);
			//Returns point dist along the ray.
			Vector3 planePoint = ray.GetPoint(dist);
	
		if (Input.GetMouseButton(LEFT_BUTTON)) {
			Debug.DrawLine(mousePos, planePoint, Color.red, 20f, false);
			moveBlueTo (planePoint);
		}

	}

	private void moveBlueTo(Vector3 position) {
		Transform blue = GameObject.Find ("Blue").transform;
		blue.position = position;
	}


	private void dragByTouch() {
		Plane targetPlane = new Plane(transform.up, transform.position);
		//message.text = "";
		foreach (Touch touch in Input.touches) {
			//Gets the ray at position where the screen is touched
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			//Gets the position of ray along plane
			float dist = 0.0f;
			//Intersects ray with the plane. Sets dist to distance along the ray where intersects
			targetPlane.Raycast(ray, out dist);
			//Returns point dist along the ray.
			Vector3 planePoint = ray.GetPoint(dist);
			//Debug.Log("Point=" + planePoint);
			//True if finger touch began. If ray intersects collider, set pickedObject to transform of collider object
			if (touch.phase == TouchPhase.Began) {
				//Struct used to get info back from a raycast
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit, 1000)) { //True when Ray intersects colider. If true, hit contains additional info about where collider was hit
					print("Ray hit target");
					pickedObject = hit.transform;
					lastPlanePoint = planePoint;
				} else {
					pickedObject = null;
				}

				// Draw ray
				Debug.DrawLine(touch.position, Vector3.zero, Color.red, 2f);


				//Move Object when finger moves after object selected.
			} else if (touch.phase == TouchPhase.Moved) {
				if (pickedObject != null) {
					print("Moving target");
					pickedObject.position += planePoint - lastPlanePoint;
					lastPlanePoint = planePoint;
				}
				//Set pickedObject to null after touch ends.
			} else if (touch.phase == TouchPhase.Ended) {
				pickedObject = null;
			}
		}
	}

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