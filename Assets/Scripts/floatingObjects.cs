using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingObjects : MonoBehaviour
{

    public GameObject zombie;
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        //Creating the three floating Objects
        Instantiate(zombie, new Vector3(135, 35, 155), Quaternion.identity);
        Instantiate(cube, new Vector3(115, 30, 165), Quaternion.identity);
        Instantiate(cube, new Vector3(155, 30, 155), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
