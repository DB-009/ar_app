using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneSwitcher : MonoBehaviour {

    [System.Serializable]
    public class customScene
    {
        public bool walkable,ar;
        public GameObject scene;
        public Transform spawnLocation;
        public bool skybox;
        public bool changeBgColor;
        public Color bgColor;
       
    }

    public AR_Asset_GameController gameController;
    public playerController cameraPlayer;
    public GameObject billboard;

    public bool autoStart;

    public Color defaultBgColor;

    public int curScene;
    public List<customScene> scenes = new List<customScene>();




    public void sceneSwitch (bool previous)
    {


        gameController.UIFix();


    

        if (previous == false)//if pressed next scene
        {
            if (curScene < scenes.Count-1)
            {

                
                    scenes[curScene].scene.SetActive(false);

                curScene++;
                scenes[curScene].scene.SetActive(true);

            }
            else
            {
                scenes[curScene].scene.SetActive(false);

                curScene = 0;
                scenes[curScene].scene.SetActive(true);
            }
        }
        else//if pressed previous scene
        {
            if (curScene != 0)
            {

               
               scenes[curScene].scene.SetActive(false);

                curScene--;
                scenes[curScene].scene.SetActive(true);

            }
            else
            {
                scenes[curScene].scene.SetActive(false);

                curScene = scenes.Count-1;
                scenes[curScene].scene.SetActive(true);
            }
        }




        if (scenes[curScene].walkable == false)
        {
            cameraPlayer.rb.useGravity = false;
           // cameraPlayer.GetComponent<SphereCollider>().isTrigger = true;
            cameraPlayer.canMove = false;
            cameraPlayer.camControls.inWalkableScene = false;

        }
        else
        {
            //cameraPlayer.GetComponent<SphereCollider>().isTrigger = false;
            cameraPlayer.canMove = true;
            cameraPlayer.camControls.inWalkableScene = true;
            cameraPlayer.rb.useGravity = true;


        }

        if (scenes[curScene].ar == true)
        {
            if (cameraPlayer.transform.GetChild(0).GetComponent<CameraStreamTexture>().unityRemoteTest == false)
                billboard.SetActive(true);
        }
        else
        {
            billboard.SetActive(false);

        }

        if(scenes[curScene].skybox == true)
        {
            cameraPlayer.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
            if (scenes[curScene].changeBgColor == true)
                RenderSettings.ambientLight = scenes[curScene].bgColor;

        }
        else
        {
            cameraPlayer.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
            if(scenes[curScene].changeBgColor == true)
            cameraPlayer.GetComponent<Camera>().backgroundColor = scenes[curScene].bgColor;
        }

        cameraPlayer.transform.position = scenes[curScene].spawnLocation.position;
        cameraPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
        cameraPlayer.camControls.rotX = 0;
        cameraPlayer.camControls.rotY = 0;




    }
}
