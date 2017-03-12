using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class _getTouch : MonoBehaviour {


    public Text printOut;
        
        // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Debug.Log(touch.position);
            //printOut.text = "touch detetcted at :" + touch.position;
        }
	}
}
