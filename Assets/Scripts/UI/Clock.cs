using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public bool showcurrenttime;
    public TextMeshProUGUI timer;

    private void Start()
    {
        StartCoroutine(currenttime());
    }

    IEnumerator currenttime()
    {
        while (showcurrenttime == true)
        {
            timer.text = System.DateTime.Now.ToShortTimeString();

            yield return null;
        }
    }
}
