using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
	[SerializeField] private GameObject[] yolkIcons;

	private Player player;
	public EnergyLevels EnergyLevel { get; private set; } = EnergyLevels.seven;

    private bool killFlag;

	private void Awake()
	{
		player = FindObjectOfType<Player>();
	}

	public enum EnergyLevels
	{
		zero, one, two, three, four, five, six, seven
	}

	// Update is called once per frame
	void Update()
	{
		SetEnergyLevelState();
		UpdateEnergyLevelsUI();
		KillPlayer();
        ManagePlayerRoll();
	}

	private void KillPlayer()
	{
        if (EnergyLevel == EnergyLevels.zero && !killFlag)
        {
            StartCoroutine(player.Die());
            killFlag = true;
        }
	}

	private void SetEnergyLevelState()
	{
        if (player.DeathTimer > player.MaxTimeOutsideYolk * 0.84f)
            EnergyLevel = EnergyLevels.zero;
        else if (player.DeathTimer == 0)
            EnergyLevel = EnergyLevels.seven;
        else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.14f &&
            player.DeathTimer > 0f)
            EnergyLevel = EnergyLevels.six;
        else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.28f &&
            player.DeathTimer > player.MaxTimeOutsideYolk * 0.14f)
            EnergyLevel = EnergyLevels.five;
        else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.42f &&
            player.DeathTimer > player.MaxTimeOutsideYolk * 0.28f)
            EnergyLevel = EnergyLevels.four;
        else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.56f &&
            player.DeathTimer > player.MaxTimeOutsideYolk * 0.42f)
            EnergyLevel = EnergyLevels.three;
        else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.70f &&
            player.DeathTimer > player.MaxTimeOutsideYolk * 0.56f)
            EnergyLevel = EnergyLevels.two;
        else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.84f &&
            player.DeathTimer > player.MaxTimeOutsideYolk * 0.70f)
            EnergyLevel = EnergyLevels.one;
    }

    private void ManagePlayerRoll()
    {
        if (EnergyLevel == EnergyLevels.one) player.CanPlayerRoll(false);
        else if(EnergyLevel != EnergyLevels.zero) player.CanPlayerRoll(true);
    }

	private void UpdateEnergyLevelsUI()
	{
			UpdateEnergyLevelsDyingUI();
			UpdateEnergyLevelsResetingUI();
	}

	private void UpdateEnergyLevelsDyingUI()
	{
		if (EnergyLevel == EnergyLevels.six)
			DisableGO(yolkIcons[6]);
		else if (EnergyLevel == EnergyLevels.five)
			DisableGO(yolkIcons[6], yolkIcons[5]);
		else if(EnergyLevel == EnergyLevels.four)
			DisableGO(yolkIcons[6], yolkIcons[5], yolkIcons[4]);
		else if(EnergyLevel == EnergyLevels.three)
			DisableGO(yolkIcons[6], yolkIcons[5], yolkIcons[4], yolkIcons[3]);
		else if(EnergyLevel == EnergyLevels.two)
			DisableGO(yolkIcons[6], yolkIcons[5], yolkIcons[4], yolkIcons[3],
				yolkIcons[2]);
		else if(EnergyLevel == EnergyLevels.one)
			DisableGO(yolkIcons[6], yolkIcons[5], yolkIcons[4], yolkIcons[3],
				yolkIcons[2], yolkIcons[1]);
		else if(EnergyLevel == EnergyLevels.zero)
			DisableGO(yolkIcons[6], yolkIcons[5], yolkIcons[4], yolkIcons[3],
				yolkIcons[2], yolkIcons[1], yolkIcons[0]);
	}

	private void UpdateEnergyLevelsResetingUI()
	{
		if (EnergyLevel == EnergyLevels.seven)
			EnableGO(yolkIcons);
		else if(EnergyLevel == EnergyLevels.six)
			EnableGO(yolkIcons[5], yolkIcons[4], yolkIcons[3], yolkIcons[2],
				yolkIcons[1], yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.five)
			EnableGO(yolkIcons[4], yolkIcons[3], yolkIcons[2], yolkIcons[1],
				yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.four)
			EnableGO(yolkIcons[3], yolkIcons[2], yolkIcons[1], yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.three)
			EnableGO(yolkIcons[2], yolkIcons[1], yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.two)
			EnableGO(yolkIcons[1], yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.one)
			EnableGO(yolkIcons[0]);
	}

	private void DisableGO(params GameObject[] array)
	{
		foreach (GameObject go in array)
			go.SetActive(false);
	}

	private void EnableGO(params GameObject[] array)
	{
		foreach (GameObject go in array)
			go.SetActive(true);
	}
}
