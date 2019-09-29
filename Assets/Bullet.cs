﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10.0f;
    private Gun gun;
    public GameObject capsule;

    public float lifeDuration = 3.0f;
    private float lifeTimer;
   
    // Start is called before the first frame update
    void Start()
    {
  
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        transform.position = GameObject.Find("Gun").transform.position; 
        transform.forward = GameObject.Find("Main Camera").transform.forward;
        //transform.forward = capsule.transform.forward;
        lifeTimer = lifeDuration;
   
        //forward = GameObject.Find("Main Camera").transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
   
        //GameObject.Find("Capsule").transform.forward  + new Vector3(0, GameObject.Find("Capsule").transform.position.y, 0)
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(forward * speed);

        transform.position += transform.forward * speed * Time.deltaTime;
        //transform.position += ( GameObject.Find("Main Camera").transform.forward  ) * speed * Time.deltaTime;


        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0.0f){
            gun.bulletFired = false;
            Destroy(gameObject);

        }


       
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "killme")
        {
            gun.bulletFired = false;
            gun.count++;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }else if(other.tag == "terrain"){
            gun.bulletFired = false;
            Destroy(this.gameObject);
        }
        gun.bulletFired = false;





    }
}
