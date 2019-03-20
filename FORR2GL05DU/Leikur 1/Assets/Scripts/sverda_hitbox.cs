using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sverda_hitbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider hlutur) {
        if (hlutur.gameObject.CompareTag("sverd")) {
            Debug.Log("Óvinur drepinn");
        }
    }
}
