using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_Asset_GameController : MonoBehaviour {


    public sceneSwitcher sceneManager;
    public GameObject mobileUI, OnScreenUI,mobileJoyStickLeft, mobileJoyStickRight,mainLayerUI;


    public enum GestureState { none, tapped, held, swyped, doubleTapped }
    public GestureState mobileGestureControl;


    public bool isPC;


    public playerController playerController;
    public AR_SwypeController cameraController;

    // Use this for initialization
    void Start () {

        sceneManager.curScene++;
        sceneManager.sceneSwitch(true);
        Debug.Log("Boot sequence breh hiding canvas");
        UIFix();
    }
	
	// Update is called once per frame
	public void UIFix () {
        if (isPC == true)
        {
            mobileUI.SetActive(false);
            OnScreenUI.SetActive(true);
            mobileJoyStickLeft.SetActive(false);
            mobileJoyStickRight.SetActive(false);

        }
        else
        {
            mobileUI.SetActive(false);
            OnScreenUI.SetActive(true);

            if (sceneManager.scenes[sceneManager.curScene].walkable == true)
            {
                Debug.Log("UI FIX SCENE SPECIFIC UIS");


            }
            mobileJoyStickLeft.SetActive(true);
            mobileJoyStickRight.SetActive(true);
            mainLayerUI.SetActive(true);
        }

    }

    public void showPanel(GameObject panel)
    {
        panel.SetActive(true);
        Debug.Log(panel.activeSelf + " ehh");
    }


    public void closePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void PanelControle(GameObject panel)
    {
        Debug.Log(panel.activeSelf + " ehh");
        if (panel.activeSelf == true)
            closePanel(panel);
        else if (panel.activeSelf == false)
            showPanel(panel);
    }



    public void UIActions(int action, bool running)
    {
        // Debug.Log("gesture control");

        if (action == 0)
        {


        }
        else if (action == 1)
        {
            if (running == true)
            {
                playerController.IncreaseSpeed(true);
            }
            else
            {
                playerController.fwdSpd = 0;

            }
        }
        else if (action == 2)
        {
            if (running == true)
            {
                playerController.DecreaseSpeed(true);
            }
            else
            {
                playerController.fwdSpd = 0;

            }
        }
        else if (action == 3)
        {
            if (running == true)
            {
                playerController.IncreaseSpeed(false);
            }
            else
            {
                playerController.sidSpd = 0;

            }
        }
        else if (action == 4)
        {
            if (running == true)
            {
                playerController.DecreaseSpeed(false);
            }
            else
            {
                playerController.sidSpd = 0;

            }
        }







        else if (action == 5)
        {
            if (running == true)
            {
                playerController.camControls.DecreaseTurn(true);
            }
            else
            {
               

            }
        }
        else if (action == 6)
        {
            if (running == true)
            {
                playerController.camControls.IncreaseTurn(true);
            }
            else
            {

            }
        }
        else if (action == 7)
        {
            if (running == true)
            {
                playerController.camControls.IncreaseTurn(false);
            }
            else
            {

            }
        }
        else if (action == 8)
        {
            if (running == true)
            {
                playerController.camControls.DecreaseTurn(false);
            }
            else
            {

            }
        }


    }



}
