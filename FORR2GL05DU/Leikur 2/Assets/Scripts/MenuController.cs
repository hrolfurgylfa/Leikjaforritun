using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// Þetta sæki ég til þess að geta skipt um senu
using UnityEngine.UI;// Ég sæki þetta til þess að geta unnið með UI í scriptu

public class MenuController : MonoBehaviour
{
    public void StartGame() {// Þetta er keyrt af takkanum í Menu senuni
        GameObject StigaGeymsla = GameObject.Find("StigaGeymsla");// Þetta nær í stiga geymsluna ef hún er til
        Destroy(StigaGeymsla);// Þetta eyðir stigageymsluni svo að það sé hægt að búa til nýja
        SceneManager.LoadScene(1);// Þetta skiptir um senu og skiptir yfir í leikinn
    }

    public Text stigSidastTextabox;// Hérna tek ég inn textabox
    public string stigSidastTexti;// Hérna tek ég inn texta

    void Start() {
        GameObject StigaGeymsla = GameObject.Find("StigaGeymsla");// Hérna reyni ég að finna gameObjectinn StigaGeymsla
        if (StigaGeymsla != null) {// Þetta keyrir ef það er búið að spila allavegana eynu sinni, annars haldast leiðbeiningarnar um hvernig á að spila og sagan
            stigSidastTextabox.text = stigSidastTexti + StigaGeymsla.GetComponent<stigaGeymsla>().stig;// Hérna breyti ég textanum í stigSidastTextabox í hversu mörg stig spilarinn fékk síðast
        }
    }    
}
