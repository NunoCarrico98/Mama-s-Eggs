using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleManager : MonoBehaviour 
{
	private Camera cam;

	private void Awake()
	{
		cam = GameObject.Find("VirtualCamera").GetComponent<Camera>();
	}

	public float GetAngle()
	{
		//Get the Screen positions of the object
		Vector3 positionOnScreen = cam.WorldToViewportPoint(transform.position);

		//Get the Screen position of the mouse
		Vector3 mouseOnScreen = cam.ScreenToViewportPoint(Input.mousePosition);

		//Get the angle between the points
		return AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
	}

	private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
	{
		float xA = Mathf.Abs(a.x);
		float xB = Mathf.Abs(b.x);
		float yA = Mathf.Abs(a.y);
		float yB = Mathf.Abs(b.y);
		return Mathf.Atan2(yA - yB, xA - xB) * Mathf.Rad2Deg;
	}
}
