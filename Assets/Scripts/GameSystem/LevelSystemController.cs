using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class LevelSystemController : MonoBehaviour
{
	[SerializeField] private GuideController guide;
	[SerializeField] private LoseGameScreen loseGameScreen;
	[SerializeField] private TMP_Text scoreTMPText;
	[SerializeField] private TMP_Text currentPlayerLevelCaption;
	[SerializeField] private Image progresBar;
	[SerializeField] private PlayerSpring playerSpring;

	public static Vector2 SizeOfScreen;
	private int levelCompleteGoal;
	private int rubies;
	private int playerScore;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		currentPlayerLevelCaption.text = DataSystemController.DataSystemValues.currentPlayerGameProgress.ToString();
		levelCompleteGoal = (int)(2 * Mathf.Log(DataSystemController.DataSystemValues.currentPlayerGameProgress + 1) + 2);
		rubies = (int)(3 * Mathf.Log(DataSystemController.DataSystemValues.currentPlayerGameProgress + 1 + 1) + 3 + DataSystemController.DataSystemValues.currentPlayerGameProgress + 1);
		ReloadProgressBar();

		bool guideNeeded = DataSystemController.DataSystemValues.guide;

		if (guideNeeded)
		{
			DataSystemController.DataSystemValues.guide = false;
			DataSystemController.SaveSystemValues();
			Guide();
		}
		else
		{
			GuideEndedHandler();
		}
	}

	private void Guide()
	{
		guide.GuideCompleted += GuideEndedHandler;
		guide.Guide();
	}

	private void GuideEndedHandler()
	{
		guide.GuideCompleted -= GuideEndedHandler;
		Touch.onFingerDown += OnTapCompleted;
		GameStartedHandler();
	}

	private void OnTapCompleted(Finger finger)
	{
		Touch.onFingerDown -= OnTapCompleted;
	}

	private void ReloadProgressBar()
	{
		progresBar.fillAmount = (float)playerScore / (float)levelCompleteGoal;
		scoreTMPText.text = playerScore.ToString() + "/" + levelCompleteGoal.ToString();
	}

	private void GameStartedHandler()
	{
		playerSpring.OnJumpSuccess += OnPlayerJumpSuccess;
		playerSpring.OnDestroyed += OnPlayerDestroyed;
		playerSpring.ProvideControls();
	}

	private void OnPlayerJumpSuccess()
	{
		playerScore += 1;
		if (playerScore >= levelCompleteGoal)
		{
			playerSpring.OnJumpSuccess -= OnPlayerJumpSuccess;
			playerSpring.OnDestroyed -= OnPlayerDestroyed;

			loseGameScreen.ShowWindowEnd(rubies, "you win!", "you lose...");
			DataSystemController.DataSystemValues.currentPlayerGameProgress += 1;
			DataSystemController.DataSystemValues.rubies += rubies;
			DataSystemController.SaveSystemValues();
		}

		ReloadProgressBar();
	}

	private void OnPlayerDestroyed()
	{
		playerSpring.OnJumpSuccess -= OnPlayerJumpSuccess;
		playerSpring.OnDestroyed -= OnPlayerDestroyed;

		loseGameScreen.ShowWindowEnd(0, "you win!", "you lose...");
	}

	public void ReturnBack()
	{
		SceneManager.LoadScene("MenuSystemScene");
	}

	public void ReloadLevel()
	{
		SceneManager.LoadScene("GameSystemScene");
	}
}
