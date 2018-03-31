using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Recharge : MonoBehaviour {
    public Text aperteE;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagsUtil.PLAYER)
        {
            aperteE.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagsUtil.PLAYER)
        {
            aperteE.gameObject.SetActive(false);
        }
    }
}
