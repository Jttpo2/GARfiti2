using UnityEngine;

public class ColorHuePicker : MonoBehaviour
{
	void SetColor (HSBColor color)
	{
		SendMessage ("SetDragPoint", new Vector3 (color.h, 0, 0));
	}

	void OnDrag (Vector3 point)
	{
//		Transform background = transform.FindChild ("Background");

//		transform.parent.BroadcastMessage ("SetHue", background.localScale.x * point.y);

		transform.parent.BroadcastMessage ("SetHue", point.x);
	}
}
