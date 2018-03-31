using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour {
    public bool isFader;
    public GameObject highscore;
    public GameObject creditosText;
    public GameObject creditos;
    public GameObject menu;

    public GameObject tutorial;

    public Image fader;
    public float velocidadeCreditos;
    public float posiYInicioCreditos;
    public float posiYfimCreditos;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isFader)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("Game");
            }
            tutorial.SetActive(true);
            fader.color = Color.Lerp(fader.color, Color.black, 5 * Time.deltaTime);
            
        }
           

        if (creditos.activeSelf)
        {
            
            if (creditosText.GetComponent<RectTransform>().localPosition.y < posiYfimCreditos)
            {
                creditosText.transform.Translate(new Vector3(0, velocidadeCreditos * Time.deltaTime, 0));
            }
            else
            {
                creditosText.GetComponent<RectTransform>().localPosition = new Vector3(creditosText.GetComponent<RectTransform>().localPosition.x, posiYInicioCreditos, creditosText.GetComponent<RectTransform>().localPosition.z);
            }
        }
        
    }
    public void OpenHighscore()
    {
        highscore.SetActive(true);
        menu.SetActive(false);
    }

    public void Play()
    {
        isFader = true;
    }
    public void  OpenCredits()
    {
        menu.SetActive(false);
        creditos.SetActive(true);
    }
    public void BackMenu()
    {
        menu.SetActive(true);
        creditos.SetActive(false);
        highscore.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
   

}
