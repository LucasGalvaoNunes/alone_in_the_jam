using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*! \class ArcadeInputs
 *  \brief Manages the score name, from button inputs.
 *  
 *  Iterate the score name letters, using ASCII, using arcade buttons as input.
 */
public class ArcadeInputs : MonoBehaviour {

    public Text txtNameInput;                                   //!< HUD text of selected name.
    public ScoreTable scoreTable;                               //!< Reference to ScoreTable.

    int actualLetter = 65;                                      //!< Actual ASCII index.
    int letterIndex;                                            //!< Index of the actual selected character.
    string actualName;                                          //!< Actual score name.

    // Use this for initialization
    void Start () {                
        txtNameInput.text = string.Concat((char)actualLetter);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {            
            ChangeLetter(-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLetter(1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectLetter();
        }
    }      

    /// Changes the active letter ASCII index.
    void ChangeLetter (int index)
    {
        actualLetter += index;

        // Clamp the letter index in the ASCII table.
        if (index > 0)
        {
            if (actualLetter > 90 && actualLetter < 95)
            {
                actualLetter = 95;
            }
            else if (actualLetter > 95)
            {
                actualLetter = 65;
            }            
        }
        else
        {
            if (actualLetter < 65)
            {
                actualLetter = 95;
            }
            else if (actualLetter < 95 && actualLetter > 90)
            {
                actualLetter = 90;
            }
        }

        txtNameInput.text = actualName + (char)actualLetter;
    }

    /// Selects the actual letter.
    void SelectLetter ()
    {
        actualName += (char)actualLetter;                
        letterIndex++; // Activate the next letter.
        actualLetter = 65;

        if (letterIndex >= scoreTable.maxNameCharacter)
        {
            //scoreTable.SetScoreName(actualName);
            // Reset the name field.
            actualName = "";
            letterIndex = 0;
            txtNameInput.text = string.Concat((char)actualLetter);
        }                
        else
        {
            txtNameInput.text = actualName + (char)actualLetter;
        }
    }
}
