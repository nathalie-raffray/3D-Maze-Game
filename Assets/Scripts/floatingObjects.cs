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
        Instantiate(zombie, new Vector3(135, 45, 300), Quaternion.identity);
        Instantiate(zombie, new Vector3(115, 40, 250), Quaternion.identity);
        Instantiate(zombie, new Vector3(155, 40, 250), Quaternion.identity);
    }

}
