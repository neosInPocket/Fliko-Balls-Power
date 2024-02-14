using UnityEngine;

public class EffectVolume : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Start()
	{
		audioSource.volume = DataSystemController.DataSystemValues.effectsVolumeEnabled ? audioSource.volume : 0;
	}
}
