using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    //public float downSpeed = 5.0f;
    public float rotationSpeed = 250.0f;
    //private CharacterController cc;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    cc = GetComponent<CharacterController>();
    //}

    //// Update is called once per frame
    void Update()
    {

        if(transform.position.y >= 5)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        //(transform.position.y)
        //if (!cc.isGrounded)
        //{
        //    transform.position += Vector3.down * downSpeed * Time.deltaTime;
        //    transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        //}

    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("FUCKYEAH");
    //        GameObject entry = GameObject.Find("entry");
    //        Destroy(entry);
    //        Destroy(this.gameObject);
    //        //open the fucking maze
    //        //make the maze open at one point specified by root which is at index (7,4)
    //        //if key drops

    //    }
    //}
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Debug.Log("FUCKYEAH");
    //        GameObject entry = GameObject.Find("entry");
    //        Destroy(entry);
    //        Destroy(this.gameObject);
    //        //open the fucking maze
    //        //make the maze open at one point specified by root which is at index (7,4)
    //        //if key drops

    //    }
    //}
}


