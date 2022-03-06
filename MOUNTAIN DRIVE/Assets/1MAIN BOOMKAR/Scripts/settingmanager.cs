using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingmanager : MonoBehaviour {

    public Slider sound;
    public Slider music;
    private float gamemusicvolume;
    private float soundvolume;
    public Button on_vibration;
    public Button off_vibration;
	// Use this for initialization
	void Start () {
        soundvolume = PlayerPrefs.GetFloat("sound", 1);
        gamemusicvolume = PlayerPrefs.GetFloat("music", .4f);
        music.value = gamemusicvolume;
        sound.value = soundvolume;

    }
	
	// Update is called once per frame
	void Update () {
        gamemusicvolume = music.value;
        soundvolume = sound.value;
        if(PlayerPrefs.GetInt("vibration", 1)==1)
        {
            off_vibration.interactable = true;
            on_vibration.interactable = false;
        }
        else if(PlayerPrefs.GetInt("vibration",1)==0)
        {
            off_vibration.interactable = false;
            on_vibration.interactable = true;
        }
    }
    public void gamevolume(float vol)
    {

        gamemusicvolume = vol;
        PlayerPrefs.SetFloat("music", gamemusicvolume);

    }
    public void soundvolumecontrol(float vol)
    {
        soundvolume = vol;
        PlayerPrefs.SetFloat("sound", soundvolume);
    }
    public void onvibration()
    {
        PlayerPrefs.SetInt("vibration", 1);
    }
    public void offvibration()
    {
        PlayerPrefs.SetInt("vibration", 0);
    }
}
