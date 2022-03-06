using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System.Globalization;

public class rewards : MonoBehaviour
{
    public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        if (System.DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern) == PlayerPrefs.GetString("date"))
        {

            for (int i = 0; i < 2; i++)
            {
                buttons[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void freecoin1()
    {
        Debug.Log(System.DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern));
        PlayerPrefs.SetString("date",System.DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern));
        int n=PlayerPrefs.GetInt("gold", 0);
        n += 50;
        PlayerPrefs.SetInt("gold", n);
        buttons[0].SetActive(false);
    }
    public void freecoin2()
    {
        Debug.Log(System.DateTime.Now.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern));
        int n = PlayerPrefs.GetInt("gold", 0);
        n += 100;
        PlayerPrefs.SetInt("gold", n);
        buttons[1].SetActive(false);
    }
}
