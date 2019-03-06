using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attack : MonoBehaviour
{
    public Animator animator;
    float sla;

    // Start is called before the first frame update
    void Start()
    {   
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        sla = Input.GetAxis ("Fire1");
        animator.SetFloat("sla",sla);
    }
}
