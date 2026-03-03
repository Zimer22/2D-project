using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    GameObject SceneController;
    SceneController SceneControllerScript;
    [SerializeField] bool      t_timeron;
    private double               t_minutes;
    private double t_seconds;
    private double t_actualtimeleft;
    private TMP_Text           t_timertext;

    void Start()
    {
        t_timertext = GetComponentInChildren<TMP_Text>();
        SceneController = GameObject.Find("SceneController");
        SceneControllerScript = SceneController.GetComponent<SceneController>();
    }

    void Update()
    {
        if (t_timeron && t_actualtimeleft > 0)
        {
            t_actualtimeleft -= Time.deltaTime;
        }
        else if (t_actualtimeleft < 0)
        {
            SceneControllerScript.CycleDay();
        }
        t_minutes = Math.Floor(t_actualtimeleft / 60d);
        t_seconds = t_actualtimeleft - (t_minutes * 60d);
        string t_textsecs = t_seconds.ToString().Replace('.',' ');
        t_timertext.text = t_minutes.ToString() + ":" + t_textsecs[0] + t_textsecs[1];
    }
    public void StartTimer(double time)
    {
        t_actualtimeleft = time;
        t_timeron = true; 
    }
}
