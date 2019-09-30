using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{


    public float range = 100f;

    public GameObject bulletPrefab;
    public GameObject key;
   

    public bool bulletFired;

    public int count;

    public Camera fpsCam;
    public GameObject capsule;
    private bool keyDropped;
  //  private int count;

   // private bool bulletShot;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        bulletFired = false;
        keyDropped = false;
        //bulletShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && !bulletFired){ //if left click is pressed and bullet is not already fired
            bulletFired = true;

            GameObject bulletObject = Instantiate(bulletPrefab);
            //StartCoroutine(Shoot());
            //Shoot();
            //GameObject bulletObject = Instantiate(bulletPrefab);

            //bulletObject.transform.forward = fpsCam.transform.forward;
            //bulletObject.transform.forward = capsule.transform.forward;
        }
        if((count == 3) && !keyDropped){
            Debug.Log("hey");
            Instantiate(key, new Vector3(150, 30, 250), Quaternion.identity);
            keyDropped = true;

            //drop key
        }


        //make the maze open at one point specified by root which is at index (7,4)
        //if key drops
        //GameObject entry = GameObject.Find("entry");
        //Destroy(entry);
    }

    private IEnumerator Shoot(){
        bulletFired = true;
        GameObject bulletObject = Instantiate(bulletPrefab);
            

        yield return null;
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
