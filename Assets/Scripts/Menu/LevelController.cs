using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;

    private void Start()
    {
        levelText.text = DataSystemController.DataSystemValues.currentPlayerGameProgress.ToString();
    }
}
