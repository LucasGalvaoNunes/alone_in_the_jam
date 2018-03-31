using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*! \class ScoreTable
 *  \brief Manages the HUD objects of high score table.
 *  
 *  Loads the saved data and fill the HUD Text components of the score table, also 
 *  receives the player's name input from a InputField component or from a string (arcade version), 
 *  and send it to the HighScoreManager.
 */
public class ScoreTable : MonoBehaviour
{

    public Text[] nameTexts;                                        //!< Text components for score table names.
    public Text[] scoreTexts;                                       //!< Text components for score table scores.

    public int maxNameCharacter = 3;                                //!< Maximum character amount for score name.

    // Use this for initialization
    void Start()
    {
        /// Loads the score data.
        HighScoreManager.LoadData();
        SetTableValues(HighScoreManager.GetHighScores());
    }

    /// Sets the HUD score table values.
    void SetTableValues(Score[] scoreTable)
    {
        for (int i = 0; i < scoreTable.Length; i++)
        {
            if (scoreTable[i].scoreName != "")
                nameTexts[i].text = scoreTable[i].scoreName;
            if (scoreTable[i].scoreValue > 0)
                scoreTexts[i].text = scoreTable[i].scoreValue.ToString();
        }
    }

    ///// <summary>
    ///// Sets the score name from an InputField.
    ///// </summary>
    //public void SetScoreName (InputField scoreInput)
    //{
    //    if (scoreInput.text.Length >= maxNameCharacter)
    //    {
    //        /// The class that has the score points is get here.
    //        ExampleScorePoints scorePoints = GameObject.FindGameObjectWithTag("GameController").GetComponent<ExampleScorePoints>();

    //        scoreInputPanel.SetActive(false);
    //        HighScoreManager.NewScore(scoreInput.text, scorePoints.ActualScore);
    //        SetTableValues(HighScoreManager.GetHighScores());

    //        /// Calls a reset for the score value.
    //        scoreInput.text = "";
    //        scorePoints.ResetScore();
    //    }
    //}

    ///// <summary>
    ///// Sets the score name form the ArcadeInputs string.
    ///// </summary>
    //public void SetScoreName(string scoreInput)
    //{
    //    /// The class that has the score points is get here.
    //    ExampleScorePoints scorePoints = GameObject.FindGameObjectWithTag("GameController").GetComponent<ExampleScorePoints>();

    //    scoreInputPanel.SetActive(false);
    //    HighScoreManager.NewScore(scoreInput, scorePoints.ActualScore);
    //    SetTableValues(HighScoreManager.GetHighScores());

    //    /// Calls a reset for the score value.
    //    scorePoints.ResetScore();        
    //}
}
