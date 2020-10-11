using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] private Text deathCounterText;
    [SerializeField] private Color color;

    private int deathCount;

    void Start()
    {
        deathCounterText.color = color;
        deathCount = 0;
        SetText();
    }

    private void OnEnable()
    {
        DeathDetector.OnDeathZoneContact += UpdateDeathCount;
    }

    private void OnDisable()
    {
        DeathDetector.OnDeathZoneContact -= UpdateDeathCount;
    }

    private void UpdateDeathCount()
    {
        deathCount++;
        SetText();
    }

    private void SetText()
    {
        deathCounterText.text = "Death count: " + deathCount.ToString();
    }
}
