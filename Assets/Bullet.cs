using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10.0f;
    private int count;


    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //make bullet move
        transform.position += transform.forward * speed * Time.deltaTime; 

        if(count == 3){
            
                //drop key
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "killme")
        {
            //Debug.Log("entered");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            count++;
        }


    }
}
