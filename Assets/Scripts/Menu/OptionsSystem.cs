using System;
using UnityEngine;

public class OptionsSystem : MonoBehaviour
{
	[SerializeField] private Color disabledColor;
	[SerializeField] private OptionComponent[] optionComponents;
	private SoundsSystem soundsSystem;

	private void Start()
	{
		soundsSystem = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<SoundsSystem>();

		bool musicValue = DataSystemController.DataSystemValues.musicVolumeEnabled;
		bool effectsValue = DataSystemController.DataSystemValues.effectsVolumeEnabled;

		optionComponents[0].ButtonImage.color = musicValue ? Color.white : disabledColor;
		optionComponents[0].IsEnabled = musicValue;
		soundsSystem.Enabled(musicValue);

		optionComponents[1].ButtonImage.color = effectsValue ? Color.white : disabledColor;
		optionComponents[1].IsEnabled = effectsValue;
		DataSystemController.DataSystemValues.effectsVolumeEnabled = effectsValue;
		DataSystemController.SaveSystemValues();
	}

	public void ToggleMusicValues()
	{
		var optionComponent = optionComponents[0];
		var value = !optionComponent.IsEnabled;

		optionComponent.ButtonImage.color = value ? Color.white : disabledColor;
		optionComponent.IsEnabled = value;
		soundsSystem.Enabled(value);
	}

	public void ToggleSoundEffects()
	{
		var optionComponent = optionComponents[1];
		var value = !optionComponent.IsEnabled;

		optionComponent.ButtonImage.color = value ? Color.white : disabledColor;
		optionComponent.IsEnabled = value;
		DataSystemController.DataSystemValues.effectsVolumeEnabled = value;
		DataSystemController.SaveSystemValues();
	}
}

public enum OptionSystemType
{
	BackgroundMusic,
	SoundEffects
}
