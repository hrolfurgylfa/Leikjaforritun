using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bilaController : MonoBehaviour {

    public int mestiHradi;
    public int mestiKraftur;
    public WheelJoint2D[] dekkjaTengingar;
    private bool motorIGangi;

    // Start is called before the first frame update
    void Start() {
        JointMotor2D motor = new JointMotor2D {motorSpeed = mestiHradi, maxMotorTorque = mestiKraftur};
        // int tel = 0;
        // while (tel != dekkjaTengingar.Length) {
        //     dekkjaTengingar[tel].motor = motor;
        //     Debug.Log("tel: "+tel);
        //     Debug.Log("dekkjaTengingar.Length: "+dekkjaTengingar.Length);
        //     tel++;

        // }

        motorIGangi = false;
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
        } else if (Input.GetAxis("Btn1") == 0 && motorIGangi == true) {
            Debug.Log("Slökkt á motor");
            int tel = 0;
            while (tel != dekkjaTengingar.Length) {
                dekkjaTengingar[tel].useMotor = false;
                tel += 1;
            }
            motorIGangi = false;
        }
    }
}
