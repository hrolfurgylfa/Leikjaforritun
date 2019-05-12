using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;// Þetta er fyrir Math.Abs() og Math.Round()
using UnityEngine.UI;// Ég sæki þetta til þess að geta unnið með UI í scriptu
using UnityEngine.SceneManagement;// Hérna sæki ég það sem ég nota til þess að fara úr valmyndinni í leikinn og öfugt

public class bilaController : MonoBehaviour {

    public int mestiHradi;
    public int mestiKraftur;
    public WheelJoint2D[] dekkjaTengingar;
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
        rb = GetComponent<Rigidbody2D>();
        audioData = GetComponent<AudioSource>();

        // Finna stigageymslu scriptið
        stigaScript = stigaGeymsla.GetComponent<stigaGeymsla>();

        // Gera texta réttan í byrjun
        stigaTextabox.text = stigaTexti + 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Btn1") != 0 && motorIGangi == false) {
            Debug.Log("Motor settur í gang");
            int tel = 0;
            while (tel != dekkjaTengingar.Length) {
                dekkjaTengingar[tel].useMotor = true;
                tel += 1;
            }
            motorIGangi = true;
            audioData.Play(0);
        } else if (Input.GetAxis("Btn1") == 0 && motorIGangi == true) {
            Debug.Log("Slökkt á motor");
            int tel = 0;
            while (tel != dekkjaTengingar.Length) {
                dekkjaTengingar[tel].useMotor = false;
                tel += 1;
            }
            motorIGangi = false;
            audioData.Stop();
        }

        if (Input.GetAxis("Btn1") != 0) {
            rb.AddTorque(-100);
        }

        int fjarlaegdNuna = (int)Math.Floor(Math.Abs(this.gameObject.transform.position.x));
        if (fjarlaegdNuna > fjarlaegdFerdast) {
            fjarlaegdFerdast = fjarlaegdNuna;

            // Uppfæra skjá og geymslur
            stigaTextabox.text = stigaTexti + fjarlaegdFerdast;
            stigaScript.stig = fjarlaegdFerdast;
        }
    }
}

