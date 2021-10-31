using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {
    public GameObject Commodoor;
    void Start() {
        StartCoroutine(End());
    }

    IEnumerator End() {
	    yield return true;
    }

}
