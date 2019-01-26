using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public int Score { get; private set; }

    [SerializeField] private TextMesh scoreHolder;
    [SerializeField] private int zombiePoints;
    [SerializeField] private int timePoints;
    [SerializeField] private int comboMultiplier = 1;
    private LevelManager lvlManager;

    private void Awake()
    {
        lvlManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        StartCoroutine(IncreaseScoreWithTime());
    }

    private void Update()
    {
        ShowScore();
    }

    private void ShowScore()
    {
        scoreHolder.text = "Score: " + Score;
    }

    private IEnumerator IncreaseScoreWithTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            Score = Score + (timePoints * comboMultiplier);
        }
    }

    public void IncreaseScore()
    {
            Score = Score + (zombiePoints * comboMultiplier);
    }

    public void IncreaseComboMultiplier()
    {
        comboMultiplier++;
    }

    public void ResetComboMultiplier()
    {
        comboMultiplier = 1;
    }
}
