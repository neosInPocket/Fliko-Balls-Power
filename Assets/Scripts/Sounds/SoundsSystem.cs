using System.Linq;
using UnityEngine;

public class SoundsSystem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        var others = FindObjectsOfType<SoundsSystem>();
        var mine = others.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

        if (mine != null && mine != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSource.enabled = DataSystemController.DataSystemValues.musicVolumeEnabled;
    }

    public void IncreaseMusicValue(float value)
    {
        audioSource.volume += value;
        audioSource.enabled = DataSystemController.DataSystemValues.musicVolumeEnabled = audioSource.enabled;
        DataSystemController.SaveSystemValues();
    }
}
