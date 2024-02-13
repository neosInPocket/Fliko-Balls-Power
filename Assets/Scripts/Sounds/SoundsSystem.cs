using System.Linq;
using UnityEngine;

public class SoundsSystem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        var soundSystems = GameObject.FindGameObjectsWithTag("SoundSystem");
        var mine = soundSystems.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

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

    public void Enabled(bool value)
    {
        audioSource.enabled = value;
        DataSystemController.DataSystemValues.musicVolumeEnabled = value;
        DataSystemController.SaveSystemValues();
    }
}
