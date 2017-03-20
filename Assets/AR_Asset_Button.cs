using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AR_Asset_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AR_Asset_GameController gameController;
    public bool isOver = false;
    public bool recurring;
  
    public int actionID;


    public void Update()
    {
         if (isOver == true && recurring == true)
            {
            gameController.UIActions(actionID,true);

            }
        

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
//        Debug.Log("Mouse enter");

        isOver = true;
        gameController.UIActions(actionID, true);
       // Debug.Log("enter control" +actionID);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("Mouse exit");
        //Debug.Log("exit control");
        isOver = false;
        gameController.UIActions(actionID, false);

     
        



    }
}

