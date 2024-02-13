using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreFrame : MonoBehaviour
{
	[SerializeField] private TMP_Text[] statuses;
	[SerializeField] private TMP_Text[] priceTexts;
	[SerializeField] private Button[] buttons;
	[SerializeField] private int[] prices;
	[SerializeField] private List<RubinsController> rubinsController;
	[SerializeField] private Color purchased;
	[SerializeField] private Color noRubins;
	[SerializeField] private Color canPurchase;

	private void Start()
	{
		RefreshStatuses();
	}

	public void RefreshStatuses()
	{
		rubinsController.ForEach(x => x.UpdateStatuses());

		for (int i = 0; i < 2; i++)
		{
			priceTexts[i].text = prices[i].ToString();

			bool upgradePurchased = DataSystemController.DataSystemValues.shopUpgradesBought[i];
			if (upgradePurchased)
			{
				buttons[i].interactable = false;
				statuses[i].text = "purchased";
				statuses[i].color = purchased;
			}
			else
			{
				if (DataSystemController.DataSystemValues.rubies >= prices[i])
				{
					buttons[i].interactable = true;
					statuses[i].text = "avaliable";
					statuses[i].color = canPurchase;
				}
				else
				{
					buttons[i].interactable = false;
					statuses[i].text = "NOT ENOUGH RUBINS";
					statuses[i].color = noRubins;
				}
			}
		}
	}

	public void Purchase(int index)
	{
		DataSystemController.DataSystemValues.rubies -= prices[index];
		DataSystemController.DataSystemValues.shopUpgradesBought[index] = true;
		DataSystemController.SaveSystemValues();

		RefreshStatuses();
	}
}
