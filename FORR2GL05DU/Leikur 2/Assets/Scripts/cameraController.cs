using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    public GameObject target;

    private Vector3 offset;
    private AudioSource audioData;

    // Start is called before the first frame update
    void Start() {
        offset = transform.position - target.transform.position;

        // Byrja tónlist
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = target.transform.position + offset;
    }
}
