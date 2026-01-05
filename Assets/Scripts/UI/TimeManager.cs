using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    // reference to real time
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 0.2f;
    private float timer;

    void Start()
    {
        // Setting the current time in-game
        Minute = 0;
        Hour = 8;
        timer = minuteToRealTime;
    }

    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            // for detecting if the minutes go past 60, adds 1 to the hour and resets the minutes
            if(Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();


            }

            timer = minuteToRealTime;
        }

        
    }
}
