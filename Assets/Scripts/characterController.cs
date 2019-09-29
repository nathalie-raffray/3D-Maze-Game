using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    public float speed = 100.0F;
    //public float jumpSpeed = 400.0F;

    private bool isJumping;

    //use AnimationCurve to specify how we jump
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    //use CharacterController to customize gravity for jumping, as well as to use the isGrounded attribute.
    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {

        //hide cursor when playing
        Cursor.lockState = CursorLockMode.Locked;

        charController = GetComponent<CharacterController>();
        isJumping = false;

    }

    // Update is called once per frame
    void Update()
    {

        float vertInput = Input.GetAxis("Vertical") * speed;
        float horizInput = Input.GetAxis("Horizontal") * speed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        //translation *= Time.deltaTime;
        //straffe *= Time.deltaTime;

        //transform.Translate(straffe, 0, translation); //why this weird behavior when I use this, and jump?

        JumpInput();

        //if escape is pressed the cursor will become visible again
        if (Input.GetKeyDown("escape")) Cursor.lockState = CursorLockMode.None;
     


    }

    private void JumpInput(){
        //if (space is pressed && the character is not already jumping)
        //this is to prevent the character from jumping again while in the air
        if(Input.GetKeyDown(jumpKey) && !isJumping){
            isJumping = true;
            StartCoroutine(JumpEvent());    //use Coroutine so that if space is pressed, the character will jump along multiple frames, instead of only the initial one.
        }
    }

    private IEnumerator JumpEvent(){

        float timeInAir = 0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);  //this will read the values along the animation curve, as time increases. 

            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);

            timeInAir += Time.deltaTime;

            yield return null;
        } while (!charController.isGrounded);

        //&& charController.collisionFlags != CollisionFlags.Above
        //charController.collisionFlags != CollisionFlags.Above will make sure that if the character hits a collision object above, it will stop going up
        //and instead fall back down. This won't really be useful for this assignment, but I add it nonetheless as it is generally important/useful. 

        isJumping = false; //the jump has finished, player is grounded. 

    }

    // private float distToGround;
    // get the distance to ground
    //distToGround = GetComponent<Collider>().bounds.extents.y;
    //private bool isGrounded(){
    //return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    //}
}


