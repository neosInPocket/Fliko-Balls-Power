using System.IO;
using UnityEngine;

public class DataSystemController : MonoBehaviour
{
	[SerializeField] private bool resetValues;
	[SerializeField] private DataSystem defaultValues;
	private static string dataFilePath => Application.persistentDataPath + "/DataSystemSaves.json";
	public static DataSystem DataSystemValues { get; set; }
	public static DataSystem DefaultValues { get; set; }

	private void Awake()
	{
		DefaultValues = defaultValues.Clone();

		if (resetValues)
		{
			DataSystemValues = defaultValues.Clone();
			SaveSystemValues();
		}
		else
		{
			SetDefaultValues();
		}
	}

	public static void SaveSystemValues()
	{
		if (!File.Exists(dataFilePath))
		{
			WriteNew();
		}
		else
		{
			WriteAll();
		}
	}

	public static void SetDefaultValues()
	{
		if (!File.Exists(dataFilePath))
		{
			WriteNew();
		}
		else
		{
			string text = File.ReadAllText(dataFilePath);
			DataSystemValues = JsonUtility.FromJson<DataSystem>(text);
		}
	}

	private static void WriteNew()
	{
		DataSystemValues = DefaultValues.Clone();
		File.WriteAllText(dataFilePath, JsonUtility.ToJson(DataSystemValues, prettyPrint: true));
	}

	private static void WriteAll()
	{
		File.WriteAllText(dataFilePath, JsonUtility.ToJson(DataSystemValues, prettyPrint: true));
	}
}
