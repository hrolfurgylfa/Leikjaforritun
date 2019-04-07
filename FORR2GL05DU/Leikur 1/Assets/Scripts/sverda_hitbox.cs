using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// Ég sæki þetta til þess að geta unnið með UI í scriptu

public class sverda_hitbox : MonoBehaviour
{
    public GameObject Spilari;

    private Text drapTextabox;
    private string drapTexti;

    void Start() {
        drapTextabox = Spilari.GetComponent<spilara_stjornandi>().drapTextabox;// Þetta sækir dráptextaboxið frá spilaranum
        drapTexti = Spilari.GetComponent<spilara_stjornandi>().drapTexti;// Þetta sækir dráptextann frá spilaranum
    }

    void OnTriggerEnter(Collider hlutur) {
        if (hlutur.gameObject.CompareTag("sverd")) {// Þetta gerist ef sverðið hittir óvin
            Spilari.GetComponent<spilara_stjornandi>().drap += 1;// Þetta hækkar dráp teljarann hjá spilaranum
            drapTextabox.text = drapTexti + Spilari.GetComponent<spilara_stjornandi>().drap;// Þetta uppfærir dráp textaboxið
            Destroy(transform.parent.gameObject);// Þetta eyðir óvininum sem var hittur með sverði
        }
    }
}
