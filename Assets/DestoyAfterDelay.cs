using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyAfterDelay : MonoBehaviour {

    [SerializeField] public float TimeToDie { get; set; }

    private void Start()
    {
        //TimeToDie = 2;
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(TimeToDie);
        Destroy(gameObject);
    }
}
