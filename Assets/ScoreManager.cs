using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int Score { get; private set; }

    [SerializeField] private GameObject scoreHolder;
    [SerializeField] private int zombiePoints;
    [SerializeField] private int timePoints;
    [SerializeField] private int comboMultiplier = 1;
    private LevelManager lvlManager;

    private void Awake()
    {
        lvlManager = FindObjectOfType<LevelManager>();
    }


    private void Update()
    {
        IncreaseScore("time");
    }

    public void IncreaseScore(string scoreGiver)
    {
        if (scoreGiver == "time")
            Score += (int)lvlManager.PlayTime * timePoints;
        if (scoreGiver == "zombie")
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
