using System;

[Serializable]
public class DataSystem
{
	public int currentPlayerGameProgress;
	public int rubies;
	public bool guide;
	public bool musicVolumeEnabled;
	public bool effectsVolumeEnabled;
	public bool[] shopUpgradesBought;

	public DataSystem Clone()
	{
		var newData = new DataSystem();
		newData.currentPlayerGameProgress = currentPlayerGameProgress;
		newData.rubies = rubies;
		newData.guide = guide;
		newData.musicVolumeEnabled = musicVolumeEnabled;
		newData.effectsVolumeEnabled = effectsVolumeEnabled;
		newData.shopUpgradesBought = shopUpgradesBought;
		return newData;
	}
}
