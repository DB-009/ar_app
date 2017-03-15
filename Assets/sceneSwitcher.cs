using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneSwitcher : MonoBehaviour {

    [System.Serializable]
    public class customScene
    {
        public bool walkable;
        public GameObject scene;
        public Transform spawnLocation;
    }
    public bool autoStart;

    public playerController cameraPlayer;
    public int curScene;
    public List<customScene> scenes = new List<customScene>();
	// Use this for initialization
	void Start () {
        if(autoStart == true)
        {
            cameraPlayer = Camera.main.gameObject.GetComponent<playerController>();

            scenes[curScene].scene.SetActive(true);
            if (scenes[curScene].walkable == false)
            {
                cameraPlayer.canMove = false;
                cameraPlayer.camControls.inWalkableScene = false;
                cameraPlayer.rb.useGravity = false;
            }
            else
            {
                cameraPlayer.canMove = true;
                cameraPlayer.camControls.inWalkableScene = true;
                cameraPlayer.rb.useGravity = true;

            }
        }



    }
	


    public void sceneSwitch (bool previous)
    {
        if(previous == false)//if pressed next scene
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
            cameraPlayer.canMove = false;
            cameraPlayer.camControls.inWalkableScene = false;
            cameraPlayer.rb.useGravity = false;

        }
        else
        {
            cameraPlayer.canMove = true;
            cameraPlayer.camControls.inWalkableScene = true;
            cameraPlayer.rb.useGravity = true;
        }

        cameraPlayer.transform.position = scenes[curScene].spawnLocation.position;
        cameraPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
        cameraPlayer.camControls.rotX = 0;
        cameraPlayer.camControls.rotY = 0;

    }
}
