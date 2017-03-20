using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {



    //public CameraTracking cam;//CameraTracking class referencing to turn off camera tarcking during events
    public AR_SwypeController camControls;

    public Rigidbody rb;//rigidBody class to add force/move/jump

    //player variables for movement
    public bool isController;//is this character in your control

    public Vector3 initPos;//initial player position

    public bool drawn;//is the player hidden or visible


    //movement
    public float fwdSpd = 0, sidSpd = 0 , mvspd;// movement variables mvSpd controls the speed the other 2 change depending which key you press



    public float jumpForce,doubleJumpedAt;//how high is jump. what time did you double jump

     public bool isDoubleJumping ;//is player double jumping currently
     public bool isGrounded ;//is player on floor or on object
     public bool doubleJump ; //did player double jump yet
     public bool  canDblJump;//can player double jump
     public bool  isJumping;//is player performing first jump



    public bool  canMove;//can player move currently




    // Use this for initialization
    void Awake () {
        camControls = this.GetComponent<AR_SwypeController>();
        rb = this.GetComponent<Rigidbody>();
       // player.gameStateManager.left.myHero = player;
       // player.gameStateManager.right.myHero = player;

       // player.gameStateManager.shootLeft.myHero = player;
       // player.gameStateManager.shootRight.myHero = player;
    }
	
	// Update is called once per frame
	void Update () {


        if (isController == true)//if player is your controller do this stuff in here
        {
            if (canMove != false)//if player can move get inputs
            {

                //WASD and Arrow Based mvoement space to jump
                    OnDown();
                    OnHold();
                    OnUp();


                

                   // sidSpd = Input.GetAxis("Horizontal");//get controller based movement
                   // fwdSpd = Input.GetAxis("Vertical");//controller based movement

            }

        }
    }


    void FixedUpdate()
    {
     
            this.gameObject.transform.Translate(sidSpd * Time.fixedDeltaTime,0, fwdSpd * Time.fixedDeltaTime);//move player based on inputs
        


    }



    public void OnMouseDown()//if you mouse click player what happens
    {


    }

    public void IncreaseSpeed(bool vertical)
    {
        if(vertical == true)
            {
            fwdSpd = mvspd;
             }
        else
        {
            sidSpd = mvspd;
        }
    }

    public void DecreaseSpeed(bool vertical)
    {
        if (vertical == true)
        {
            fwdSpd = mvspd*-1;
        }
        else
        {
            sidSpd = mvspd*-1;
        }
    }


    //on keys down update speed variables
    public void OnDown()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           
                   
            IncreaseSpeed(true);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            DecreaseSpeed(true);


        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            DecreaseSpeed(false);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {


            IncreaseSpeed(false);


        }



        if (Input.GetKeyDown(KeyCode.Space))//if you press space jump or jump button on control
        {

       
                Jump();

        }

    }





        public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")//if hitting floor player is Grounded and can jump again
        {

            //reset old jumps and isGrounded
            isGrounded = true;

            isJumping = false;
            isDoubleJumping = false;
            doubleJump = false;
        }

      

    }



    public void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "ground")//if hitting floor player is Grounded and can jump again
        {
            isGrounded = false;
            doubleJump = false;
            isDoubleJumping = false;
        }

    }




///NO NEED TO CHNAGE THESE
      


//on keys hold update speed variables

    public void OnHold()
    {




        if (Input.GetKey(KeyCode.LeftArrow) )
        {
        
                sidSpd = mvspd *-1;

        }
        else if (Input.GetKey(KeyCode.RightArrow) )
        {

                      sidSpd = mvspd;

            
        }

        if (Input.GetKey(KeyCode.UpArrow) )
        {

             fwdSpd = mvspd;


        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

                        fwdSpd = mvspd*-1;

        }
    }





//on keys up update speed variables to zero

    public void OnUp()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) )
        {

                sidSpd = 0;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {

                sidSpd = 0;

        }

        if (Input.GetKeyUp(KeyCode.UpArrow) )
        {

                    fwdSpd = 0;


        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {

         fwdSpd = 0;
           
        }
    }


 public void Jump()
    {



        if (!doubleJump && !isGrounded )//double jump
        {
            if (canDblJump == true)
            {
                doubleJump = true;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode.Impulse);

                isDoubleJumping = true;
                doubleJumpedAt = Time.time;
            }
        }
        else if (isGrounded && !doubleJump )//normal jump
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce),ForceMode.Impulse);

            isJumping = true;
        }




    }






}
