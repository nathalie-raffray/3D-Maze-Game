using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{


    public float range = 100f;

    public GameObject bulletPrefab;
    public GameObject capsule;


    public Camera fpsCam;
    private int count;

   // private bool bulletShot;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        //bulletShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            Shoot();
            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = transform.position; 
            //bulletObject.transform.forward = fpsCam.transform.forward;
            bulletObject.transform.forward = capsule.transform.forward;
        }
    }

    void Shoot(){
            //RaycastHit hit;
            //if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){

            ////if(hit.transform.tag == "killme"){
            ////    Destroy(hit.transform.gameObject);
            ////    count++;
            ////}



            ////Debug.Log(hit.transform.tag);
            //}




        }
}
