using TMPro;
using UnityEngine;

public class RubinsController : MonoBehaviour
{
	[SerializeField] private TMP_Text rubinsText;

	private void Start()
	{
		rubinsText.text = DataSystemController.DataSystemValues.rubies.ToString();
	}

	public void UpdateStatuses()
	{
		rubinsText.text = DataSystemController.DataSystemValues.rubies.ToString();
	}
}
