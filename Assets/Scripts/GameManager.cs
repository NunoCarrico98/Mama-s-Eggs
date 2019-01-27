using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	[SerializeField] GameObject virtualCam;

	// Use this for initialization
	void Awake () 
	{
		virtualCam.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
