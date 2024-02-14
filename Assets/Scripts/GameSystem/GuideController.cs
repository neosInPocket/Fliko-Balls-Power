using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GuideController : MonoBehaviour
{
	[SerializeField] private GameObject[] textPanels;
	[SerializeField] private GameObject[] characters;
	[SerializeField] private GameObject[] arrows;
	public Action GuideCompleted { get; set; }

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void Guide()
	{
		gameObject.SetActive(true);
		Touch.onFingerDown += SpringPointer;
		textPanels[0].SetActive(true);
		characters[0].SetActive(true);
	}

	private void SpringPointer(Finger finger)
	{
		Touch.onFingerDown -= SpringPointer;
		Touch.onFingerDown += RockPointer;
		textPanels[0].SetActive(false);
		characters[0].SetActive(false);
		textPanels[1].SetActive(true);
		characters[1].SetActive(true);
		arrows[0].SetActive(true);
	}

	private void RockPointer(Finger finger)
	{
		Touch.onFingerDown -= RockPointer;
		Touch.onFingerDown += ProgressBarPointer;
		textPanels[1].SetActive(false);
		characters[1].SetActive(false);
		textPanels[2].SetActive(true);
		characters[2].SetActive(true);
		arrows[1].SetActive(true);
		arrows[0].SetActive(false);
	}

	private void ProgressBarPointer(Finger finger)
	{
		Touch.onFingerDown -= ProgressBarPointer;
		Touch.onFingerDown += EndPhrase;
		textPanels[2].SetActive(false);
		characters[2].SetActive(false);
		textPanels[3].SetActive(true);
		characters[3].SetActive(true);
		arrows[2].SetActive(true);
		arrows[1].SetActive(false);
	}

	private void EndPhrase(Finger finger)
	{
		Touch.onFingerDown -= EndPhrase;
		Touch.onFingerDown += End;
		textPanels[3].SetActive(false);
		characters[3].SetActive(false);
		textPanels[4].SetActive(true);
		characters[4].SetActive(true);
		arrows[2].SetActive(false);
	}

	private void End(Finger finger)
	{
		Touch.onFingerDown -= End;

		GuideCompleted?.Invoke();
		if (this != null)
		{
			gameObject.SetActive(false);
		}

	}
}
