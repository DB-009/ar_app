using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_SwypeController : MonoBehaviour {


    public AR_Asset_GameController gameController;

    public float minSwipeDistY;
    public float minSwipeDistX;
    public float swypeVar, camTurnDistance, camTurnForce, touchDelta, power, rotX, rotY, keyboardMultiplier;
    public Rigidbody rb;
    public Vector3 camerRot;

    public float resetTimer;
 

    [SerializeField]
    public List<taps> currentTouches = new List<taps> { new taps(), new taps(), new taps(), new taps(), new taps(), new taps(), new taps(), new taps(), new taps(), new taps() };

    public int fingersDetected;
    public bool detectedPrevious;


    public bool inWalkableScene;
    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        minSwipeDistY = Screen.height * swypeVar;
        minSwipeDistX = Screen.width * swypeVar;





    }
	
	// Update is called once per frame
	void Update () {

        OnHold();//keyboard fall back

        
           _touchControls();
        this.transform.eulerAngles = new Vector3(rotY, rotX, 0);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(camerRot), Time.deltaTime*camTurnForce);
        // Debug.Log(Time.deltaTime + ":" + touchDelta);
    }





    public  void _touchControls()
    {

       
       
        //#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            fingersDetected = Input.touchCount;

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                TouchPhase phase = touch.phase;




                    //Custom controls for this control scheme if gesture is on the controlling finger is 2.
                    int touchToGet = 0;
                if (fingersDetected > 1)
                    touchToGet = fingersDetected - 1 ;

                   // Debug.Log("touch to get is " + touchToGet);

                    if (touch.fingerId == touchToGet)
                        GestureControls(touch);

                

            }
        
        }
    }


    public void GestureControls(Touch touch)
    {
        float swipeVerValue = 0;
        float swipeHorValue = 0;
        TouchPhase phase = touch.phase;

        if (phase == TouchPhase.Began)
        {
            currentTouches[touch.fingerId] = (new taps(Time.realtimeSinceStartup, touch.position, touch.position, touch.fingerId, true));
            print("Touch index " + touch.fingerId + " started ");
        }

        if (phase == TouchPhase.Moved)
        {

            print("Touch index " + touch.fingerId + " moved ");
            //print("Touch index " + touch.fingerId + " moving ");
            currentTouches[touch.fingerId].position = touch.position;

            if (currentTouches[touch.fingerId].alive == false)
            {
                currentTouches[touch.fingerId].start_pos = touch.position;
                currentTouches[touch.fingerId].alive = true;
                currentTouches[touch.fingerId].time = Time.realtimeSinceStartup;
            }


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

                }
                else if (swipeVerValue > 0 && swipeDistVertical >= minSwipeDistY)
                {
                    Debug.Log("Up Swype");

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

                }
                else if (swipeHorValue > 0 && swipeDistHorizontal >= minSwipeDistX)
                {
                    Debug.Log("Right Swype");

                }
                else
                {
                    Debug.Log("hor Swype didnt meet minimum");
                }

            }

            //rb.AddTorque(camerRot, ForceMode.Force);
            currentTouches[touch.fingerId].alive = false;
        }

    }


    public void CameraRotateControls(Touch touch)
    {
        float swipeVerValue = 0;
        float swipeHorValue = 0;
        TouchPhase phase = touch.phase;

            if (phase == TouchPhase.Began)
            {
                currentTouches[touch.fingerId] = (new taps(Time.realtimeSinceStartup, touch.position, touch.position, touch.fingerId,true));
               print("Touch index " + touch.fingerId + " started ");
        }

            if (phase == TouchPhase.Moved)
            {

            print("Touch index " + touch.fingerId + " moved ");
            //print("Touch index " + touch.fingerId + " moving ");
            currentTouches[touch.fingerId].position = touch.position;

                if(currentTouches[touch.fingerId].alive == false)
            {
                currentTouches[touch.fingerId].start_pos = touch.position;
                currentTouches[touch.fingerId].alive = true;
                currentTouches[touch.fingerId].time = Time.realtimeSinceStartup;
            }




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

                    }
                    else if (swipeVerValue > 0 && swipeDistVertical >= minSwipeDistY)
                    {
                        Debug.Log("Up Swype");

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

                    }
                    else if (swipeHorValue > 0 && swipeDistHorizontal >= minSwipeDistX)
                    {
                        Debug.Log("Right Swype");

                    }
                    else
                    {
                        Debug.Log("hor Swype didnt meet minimum");
                    }

                }

            //rb.AddTorque(camerRot, ForceMode.Force);
            currentTouches[touch.fingerId].alive = false;
            }
        
    }


    public void IncreaseTurn(bool vertical)
    {
        if(vertical == false)
        {
            rotX += Time.deltaTime * camTurnForce * keyboardMultiplier;

        }
        else
        {
            rotY += Time.deltaTime * camTurnForce * keyboardMultiplier;

        }
}
    public void DecreaseTurn(bool vertical)
    {
        if (vertical == false)
        {
            rotX -= Time.deltaTime * camTurnForce * keyboardMultiplier;

        }
        else
        {
            rotY -= Time.deltaTime * camTurnForce * keyboardMultiplier;

        }
    }





    public void OnHold()
    {



        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)  && inWalkableScene == false))
        {

            DecreaseTurn(false);     
            this.transform.eulerAngles = new Vector3(rotY, rotX, 0);

        }
        else if ( Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow) && inWalkableScene == false))
        {

            IncreaseTurn(false);
            this.transform.eulerAngles = new Vector3(rotY, rotX, 0);

        }

        if ( Input.GetKey(KeyCode.W)||  (Input.GetKey(KeyCode.UpArrow)&& inWalkableScene == false))
        {

            IncreaseTurn(true);
            this.transform.eulerAngles = new Vector3(rotY, rotX, 0);

        }
        else if (Input.GetKey(KeyCode.S) || ( Input.GetKey(KeyCode.DownArrow) && inWalkableScene == false))
        {

            DecreaseTurn(true);

            this.transform.eulerAngles = new Vector3(rotY, rotX, 0);
        }
    }


}
