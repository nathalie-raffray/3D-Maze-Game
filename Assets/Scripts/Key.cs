using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public float rotationSpeed = 250.0f;
   
    void Update()
    {

        if(transform.position.y >= 5)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime); //make key rotate as it falls down
        }

    }

}


