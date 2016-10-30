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
		Plane targetPlane = new Plane(transform.up, transform.position);
		//message.text = "";
//		foreach (Touch touch in Input.touches) {
			
		//Gets the ray at position where the screen is touched
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			//Gets the position of ray along plane
			float dist = 0.0f;
			//Intersects ray with the plane. Sets dist to distance along the ray where intersects
			targetPlane.Raycast(ray, out dist);
			//Returns point dist along the ray.
			Vector3 planePoint = ray.GetPoint(dist);
			//Debug.Log("Point=" + planePoint);
			//True if finger touch began. If ray intersects collider, set pickedObject to transform of collider object
		if (Input.GetMouseButtonDown(LEFT_BUTTON)) {
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
//				Debug.DrawLine(touch.position, Vector3.zero, Color.red, 2f);


				//Move Object when finger moves after object selected.
//		} else 
		} else if (Input.GetMouseButton(LEFT_BUTTON)) {
				if (pickedObject != null) {
				
					
				pickedObject.position += planePoint - lastPlanePoint;
					lastPlanePoint = planePoint;
				}
			print("Moving target " + pickedObject.position);
			Debug.DrawLine(mousePos, planePoint, Color.red, 2f, false);

				//Set pickedObject to null after touch ends.
		} else if (Input.GetMouseButtonUp(LEFT_BUTTON) ) {
				print("Letting target go");
				pickedObject = null;
		}

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
}