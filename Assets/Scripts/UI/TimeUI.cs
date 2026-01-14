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
        timeText.text = $"Open \r\n {TimeManager.Hour:00}:{TimeManager.Minute:00}";

        if (TimeManager.Hour >= 18 && TimeManager.Minute >= 0)
        {
            timeText.text = $"Closed \r\n 18:00";
            Debug.Log("Flame & Fork is now Closed!");
            return;
        }
    }
}
