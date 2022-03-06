using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menu : MonoBehaviour
{
    public TextMeshProUGUI coin_text;
    public TextMeshProUGUI stone_text;
    public TextMeshProUGUI troppy_text;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        coin_text.text = PlayerPrefs.GetInt("gold", 0).ToString();
        stone_text.text = PlayerPrefs.GetInt("stone", 0).ToString();
        troppy_text.text = PlayerPrefs.GetInt("troppy", 0).ToString();
    }
    public void changescene(int n)
    {
        SceneManager.LoadScene(n);
    }
}
