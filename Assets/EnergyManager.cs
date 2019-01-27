using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
	[SerializeField] private GameObject[] yolkIcons;

	private Player player;
	public EnergyLevels EnergyLevel { get; private set; } = EnergyLevels.seven;

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
		ManagePlayerRolls();
		KillPlayer();
	}

	private void KillPlayer()
	{
		if(EnergyLevel == EnergyLevels.zero) StartCoroutine(player.Die());
	}

	private void SetEnergyLevelState()
	{
		if (player.DeathTimer >= player.MaxTimeOutsideYolk)
			EnergyLevel = EnergyLevels.zero;
		else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.14f &&
			player.DeathTimer > 0f)
			EnergyLevel = EnergyLevels.seven;
		else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.28f &&
			player.DeathTimer > player.MaxTimeOutsideYolk * 0.14f)
			EnergyLevel = EnergyLevels.six;
		else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.32f &&
			player.DeathTimer > player.MaxTimeOutsideYolk * 0.28f)
			EnergyLevel = EnergyLevels.five;
		else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.46f &&
			player.DeathTimer > player.MaxTimeOutsideYolk * 0.32f)
			EnergyLevel = EnergyLevels.four;
		else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.60f &&
			player.DeathTimer > player.MaxTimeOutsideYolk * 0.46f)
			EnergyLevel = EnergyLevels.three;
		else if (player.DeathTimer < player.MaxTimeOutsideYolk * 0.74f &&
			player.DeathTimer > player.MaxTimeOutsideYolk * 0.60f)
			EnergyLevel = EnergyLevels.two;
		else if (player.DeathTimer > player.MaxTimeOutsideYolk * 0.74f)
			EnergyLevel = EnergyLevels.one;
	}

	private void UpdateEnergyLevelsUI()
	{
		if (player.IsDying)
			UpdateEnergyLevelsDyingUI();
		else if(player.IsReseting)
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
		if (EnergyLevel == EnergyLevels.six)
			EnableGO(yolkIcons);
		else if(EnergyLevel == EnergyLevels.five)
			EnableGO(yolkIcons[5], yolkIcons[4], yolkIcons[3], yolkIcons[2],
				yolkIcons[1], yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.four)
			EnableGO(yolkIcons[4], yolkIcons[3], yolkIcons[2], yolkIcons[1],
				yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.three)
			EnableGO(yolkIcons[3], yolkIcons[2], yolkIcons[1], yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.two)
			EnableGO(yolkIcons[2], yolkIcons[1], yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.one)
			EnableGO(yolkIcons[1], yolkIcons[0]);
		else if(EnergyLevel == EnergyLevels.zero)
			EnableGO(yolkIcons[0]);
	}

	private void ManagePlayerRolls()
	{
		if (EnergyLevel == EnergyLevels.seven && player.Rolling)
			EnergyLevel = EnergyLevels.six;
		else if(EnergyLevel == EnergyLevels.six && player.Rolling)
			EnergyLevel = EnergyLevels.five;
		else if(EnergyLevel == EnergyLevels.five && player.Rolling)
			EnergyLevel = EnergyLevels.four;
		else if(EnergyLevel == EnergyLevels.four && player.Rolling)
			EnergyLevel = EnergyLevels.three;
		else if(EnergyLevel == EnergyLevels.three && player.Rolling)
			EnergyLevel = EnergyLevels.two;
		else if(EnergyLevel == EnergyLevels.two && player.Rolling)
			EnergyLevel = EnergyLevels.one;
		else if(EnergyLevel == EnergyLevels.one && player.Rolling)
			EnergyLevel = EnergyLevels.zero;
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
