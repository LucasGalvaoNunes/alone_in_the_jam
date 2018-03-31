using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController gameController;
   public  bool gameOver;
    float tempoJogo;
    float auxtempoJogo;
    public int valorTempo;

    public bool iniciaPartida;
    public Phone phoneScript;
    public Image faderGameOver;

    #region UI COMPONENTS
    public Text textCountBruxas;
    public Text textPreparese;
    public Text textseEsconda;
    public Text scoreHighscore;
    public Text seuScore;
    public Text nameHighscore;

    public GameObject pressEtoRechargeGO;
    public GameObject prepareseGO;
    public GameObject seEscondaGO;
    public GameObject scoreGameOver;
    public GameObject highScoreGameOver;

    public Button restart;
    public Button menu;

    #endregion
    public AudioPlayer audioPlayer;
 
    // Use this for initialization
    void Start () {
        restart.interactable = false;
        menu.interactable = false;
        iniciaPartida = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameController = this;
        StartCoroutine(Prepararse(textPreparese, prepareseGO));
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOver)
        {
            faderGameOver.color = Color.Lerp(faderGameOver.color, Color.black, 5 * Time.deltaTime);
        }
        textCountBruxas.text = phoneScript.contFreirasTotal.ToString();
	}

    float alpha;
    public void GameOver()
    {
        gameOver = true;
        iniciaPartida = false;
        if (!HighScoreManager.IsHighScore(Phone.phone.contFreirasTotal))
        {
            scoreGameOver.SetActive(true);
            HighScoreManager.LoadData();
            var high = HighScoreManager.GetHighScores();
            scoreHighscore.text = high[0].scoreValue.ToString();
            nameHighscore.text = high[0].scoreName.ToString();
        }else
        {
            highScoreGameOver.SetActive(true);
            seuScore.text = Phone.phone.contFreirasTotal.ToString();
        }
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ShowWarning()
    {
        seEscondaGO.SetActive(true);
        StartCoroutine(Prepararse(textseEsconda, seEscondaGO));
    }



    public void SaveHighscore(Text name)
    {
        HighScoreManager.NewScore(name.text, Phone.phone.contFreirasTotal);
        restart.interactable = true;
        menu.interactable = true;
    }

    IEnumerator OpenGameObject(GameObject open)
    {
        yield return new WaitForSeconds(2);
        open.SetActive(true);
    }

    IEnumerator Prepararse(Text text, GameObject active)
    {
        text.text = 5.ToString();
        yield return new WaitForSeconds(1);
        text.text = 4.ToString();
        yield return new WaitForSeconds(1);
        text.text = 3.ToString();
        yield return new WaitForSeconds(1);
        text.text = 2.ToString();
        yield return new WaitForSeconds(1);
        text.text = 1.ToString();
        yield return new WaitForSeconds(1);
        text.text = "VAI!";
        
        yield return new WaitForSeconds(1);
        iniciaPartida = true;
        audioPlayer.Caraca();
        active.SetActive(false);
    }
    
}
