using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*! \class ExampleScorePoints
 *  \brief Has the simulated score point.
 */
public class ExampleScorePoints : MonoBehaviour {

    public Text txtScore;                           //!< Score Text component.

    int actualScore;                                //!< Actual test score.

    public int ActualScore
    {
        get { return actualScore; }
    }

    /// <summary>
    /// Changes the actual score amount.
    /// </summary>    
	public void ChangePoints (int value)
    {
        actualScore += value;
        txtScore.text = actualScore.ToString();
    }

    /// <summary>
    /// Resets the score value.
    /// </summary>
    public void ResetScore ()
    {
        actualScore = 0;
        txtScore.text = actualScore.ToString();
    }
}
