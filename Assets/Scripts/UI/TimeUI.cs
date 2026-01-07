using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    public void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";

        if (TimeManager.Hour >= 18)
        {
            timeText.text = $"Closed";
            Debug.Log("Flame & Fork is now Closed!");
        }
    }
}
