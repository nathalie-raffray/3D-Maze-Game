using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    public float speed = 30.0F;
    //public float jumpSpeed = 400.0F;

    private bool isJumping;

    public GameObject pathTilePrefab;

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


        JumpInput();

        //if escape is pressed the cursor will become visible again
        if (Input.GetKeyDown("escape")) Cursor.lockState = CursorLockMode.None;

        //135.33f, 50, 193.24f
        //DELEEET LATER
        //if(transform.position.x <=138 && transform.position.x >= 132 && transform.position.z <= 196 && transform.position.x >= 190)
        //{
        //    GameObject entry = GameObject.Find("entry");
        //    Destroy(entry);
        //    Instantiate(pathTilePrefab, new Vector3(135, 0, 170), Quaternion.identity); //the first tile when you enter is colored.
        //}



    }

    private void JumpInput()
    {
        //if (space is pressed && the character is not already jumping)
        //this is to prevent the character from jumping again while in the air
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());    //use Coroutine so that if space is pressed, the character will jump along multiple frames, instead of only the initial one.
        }
    }

    private IEnumerator JumpEvent()
    {

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

    //void onTriggerEnter(Collider other)
    //{
    //    Debug.Log("collided");
    //    if(other.tag == "key")
    //    {
    //        Destroy(other);
    //        //open the fucking maze
    //        //make the maze open at one point specified by root which is at index (7,4)
    //        //if key drops
    //        GameObject entry = GameObject.Find("entry");
    //        Destroy(entry);
    //        Instantiate(pathTilePrefab, new Vector3(135, 0, 170), Quaternion.identity); //the first tile when you enter is colored.
    //    }
    //}



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("collided");
        if (hit.gameObject.tag == "key")
        {
            Destroy(hit.gameObject);
            //open the fucking maze
            //make the maze open at one point specified by root which is at index (7,4)
            //if key drops
            GameObject entry = GameObject.Find("entry");
            Destroy(entry);
            Instantiate(pathTilePrefab, new Vector3(135, 0, 170), Quaternion.identity); //the first tile when you enter is colored.
        }

        //if(hit.gameObject.tag == "MazeTile")
        //{
        //    MazeGenerator2.hitTile = hit.gameObject;
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MazeTile"){
            MazeGenerator2.hitTile = other.gameObject;
        }
    }
}


