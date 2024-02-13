using UnityEngine;
using UnityEngine.UI;

public class OptionComponent : MonoBehaviour
{
	[SerializeField] private Image buttonImage;
	[SerializeField] private OptionSystemType optionSystemType;
	public Image ButtonImage => buttonImage;
	public OptionSystemType OptionSystemType => optionSystemType;
	public bool IsEnabled { get; set; }
}
