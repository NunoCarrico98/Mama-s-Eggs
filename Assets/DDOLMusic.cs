using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLMusic : MonoBehaviour {

    void Awake()
    {
        // Find an object of type DDOLMusic
        DDOLMusic bgMusic = FindObjectOfType<DDOLMusic>();
        // If that object's name is "BackgroundMusic" and it isn't this object
        if (bgMusic.name == "BackgroundMusic" && bgMusic != this)
            // Destroy this GameObject
            Destroy(gameObject);

        // Prevent this object from getting destroyed when changing scenes
        DontDestroyOnLoad(gameObject);
    }
}
