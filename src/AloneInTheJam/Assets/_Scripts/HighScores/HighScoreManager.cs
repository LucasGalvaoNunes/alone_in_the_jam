using UnityEngine;

/*! \struct Score
 *  \brief Data saved on high score table. 
 */ 
public struct Score
{
    public string scoreName;        //!< Player name.
    public int scoreValue;          //!< Player score.
}

/*! \class HighScoreManager
 *  \brief Manages the saved high scores.
 *  
 *  Adds new score values in the score table and save this data to PlayerPrefs.
 */ 
public class HighScoreManager {    

    static Score[] highScores = new Score[5];                                         //!< Array with the high scores data.       

    /// Saves the high scores data to PlayerPrefs.   
    static void SaveData()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            string highScore = highScores[i].scoreName + "/" + highScores[i].scoreValue;
            PlayerPrefs.SetString("HighScore" + i, highScore);
        }
    }

    /// Checks if a new score is higher than any previous values, and find its rank position.     
    static bool IsHighScore(int score, ref int rank)
    {
        for (int i = highScores.Length - 1; i >= 0; i--)
        {
            if (highScores[i].scoreValue > score)
            {
                break;
            }

            rank--;
        }

        return rank < highScores.Length;
    }

    /// <summary>
    /// Checks if a new score is higher than the last rank position to enter the highscore table.
    /// </summary> 
    public static bool IsHighScore(int score)
    {     
        return score > highScores[highScores.Length - 1].scoreValue;
    }

    /// <summary>
    /// Returns the high scores data.
    /// </summary>    
    public static Score[] GetHighScores() {
        return highScores;
    }
    
    /// <summary>
    /// Loads the saved high scores, optionally creates an initial table.
    /// </summary>    
    public static void LoadData(bool createInitalTable = false, int higherInitialScore = 100)
    {        
        for (int i = 0; i < highScores.Length; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                string[] highScoreData = PlayerPrefs.GetString("HighScore" + i).Split('/');

                highScores[i].scoreName = highScoreData[0];
                highScores[i].scoreValue = int.Parse(highScoreData[1]);                    
            }            
            else
            {
                // Fill the score table with temp values if no one was previously saved.
                if (createInitalTable)
                {
                    int letter01 = Random.Range(65, 91);
                    int letter02 = Random.Range(65, 91);
                    int letter03 = Random.Range(65, 91);
                    string randomName = char.ConvertFromUtf32(letter01) + char.ConvertFromUtf32(letter02) + char.ConvertFromUtf32(letter03);

                    highScores[i].scoreName = randomName;
                    highScores[i].scoreValue = higherInitialScore / (i + 1);

                    string highScore = highScores[i].scoreName + "/" + highScores[i].scoreValue;
                    PlayerPrefs.SetString("HighScore" + i, highScore);
                }
                else
                {
                    highScores[i].scoreName = "";
                    highScores[i].scoreValue = 0;
                }
            }
        }
    }    

    /// <summary>
    /// Receives a new score from the player.
    /// </summary>    
    public static void NewScore(string newName, int newScore)
    {
        int scoreRank = 5;  // New score position rank.

        if (IsHighScore(newScore, ref scoreRank))
        {
            // If the value is not in the last rank position, the array need to be rearranged.
            if (scoreRank < highScores.Length - 1)
            {                
                for (int i = highScores.Length - 2; i >= scoreRank; i--)
                {
                    highScores[i + 1].scoreName = highScores[i].scoreName;
                    highScores[i + 1].scoreValue = highScores[i].scoreValue;
                }
            }            

            highScores[scoreRank].scoreName = newName;
            highScores[scoreRank].scoreValue = newScore;

            SaveData();
        }
    }
}
