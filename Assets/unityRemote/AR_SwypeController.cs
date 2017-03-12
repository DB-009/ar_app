using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_SwypeController : MonoBehaviour {

    public float minSwipeDistY;
    public float minSwipeDistX;
    public float swypeVar,camTurnDistance, camTurnForce,touchDelta,power,rotX,rotY,keyboardMultiplier;
    public Rigidbody rb;
    public Vector3 camerRot;

    [SerializeField]
    public List<taps> currentTouches = new List<taps> { new taps(), new taps(), new taps(), new taps(), new taps(), new taps(), new taps(), new taps(), new taps(), new taps() };
   
    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        minSwipeDistY = Screen.height * swypeVar;
        minSwipeDistX = Screen.width * swypeVar;
    }
	
	// Update is called once per frame
	void Update () {
        _touchControls();
        OnHold();//keyboard fall back
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(camerRot), Time.deltaTime*camTurnForce);
       // Debug.Log(Time.deltaTime + ":" + touchDelta);
    }





    void _touchControls()
    {
        float swipeVerValue = 0;
        float swipeHorValue = 0;
       
       
        //#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                TouchPhase phase = touch.phase;

                if (phase == TouchPhase.Began)
                {
                    Debug.Log("touch detected");
                    currentTouches[touch.fingerId] = (new taps(Time.realtimeSinceStartup, touch.position, touch.position, touch.fingerId));
                }

                if (phase == TouchPhase.Moved)
                {
                    //print("Touch index " + touch.fingerId + " moving ");
                    currentTouches[touch.fingerId].position = touch.position;
                    float deltaX = currentTouches[touch.fingerId].start_pos.x - currentTouches[touch.fingerId].position.x;
                    float deltaY = currentTouches[touch.fingerId].start_pos.y - currentTouches[touch.fingerId].position.y;
                     rotX -= deltaY * Time.deltaTime * camTurnForce * -1;
                     rotY += deltaX * Time.deltaTime * camTurnForce * -1;
                    this.transform.eulerAngles = new Vector3(rotX, rotY, 0);
                }
                if (phase == TouchPhase.Ended)
                {

                    float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, currentTouches[touch.fingerId].start_pos.y, 0)).magnitude;
                    float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(currentTouches[touch.fingerId].start_pos.x, 0, 0)).magnitude;

                    //touchDelta = Time.realtimeSinceStartup - currentTouches[touch.fingerId].time;


                    Debug.Log("Remove min swipe and just move camera to left righ up or down min swype is for other type of games");

                   if (swipeDistVertical > swipeDistHorizontal)
                    {
                         swipeVerValue = Mathf.Sign(touch.position.y - currentTouches[touch.fingerId].start_pos.y);


                        if (swipeVerValue < 0 && swipeDistVertical >= minSwipeDistY)
                        {
                            Debug.Log("DOwn Swype");
                        //x up is down, x down is up, y left is right, y right is left
                       // Debug.Log("turn force should  be" + camTurnDistance * (swipeVerValue * swipeDistVertical));
                           // power = swipeDistVertical / touchDelta;
                            //camerRot += new Vector3(camTurnDistance, 0, 0) *power;
                        }
                        else if (swipeVerValue > 0 && swipeDistVertical >= minSwipeDistY)
                        {
                            Debug.Log("Up Swype");
                        //x up is down, x down is up, y left is right, y right is left
                       // Debug.Log("turn force should  be" + camTurnDistance * (swipeVerValue * swipeDistVertical));
                           // power = -swipeDistVertical / touchDelta;
                           // camerRot -= new Vector3(camTurnDistance, 0, 0) * power;
                        }
                        else
                        {
                            Debug.Log("ver Swype didnt meet minimum");
                        }

                    }
                    else if (swipeDistHorizontal > swipeDistVertical)
                    {
                         swipeHorValue = Mathf.Sign(touch.position.x - currentTouches[touch.fingerId].start_pos.x);

                        if (swipeHorValue < 0 && swipeDistHorizontal >= minSwipeDistX)
                        {
                            Debug.Log("Left Swype");
                        //x up is down, x down is up, y left is right, y right is left
                        //Debug.Log("turn force should  be" + camTurnDistance * (swipeVerValue * swipeDistHorizontal));
                            //power = swipeDistHorizontal / touchDelta;
                           // camerRot += new Vector3(0, camTurnDistance, 0)* power;
                    }
                        else if (swipeHorValue > 0 && swipeDistHorizontal >= minSwipeDistX)
                        {
                            Debug.Log("Right Swype");
                        //x up is down, x down is up, y left is right, y right is left
                        //Debug.Log("turn force should  be" + camTurnDistance * (swipeVerValue * swipeDistHorizontal));
                           // power = -swipeDistHorizontal / touchDelta;
                           // camerRot -= new Vector3(0, camTurnDistance, 0) * power;
                        }
                        else
                        {
                            Debug.Log("hor Swype didnt meet minimum");
                        }

                    }

                    //rb.AddTorque(camerRot, ForceMode.Force);
                }
            }
        
        }
    }





    public void OnHold()
    {



        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            
            rotX -= Time.deltaTime * camTurnForce * keyboardMultiplier;
          
            this.transform.eulerAngles = new Vector3(rotY, rotX, 0);

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {

            rotX += Time.deltaTime * camTurnForce * keyboardMultiplier;

            this.transform.eulerAngles = new Vector3(rotY, rotX, 0);

        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {

            rotY -= Time.deltaTime * camTurnForce * keyboardMultiplier;

            this.transform.eulerAngles = new Vector3(rotY, rotX, 0);

        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {

             rotY += Time.deltaTime* camTurnForce * keyboardMultiplier;

            this.transform.eulerAngles = new Vector3(rotY, rotX, 0);
        }
    }


}
