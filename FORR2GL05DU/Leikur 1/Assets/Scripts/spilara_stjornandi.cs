using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spilara_stjornandi : MonoBehaviour {

    public Animator animator;
    public int HP;
    public string drownedMsg;
    public string killedByEnemyMsg;
    public int kills;

    private float sla;
    private bool vatn_buid;
    private string killedMsg;

    // Start is called before the first frame update
    void Start() {   
        animator = this.gameObject.GetComponent<Animator>();
        vatn_buid = false;
        kills = 0;
    }

    // FixedUpdate is called once per frame
    void Update() {

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
            vatn_buid = true;
            killedMsg = drownedMsg;
            showKillScreen();
        }
        if (hlutur.gameObject.CompareTag("óvinur")) {// Þetta passar að það sem er hérna inni gerist bara ef spilarinn snertir óvin
            HP--;
            Debug.Log("Meiddur");
            if (HP == 0) {// Þetta gerist ef leikmaðurinn er búinn að missa öll lífin sín
                Debug.Log("Drepinn");
                killedMsg = killedByEnemyMsg;
                showKillScreen();
            }
        }
        // Debug.Log("Eitthvað snert");
    }
    
    IEnumerator BidaEftirEnda() {
        yield return new WaitForSeconds(3);
        
        // Endurræsa borðið
    }
    void showKillScreen() {
        // Sýna skjá með dauða skilaboðum
        Debug.Log(killedMsg);

        // Stoppa leik

        StartCoroutine(BidaEftirEnda());
    }
}