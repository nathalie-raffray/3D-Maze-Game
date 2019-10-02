using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    public float speed = 30.0F;

    private bool isJumping;

    public GameObject pathTilePrefab;

    //use AnimationCurve to specify how we jump
    public AnimationCurve jumpFallOff;
    public float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    //use CharacterController to customize gravity for jumping, as well as to use the isGrounded attribute.
    private CharacterController charController;
    private bool playerInsideMaze;
    private bool win;

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

        win = MazeGenerator2.win;
         if (!win) //when the player won, no action should be possible
        {
            float vertInput = Input.GetAxis("Vertical") * speed;
            float horizInput = Input.GetAxis("Horizontal") * speed;

            Vector3 forwardMovement = transform.forward * vertInput;
            Vector3 rightMovement = transform.right * horizInput;

            charController.SimpleMove(forwardMovement + rightMovement);


            JumpInput();
        }

       

        //if escape is pressed the cursor will become visible again
        if (Input.GetKeyDown("escape")) Cursor.lockState = CursorLockMode.None;

    }
    private bool goodToGo;

    private void JumpInput()
    {
        //if (space is pressed && the character is not already jumping)
        //this is to prevent the character from jumping again while in the air
        Vector3 pos = transform.position;
        playerInsideMaze = ((pos.x <= 174 && pos.x >= 96) && (pos.z <= 179 && pos.z >= 101));
        goodToGo = true;


        if (playerInsideMaze)
        {
            if (charController.velocity.x != 0 && charController.velocity.z != 0) //this is so the player cannot jump and move at the same time inside the maze
            {
                goodToGo = false;
            }
        }
        if (Input.GetKeyDown(jumpKey) && !isJumping && goodToGo)
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
        

        isJumping = false; //the jump has finished, player is grounded. 

    }



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("collided");
        if (hit.gameObject.tag == "key")
        {
            Destroy(hit.gameObject);
            //open the fucking maze
            GameObject entry = GameObject.Find("entry");
            Destroy(entry);
            Instantiate(pathTilePrefab, new Vector3(135, 0, 170), Quaternion.identity); //the first tile when you enter is colored.
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MazeTile"){ //this is to check if we change tiles in the maze
            MazeGenerator2.hitTile = other.gameObject;
        }
    }


}


