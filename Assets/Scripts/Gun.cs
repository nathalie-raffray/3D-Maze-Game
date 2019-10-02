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
    public GameObject player;

    private bool playerInsideMaze;
   // private bool keyDropped;
  //  private int count;

   // private bool bulletShot;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        bulletFired = false;
       // keyDropped = false;
        //bulletShot = false;
    }



    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        playerInsideMaze = ((pos.x <= 174 && pos.x >= 96) && (pos.z <= 179 && pos.z >= 101));
        if (Input.GetMouseButton(0) && !bulletFired && !playerInsideMaze){ //if left click is pressed and bullet is not already fired
            bulletFired = true;

            GameObject bulletObject = Instantiate(bulletPrefab);
            //StartCoroutine(Shoot());
            //Shoot();
            //GameObject bulletObject = Instantiate(bulletPrefab);

            //bulletObject.transform.forward = fpsCam.transform.forward;
            //bulletObject.transform.forward = player.transform.forward;
        }
        if((count == 3)){
            Debug.Log("hey");
            Instantiate(key, new Vector3(135.33f, 50, 193.24f), Quaternion.identity);
            // keyDropped = true;
            count = 4; ///that way we don't instantiate more keys
            //drop key
        }


       
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
