using UnityEngine;
using UnityEngine.UI;

public class CameraStreamTexture_extra : MonoBehaviour
{
    public WebCamTexture cameraTexture;//public camera texture for stoping and switching

    [SerializeField]
    public WebCamDevice[] deviceCameras;//list of camera devices

    public int currentCam;//current camera
    public Text printOut;//leaving the print out text info in case nayone or myself wants to see info on it

    public int streamWidth;//width of stream will auto change 2040 is good size
    public int streamHeight;//height of stream will auto change 2040 is good size


    public int ansioLevel;
    public int currentMaterial;
    public Material[] mats;

    public bool skyboxLightSource;
    public float ambientLevel;
    public float lightAmbientLevel;
    public float ambientIntensity;

    public GameObject ambientLight;
    public Light directionalLight;

    private void Start()
    {
         deviceCameras = WebCamTexture.devices;
         currentCam = 0;
         currentMaterial = 0;
        if (! skyboxLightSource)
        {
             ambientLevel = 0.9f;
            RenderSettings.ambientLight = new Color( ambientLevel,  ambientLevel,  ambientLevel, 1f);
        }
        else
             ambientLight.SetActive(false);

         lightAmbientLevel = 0.5f;
         ambientIntensity =  directionalLight.intensity;
         directionalLight.color = new Color( lightAmbientLevel,  lightAmbientLevel,  lightAmbientLevel, 1f);
         directionalLight.intensity =  ambientIntensity;
         StartCamera();
    }

    public void StopCamera()
    {
        if (!((Object) cameraTexture != (Object)null) || ! cameraTexture.isPlaying)
            return;
         cameraTexture.Stop();
    }

    public void StartCamera()
    {
        if ( deviceCameras.Length > 0)
        {
             cameraTexture = new WebCamTexture( deviceCameras[ currentCam].name,  streamWidth,  streamHeight);
             GetComponent<Renderer>().material =  mats[ currentMaterial];
             GetComponent<Renderer>().material.mainTexture = (Texture) cameraTexture;
             cameraTexture.Play();
        }
         cameraTexture.filterMode = FilterMode.Trilinear;
         ansioLevel =  cameraTexture.anisoLevel;
         textUpdate();
    }

    public void switchCamera()
    {
        if (! cameraTexture.isPlaying)
            return;
        if ( currentCam <  deviceCameras.Length - 1)
        {
            ++ currentCam;
             StopCamera();
        }
        else
        {
             currentCam = 0;
             StopCamera();
        }
         StartCamera();
    }

    public void textUpdate()
    {
         printOut.text = (string)(object) deviceCameras.Length + (object)" Cameras detected current camera: " +  deviceCameras[ currentCam].name + "| pos:" + (string)(object) currentCam + "| texture Width & height:" + (string)(object) cameraTexture.width + " , " + (string)(object) cameraTexture.height + "| global embience:" + (string)(object) ambientLevel + "| lightObj ambience & intensity:" + (string)(object) lightAmbientLevel + "," + (string)(object) ambientIntensity + "| currentMaterial:" + (string)(object) currentMaterial;
    }

    public void ChangeAnsioLevel(bool negative)
    {
        if (negative &&  ansioLevel >= 1)
        {
            -- ansioLevel;
             cameraTexture.anisoLevel =  ansioLevel;
        }
        else if (!negative &&  ansioLevel <= 8)
        {
            ++ ansioLevel;
             cameraTexture.anisoLevel =  ansioLevel;
        }
         textUpdate();
    }

    public void ChangeAmbientLevel(bool negative)
    {
        if (negative && (double) ambientLevel >= 0.100000001490116)
        {
             ambientLevel -= 0.1f;
            RenderSettings.ambientLight = new Color( ambientLevel,  ambientLevel,  ambientLevel, 1f);
        }
        else if (!negative && (double) ambientLevel <= 0.899999976158142)
        {
             ambientLevel += 0.1f;
            RenderSettings.ambientLight = new Color( ambientLevel,  ambientLevel,  ambientLevel, 1f);
        }
         textUpdate();
    }

    public void ChangeAmbientIntensity(bool negative)
    {
        if (negative && (double) ambientIntensity >= 0.100000001490116)
        {
             ambientIntensity -= 0.1f;
             directionalLight.intensity =  ambientIntensity;
        }
        else if (!negative && (double) ambientIntensity <= 7.0)
        {
             ambientIntensity += 0.1f;
             directionalLight.intensity =  ambientIntensity;
        }
         textUpdate();
    }

    public void ChangeAmbientLevelLightObj(bool negative)
    {
        if (negative && (double) lightAmbientLevel >= 0.100000001490116)
        {
             lightAmbientLevel -= 0.05f;
             directionalLight.color = new Color( lightAmbientLevel,  lightAmbientLevel,  lightAmbientLevel, 1f);
        }
        else if (!negative && (double) lightAmbientLevel <= 0.949999988079071)
        {
             lightAmbientLevel += 0.05f;
             directionalLight.color = new Color( lightAmbientLevel,  lightAmbientLevel,  lightAmbientLevel, 1f);
        }
         textUpdate();
    }

    public void ChangeMaterial()
    {
         StopCamera();
        if ( currentMaterial <=  mats.Length - 1)
        {
            if ( currentMaterial <  mats.Length - 1)
                ++ currentMaterial;
            else
                 currentMaterial = 0;
        }
         StartCamera();
    }
}
