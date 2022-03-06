using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelon : MonoBehaviour {

    int highestlevel;
    public Button[] buttons;
    public GameObject[] completed;
    public GameObject[] tick;
    public GameObject[] newlevel;
    public GameObject[] locked;

	// Use this for initialization
	void Start () {
     highestlevel=   PlayerPrefs.GetInt("hightest", 3);
        Debug.Log(highestlevel);
        highestlevel -= 2;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < completed.Length; i++)
        {
            completed[i].SetActive(false);
        }
        for (int i = 0; i < tick.Length; i++)
        {
            tick[i].SetActive(false);
        }
        for (int i = 0; i < newlevel.Length; i++)
        {
            newlevel[i].SetActive(false);
        }
        for (int i = 0; i < locked.Length; i++)
        {
            locked[i].SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < highestlevel; i++)
        {
            buttons[i].interactable = true;
        }
        for (int i = 0; i < highestlevel-1; i++)
        {
            completed[i].SetActive(true);
        }
        for (int i = 0; i < highestlevel-1; i++)
        {
            tick[i].SetActive(true);
        }

        newlevel[highestlevel-1].SetActive(true);
        
        for (int i = highestlevel-1; i < locked.Length; i++)
        {
            locked[i].SetActive(true);
        }
    }
}
