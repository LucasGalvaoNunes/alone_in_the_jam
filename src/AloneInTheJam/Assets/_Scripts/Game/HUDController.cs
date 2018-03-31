using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    public static HUDController instance;

    public GameObject UICamera;

    public Image crosshair;

    void Awake()
    {
        UICamera.SetActive(false);
    }
	// Use this for initialization
	void Start () {
        instance = this;
        UICamera.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
