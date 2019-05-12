using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// Þetta sæki ég til þess að geta skipt um senu
using UnityEngine.UI;// Ég sæki þetta til þess að geta unnið með UI í scriptu

public class MenuController : MonoBehaviour
{
    public void StartGame() {
        GameObject StigaGeymsla = GameObject.Find("StigaGeymsla");
        Destroy(StigaGeymsla);
        SceneManager.LoadScene(1);// Þetta skiptir um senu og skiptir yfir í leikinn
    }

    public Text stigSidastTextabox;
    public string stigSidastTexti;

    void Start() {
        GameObject StigaGeymsla = GameObject.Find("StigaGeymsla");
        if (StigaGeymsla != null) {
            stigSidastTextabox.text = stigSidastTexti + StigaGeymsla.GetComponent<stigaGeymsla>().stig;
        }
    }    
}
