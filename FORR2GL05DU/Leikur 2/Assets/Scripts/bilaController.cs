using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;// Þetta er fyrir Math.Abs() og Math.Round()
using UnityEngine.UI;// Ég sæki þetta til þess að geta unnið með UI í scriptu
using UnityEngine.SceneManagement;// Hérna sæki ég það sem ég nota til þess að fara úr valmyndinni í leikinn og öfugt

public class bilaController : MonoBehaviour {

    public int mestiHradi;
    public int mestiKraftur;
    public WheelJoint2D[] dekkjaTengingar;// Þetta er breytanlegur listi í UIinu
    public GameObject stigaGeymsla;

    public string stigaTexti;
    public Text stigaTextabox;

    private int fjarlaegdFerdast = 0;
    private bool motorIGangi;
    private Rigidbody2D rb;
    private AudioSource audioData;
    private stigaGeymsla stigaScript;

    // Start is called before the first frame update
    void Start() {
        // JointMotor2D motor = new JointMotor2D {motorSpeed = mestiHradi, maxMotorTorque = mestiKraftur};
        motorIGangi = false;
        rb = GetComponent<Rigidbody2D>();// Þetta finnur rigidbodyinn á gameObjectinum
        audioData = GetComponent<AudioSource>();// Þetta finnur hljóð spilarann á gameObjectinum

        // Finna stigageymslu scriptið
        stigaScript = stigaGeymsla.GetComponent<stigaGeymsla>();

        // Gera texta réttan í byrjun
        stigaTextabox.text = stigaTexti + 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Btn1") != 0 && motorIGangi == false) {// Þetta kveikir á mótornum ef það er ekki kveikt á honum núþegar og það er haldið inni Btn1
            int tel = 0;
            while (tel != dekkjaTengingar.Length) {// Þetta keyrir í gegnum allar dekkja tengingarnar og kveikir á öllum mótorunum
                dekkjaTengingar[tel].useMotor = true;
                tel += 1;
            }
            motorIGangi = true;
            audioData.Play(0);// Þetta spilar bíla hljóðið
        } else if (Input.GetAxis("Btn1") == 0 && motorIGangi == true) {// Þetta slekkur á mótornum ef það er ekki slökkt á honum núþegar og það er sleppt Btn1
            int tel = 0;
            while (tel != dekkjaTengingar.Length) {// Þetta keyrir í gegnum allar dekkja tengingarnar og slekkur á öllum mótorunum
                dekkjaTengingar[tel].useMotor = false;
                tel += 1;
            }
            motorIGangi = false;
            audioData.Stop();// Þetta stoppar bíla hljóðið
        }

        if (Input.GetAxis("Btn1") != 0) {// Þetta bætir við snúningi ef það er haldið niðri Btn1
            rb.AddTorque(-100);
        }

        int fjarlaegdNuna = (int)Math.Floor(Math.Abs(this.gameObject.transform.position.x));// Þetta finnur núverandi fjarlægð frá núll punkti sem er notað sem stig, Abs er notað til þess að fá absolout gildi vegna þess að bílinn keyrir í áttina -x
        if (fjarlaegdNuna > fjarlaegdFerdast) {// Þetta keyrir ef fjarlægðin núna frá byrjunarpunktinum er sú mesta sem hún hefur verið
            fjarlaegdFerdast = fjarlaegdNuna;// Þetta uppfærir mestu fjarlægðina

            // Uppfæra skjá og geymslur
            stigaTextabox.text = stigaTexti + fjarlaegdFerdast;
            stigaScript.stig = fjarlaegdFerdast;
        }
    }
}

