using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	private Transform player;

	private void Awake()
	{
		player = FindObjectOfType<Player>().transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		SetCameraPosition();
	}

	private void SetCameraPosition()
	{
		transform.position = new Vector3(player.position.x, player.position.y,
			transform.position.z);
	}
}
