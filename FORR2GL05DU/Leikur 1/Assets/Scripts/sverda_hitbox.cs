using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sverda_hitbox : MonoBehaviour
{
    public GameObject Spilari;

    // Start is called before the first frame update
    void Start() {
        spilara_stjornandi SpilaraStjornandi = Spilari.GetComponent<spilara_stjornandi>();
    }

    void OnTriggerEnter(Collider hlutur) {
        if (hlutur.gameObject.CompareTag("sverd")) {
            Debug.Log("Óvinur drepinn");
            SpilaraStjornandi.kills += 1;
            Destroy(transform.parent.gameObject);
        }
    }
}
