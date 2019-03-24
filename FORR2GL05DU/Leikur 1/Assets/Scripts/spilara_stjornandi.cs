using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// Ég sæki þetta til þess að geta unnið með UI í scriptu
using UnityEngine.SceneManagement;// Hérna sæki ég það sem ég nota til þess að restarta leiknum

public class spilara_stjornandi : MonoBehaviour {

    public Animator animator;
    public int HP;
    public string drownedMsg;
    public string killedByEnemyMsg;
    public int drap;
    public Text lifTextabox;
    public Text drapTextabox;
    public Text timiTextabox;
    public string lifTexti;
    public string drapTexti;
    public string timiTexti;
    public Canvas adalCanvas;
    public Canvas daudaCanvas;
    public Text daudaTextabox;
    public Text daudaDrapTextabox;
    public Text daudaTimaTextabox;

    private float sla;
    private bool vatn_buid;
    private string killedMsg;

    // Start is called before the first frame update
    void Start() {   
        animator = this.gameObject.GetComponent<Animator>();// Þetta sækir animatorinn á spilaranum svo að ég geti notað hann seinna
        vatn_buid = false;
        drap = 0;

        // Gera texta réttan í byrjun
        lifTextabox.text = lifTexti + HP;
        drapTextabox.text = drapTexti + drap;
        timiTextabox.text = timiTexti + 0;
    }

    // FixedUpdate is called once per frame
    void Update() {
        if (Input.GetButtonDown("Restart") == true) {// Þetta gerist ef spilarinn ýtir á r
            Time.timeScale = 1.0f;// Þetta setir tímann aftur í gang svo að spilarinn sé ekki frosinn þegar hann byrjar aftur
            SceneManager.LoadScene (SceneManager.GetActiveScene().name);// Þetta byrjar að fá nafnið á levelinu sem er í gangi núna og loadar því svo svo að levelið endurræsist
        }
        // Uppfæra skeiðklukku
        timiTextabox.text = timiTexti + System.Math.Round(Time.timeSinceLevelLoad,2).ToString();// Þetta námundar tímann síðan levelið byrjaði að tveimur aukastöfum, breytir því í streng og bætir því svo við á skjáinn

        // Sverða stjórnun
        sla = Input.GetAxis ("Fire1");
        if (sla == 1) {// Þetta tékkar hvort að spilarinn sé að ýta á takkann Fire1
            animator.Play("Sla_med_sverdi");// Þetta skiftir strax yfir í animationið sama hvaða animation er að keyra
        }
        // Þessi aðferð breytti bara um animation á u.þ.b. 1s fresti svo að þetta virkar ekki fyrir attack animation
        // animator.SetFloat("sla",sla);
    }
    
    void OnTriggerEnter(Collider hlutur) {
        if (hlutur.gameObject.CompareTag("vatn") && vatn_buid == false) {// Þetta passar að það sem er hérna inni gerist bara ef hluturinn sem spilarinn fer inní er vatn og að þetta sé ekki búið að keyra
            vatn_buid = true;// Þetta passar að þetta muni ekki keyra tvisvar
            killedMsg = drownedMsg;// Þetta breytir dauðaskilaboðunum sem munu koma á lokaskjánum í skilaboðin sem segja að spilarinn hafi drukknað
            showKillScreen();
        }
        if (hlutur.gameObject.CompareTag("óvinur")) {// Þetta passar að það sem er hérna inni gerist bara ef spilarinn snertir óvin
            HP--;
            lifTextabox.text = lifTexti + HP;// Þetta uppfærir textann á skjánum
            if (HP == 0) {// Þetta gerist ef leikmaðurinn er búinn að missa öll lífin sín
                killedMsg = killedByEnemyMsg;// Þetta breytir dauðaskilaboðunum sem munu koma á lokaskjánum í skilaboðin sem segja drepinn af óvini
                showKillScreen();
            }
        }
    }

    void showKillScreen() {// Þetta fall sýnir dauða skjáinn og stoppar leikinn
        // Sýna skjá með dauða skilaboðum
        adalCanvas.gameObject.SetActive(false);// Þetta slekkur á venjulega UIinu
        daudaCanvas.gameObject.SetActive(true);// Þetta kveikir á dauða UIinu
        daudaTextabox.text = killedMsg;// Þetta sýnir dauðaskilaboðin
        // Þetta er til þess að sýna textaboxið með nýjustu upplýsingum eftir að maður deyr
        daudaDrapTextabox.text = drapTexti + drap;
        daudaTimaTextabox.text = timiTexti + System.Math.Round(Time.timeSinceLevelLoad,2).ToString();

        // Stoppa leikinn
        Time.timeScale = 0.0f;// Þetta stoppar allt sem er að gerast í leiknum með því að láta leikinn keyra á 0 hraða
    }
}