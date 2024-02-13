using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGameScreen : MonoBehaviour
{

	[SerializeField] private TMP_Text rubinsText;
	[SerializeField] private TMP_Text gameScreenEndText;
	[SerializeField] private TMP_Text nextButtonText;
	[SerializeField] private GameObject noteContainer;
	[SerializeField] private GameObject rewardContainer;
	public void ShowWindowEnd(int rubinsGained, string winString, string loseString)
	{
		gameObject.SetActive(true);
		bool lose;
		if (rubinsGained == 0)
		{
			lose = true;
		}
		else
		{
			lose = false;
		}

		noteContainer.SetActive(lose);
		rewardContainer.SetActive(!lose);

		if (lose)
		{
			rubinsText.gameObject.SetActive(false);
			gameScreenEndText.text = loseString;
			nextButtonText.text = "try again";
		}
		else
		{
			gameScreenEndText.text = winString;
			nextButtonText.text = "next";
		}

		rubinsText.text = rubinsGained.ToString();
	}

	public void ReturnBack()
	{
		SceneManager.LoadScene("MenuSystemScene");
	}

	public void LoadNewGame()
	{
		SceneManager.LoadScene("GameSystemScene");
	}
}
