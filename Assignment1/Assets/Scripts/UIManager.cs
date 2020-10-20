using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private float time = 0.0f;
    public float Time
    {
        get { return time; }

        set { time = value; }
    }
    bool EndUiactive = false;

    [SerializeField]
    private Text pointsText;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text EndText;

    [SerializeField]
    public void UpdateScoreDisplay(int currentScore)
    {
        pointsText.text = "Points: " + currentScore.ToString();

    }

    public void SetEndUi(int state)
    {
        EndUiactive = true;
        if (state == 2)
        {
            EndText.text = "YOU WIN";
            SetUI();


        }
        else if (state == 1)
        {
            EndText.text = "Next Level";
            SetUI();
        }
        else if(state == 0)
        {
            EndText.text = "YOU LOSE";
            SetUI();
        }
        EndUiactive = false;
    }

    public void SetUI()
    {
        EndText.gameObject.SetActive(EndUiactive); 
    }

    void Start()
    {
        StartCoroutine(TimeAdd());
        SetUI();

    }
    private IEnumerator TimeAdd()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            time++;
            timeText.text = "Time: " + time.ToString();
        }
    }

}
