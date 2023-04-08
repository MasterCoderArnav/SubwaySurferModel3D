using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private float timeMultiplier;
    [SerializeField]
    private float startHour;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private Light sunLight;
    [SerializeField]
    private float sunriseHour;
    [SerializeField]
    private float sunsetHour;
    [SerializeField]
    private Color dayAmbientLight;
    [SerializeField]
    private Color nightAmbientLight;
    [SerializeField]
    private AnimationCurve lightChangeCurve;
    [SerializeField]
    private float maxSunLightIntensity;
    [SerializeField]
    private Light moonLight;
    [SerializeField]
    private float maxMoonLightIntensity;
    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    // Start is called before the first frame update
    void Start(){
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update(){
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay(){
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void RotateSun(){
        float sunLightRotation;
        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime){
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else{
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }
        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings(){
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime){
        TimeSpan diff = toTime - fromTime;
        int day = 24;
        if (diff.TotalSeconds < 0){
            diff += TimeSpan.FromHours(day);
        }
        return diff;
    }
}