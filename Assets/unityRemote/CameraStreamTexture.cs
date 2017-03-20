using UnityEngine;
using UnityEngine.UI;

public class CameraStreamTexture : MonoBehaviour
{
    public WebCamTexture cameraTexture;//public camera texture for stoping and switching

    [SerializeField]
    public WebCamDevice[] deviceCameras;//list of camera devices

    public int currentCam;//current camera
    public Text printOut;//leaving the print out text info in case nayone or myself wants to see info on it
    
    public int streamWidth;//width of stream will auto change 2040 is good size
    public int streamHeight;//height of stream will auto change 2040 is good size

    public float distanceFromCamera;

    public bool unityRemoteTest;
    private void Start()
    {

        float height = 2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * distanceFromCamera;
        float width = height * Screen.width / Screen.height;

        this.transform.localScale = new Vector3(width, height, 1);

        deviceCameras = WebCamTexture.devices;//set array of camera devices found

        for(int i = 0; i < deviceCameras.Length;i++)
        {
            Debug.Log("found : " + deviceCameras[i].name);
        }



        currentCam = 0;//set current camera to 0
   
        if(unityRemoteTest == false)
        StartCamera();
    }



    public void StartCamera()
    {
        if ( deviceCameras.Length > 0)
        {

            deviceCameras = WebCamTexture.devices;//set array of camera devices found

            for (int i = 0; i < deviceCameras.Length; i++)
            {
                Debug.Log("found : " + deviceCameras[i].name);
            }

            cameraTexture = new WebCamTexture(streamWidth,streamHeight);//create new camera texture with our chosen width and height

             cameraTexture.deviceName = deviceCameras[currentCam].name;//select current camera

            //cameraTexture = new WebCamTexture( deviceCameras[ currentCam].name,  streamWidth,  streamHeight);//crate new camera texture with our chosen camera and setting

            GetComponent<Renderer>().material.mainTexture = cameraTexture;//chnge main texture on billboard object to camera texture
             cameraTexture.Play();//play camera texture
        }
         cameraTexture.filterMode = FilterMode.Trilinear;//change filter mode to trileanear

         //textUpdate();//un comment sand create a text object in canvas to use this
    }


    public void StopCamera()
    {
        if ((cameraTexture != null) || cameraTexture.isPlaying == true)//if stream is playing or camera texture exist stop it
            cameraTexture.Stop();
    }

    public void switchCamera()
    {
        if (cameraTexture.isPlaying == false)//if no camera is playing we probably havent started the process and this shouldnt be run
            return;
        if ( currentCam <  deviceCameras.Length - 1)//if we havent reached last camera go in here otherwise go to else
        {
            ++ currentCam;//increase camera by one
             StopCamera();//stop it 
        }
        else
        {
             currentCam = 0;//reset to 0 if on last camera in array
             StopCamera();
        }
         StartCamera();//start camera with changed current cam variable 
    }

    public void textUpdate()//prints out basic info to a text object on screen
    {
        printOut.text = (string)(object)deviceCameras.Length + (object)" Cameras detected current camera: " + deviceCameras[currentCam].name +
           "| pos:" + (string)(object)currentCam +
           "| texture Width & height:" + (string)(object)cameraTexture.width + " , " + (string)(object)cameraTexture.height;
    }


}
