using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private Button playButton;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;

    private int energy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    //private void Start()
    //{
    //    OnApplicationFocus(true);
    //}

    private void Start()
    {
        //if (!hasFocus) { return; }

        //CancelInvoke(nameof(EnergyRecharged));

        int highScore = PlayerPrefs.GetInt(ScoreHandler.HighScoreKey, 0);

        highScoreText.text = $"High Score: {highScore}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);

        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if (energyReadyString == string.Empty) { return; }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }

        energyText.text = $"Play: {energy}";
    }

    private void EnergyRecharged()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Play: {energy}";
    }

    public void Play()
    {
        if (energy < 1) { return; }

        energy--;

        PlayerPrefs.SetInt(EnergyKey, energy);

        if (energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
# if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
#endif
        }

        SceneManager.LoadScene(1);
    }
}
