using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLock : MonoBehaviour
{
    //keep track of how much movement the camera has made, so we can add it to the controller
    Vector2 mouseLook;

    //smoothV helps to smooth the movement of the mouse
    Vector2 smoothV;

    public float sensitivity = 0.5f;
    public float smoothing = 1.0f;

    //this will point back to the parent of the camera (the capsule).
    GameObject character;


    void Start()
    {
       character = this.transform.parent.gameObject;
    }


    void Update()
    {

        //get change of mouse movement since last update. md for "mouse delta"
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //scale md by the sensitivity and smoothing values
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        //smoothV is used to smoothe the movement by linearly interpolating between a lower value (smoothV) and md
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        //mouseLook is adding up all our movements so we can apply it to the character
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        //only moving the camera by x will affect the frame of reference of the character (and hence update new axes for translation)
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

    }
}
