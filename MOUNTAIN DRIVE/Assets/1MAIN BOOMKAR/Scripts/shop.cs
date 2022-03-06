using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class shop : MonoBehaviour
{
    public Material[] _matterial;
    int money;
    public GameObject[] carbody;
    private SkinnedMeshRenderer SkinnedMeshRenderer;
    public GameObject money_needed,vehcial,menu,point,checkmark;
    public GameObject[] cars;
    public Image[] car_image;
    public Image[] skin_image;
    public Image[] car_image_blueeffeect;
    public Sprite[] car_sprite;
    public Sprite[] skin_sprite;
    public string[] differenames;
    public TextMeshProUGUI[] car_names;
    public TextMeshProUGUI  buytext;
    public TextMeshProUGUI costs,stone,trophy,moneyneed;
    int cost, selectedcharacter,diff,selectedskin;
    bool sold,soldskin;
    public AudioSource brought;

    public void Awake()
    {
        selectedcharacter =PlayerPrefs.GetInt("car", 0);
        selectedskin = PlayerPrefs.GetInt("Skin", 0);
        onoff();
    }
    public void onoff()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);   
        }
        cars[selectedcharacter].gameObject.transform.position = point.transform.position;
        cars[selectedcharacter].SetActive(true);

    }
    private void Update()
    {
        money = PlayerPrefs.GetInt("gold", 0);
        checkcost();
        SkinnedMeshRenderer = carbody[selectedcharacter].transform.GetComponent<SkinnedMeshRenderer>();
    }
    void maincharacter()
    {
        sold = true;     
    }

    void character1()
    {
        if (PlayerPrefs.GetInt("Sold1", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void character2()
    {
        if (PlayerPrefs.GetInt("Sold2", 0) < 1)
        {
            sold = false;            
        }
        else
        {
            sold = true;
        }
    }
    void character3()
    {
        if (PlayerPrefs.GetInt("Sold3", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }

    void skin0()
    {
        soldskin = true;
    }
    void skin1()
    {
        if (PlayerPrefs.GetInt("skin1", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void skin2()
    {
        if (PlayerPrefs.GetInt("skin2", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void skin3()
    {
        if (PlayerPrefs.GetInt("skin3", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void skin4()
    {
        if (PlayerPrefs.GetInt("skin4", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void skin5()
    {
        if (PlayerPrefs.GetInt("skin5", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void skin6()
    {
        if (PlayerPrefs.GetInt("skin6", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void skin7()
    {
        if (PlayerPrefs.GetInt("skin7", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void skin8()
    {
        if (PlayerPrefs.GetInt("skin8", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    void skin9()
    {
        if (PlayerPrefs.GetInt("skin9", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    public void buyskin()
    {
        switch (selectedskin)
        {
            case 0:
                skin0();
                break;
            case 1:
                skin1();
                break;
            case 2:
                skin2();
                break;
            case 3:
                skin3();
                break;
            case 4:
                skin4();
                break;
            case 5:
                skin5();
                break;
            case 6:
                skin6();
                break;
            case 7:
                skin7();
                break;
            case 8:
                skin8();
                break;
            case 9:
                skin9();
                break;
        }
        if (soldskin == false)
        {
            if (money >= cost)
            {
                money -= cost;
                purchasing_skin();
                PlayerPrefs.SetInt("gold", money);
                brought.Play();
            }
            else
            {
                money_needed.SetActive(true);
                diff = cost - money;
                moneyneed.text = diff.ToString();
            }

        }
        else if (soldskin == true)
        {
            PlayerPrefs.SetInt("Skin", selectedskin);
        }
    }

    void purchasing_skin()
    {
        switch (selectedskin)
        {
            case 1:
                PlayerPrefs.SetInt("skin1", 2);
                break;
            case 2:
                PlayerPrefs.SetInt("skin2", 2);
                break;
            case 3:
                PlayerPrefs.SetInt("skin3", 2);
                break;
            case 4:
                PlayerPrefs.SetInt("skin4", 2);
                break;
            case 5:
                PlayerPrefs.SetInt("skin5", 2);
                break;
            case 6:
                PlayerPrefs.SetInt("skin6", 2);
                break;
            case 7:
                PlayerPrefs.SetInt("skin7", 2);
                break;
            case 8:
                PlayerPrefs.SetInt("skin8", 2);
                break;
            case 9:
                PlayerPrefs.SetInt("skin9", 2);
                break;
        }
        PlayerPrefs.SetInt("skin", selectedskin);
    }
    public void buybutton()
    {
      
        switch (selectedcharacter)
        {

            case 0:
                maincharacter();
                break;
            case 1:
                character1();
                break;
            case 2:
                character2();
                break;
            case 3:
                character3();
                break;

        }
        if (sold == false)
        {
             if (money >= cost)
             {
                    money -= cost;
                    purchasing();
                    PlayerPrefs.SetInt("gold", money);
                    PlayerPrefs.SetInt("car", selectedcharacter);
                    brought.Play();
              }
             else
            {
                vehcial.SetActive(false);
                money_needed.SetActive(true);
                diff = cost - money;
                moneyneed.text = diff.ToString();
            }
               
        }
        else if (sold == true)
        {
            PlayerPrefs.SetInt("car", selectedcharacter);
            vehcial.SetActive(false);
            menu.SetActive(true);

        }

    }
    void purchasing()
    {
        switch (selectedcharacter)
        {
            case 1:
                PlayerPrefs.SetInt("Sold1", 2);
                break;
            case 2:
                PlayerPrefs.SetInt("Sold2", 2);
                break;
            case 3:
                PlayerPrefs.SetInt("Sold3", 2);
                break;
        }
        PlayerPrefs.SetInt("car", selectedcharacter);
    }
    public void left()
    {
        selectedcharacter -= 1;
        if (selectedcharacter<0)
        {
            selectedcharacter = cars.Length;
        }
        onoff();
        car_imagecheck();
    }

    public void right()
    {
        selectedcharacter += 1;
        if (selectedcharacter >= cars.Length)
        {
            selectedcharacter = 0;
        }
        onoff();
        car_imagecheck();
    }
    public void left_skin()
    {
        selectedskin -= 1;
        if (selectedskin < 0)
        {
            selectedskin = skin_sprite.Length;
        }
        skin_check();
    }
    public void right_skin()
    {
        selectedskin += 1;
        if (selectedskin >= skin_sprite.Length)
        {
            selectedskin = 0;
        }
        skin_check();
    }

    void skin_check()
    {
        SkinnedMeshRenderer.material = _matterial[selectedskin];
        switch(selectedskin)
        {
            case 0:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    skin_image[i].sprite = skin_sprite[i];
                }
                break;
            case 1:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    skin_image[i].sprite = skin_sprite[i+selectedskin];
                }
                break;
            case 2:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    skin_image[i].sprite = skin_sprite[i+selectedskin];
                }
                break;
            case 3:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    skin_image[i].sprite = skin_sprite[i + selectedskin];
                }
                break;
            case 4:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    skin_image[i].sprite = skin_sprite[i + selectedskin];
                }
                break;
            case 5:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    skin_image[i].sprite = skin_sprite[i + selectedskin];
                }
                break;
            case 6:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    skin_image[i].sprite = skin_sprite[i + selectedskin];
                }
                break;
            case 7:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    skin_image[i].sprite = skin_sprite[i + selectedskin];
                }
                break;
            case 8:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    if(i==2)
                    {
                        skin_image[2].sprite = skin_sprite[0];
                    }
                    else
                    skin_image[i].sprite = skin_sprite[i + selectedskin];
                }
                break;
            case 9:
                for (int i = 0; i < skin_image.Length; i++)
                {
                    if (i == 2)
                    {
                        skin_image[2].sprite = skin_sprite[1];
                    }
                    else if(i==1)
                    {
                        skin_image[2].sprite = skin_sprite[0];
                    }
                    else
                        skin_image[i].sprite = skin_sprite[i + selectedskin];
                }
                break;

        }
    }
    void car_imagecheck()
    {
        switch (selectedcharacter)
        {

            case 0:
                for (int i = 0; i < car_image.Length; i++)
                {
                    car_image_blueeffeect[i].sprite = car_sprite[i];
                    car_image[i].sprite = car_sprite[i];
                    car_names[i].text = differenames[i];
                }
                break;
            case 1:
                for (int i = 0; i < car_image.Length; i++)
                {
                    car_image[i].sprite = car_sprite[i+1];
                    car_image_blueeffeect[i].sprite = car_sprite[i + 1];
                    car_names[i].text = differenames[i+1];
                    if (i==car_image.Length)
                    {
                        car_image[i].sprite = car_sprite[0];
                        car_image_blueeffeect[i].sprite = car_sprite[0];
                        car_names[i].text = differenames[0];
                    }
                }
                break;
            case 2:
                for (int i = 0; i < car_image.Length; i++)
                {
                    if (i == 0)
                    {
                        car_image[0].sprite = car_sprite[2];
                        car_names[0].text = differenames[2];
                        car_image_blueeffeect[0].sprite = car_sprite[2];
                    }
                    if (i == car_image.Length)
                    {
                        car_image[i].sprite = car_sprite[1];
                        car_image_blueeffeect[i].sprite = car_sprite[1];
                        car_names[i].text = differenames[1];
                    }
                    if (i == car_image.Length - 1)
                    {
                        car_image[i].sprite = car_sprite[0];
                        car_image_blueeffeect[i].sprite = car_sprite[0];
                        car_names[i].text = differenames[0];
                    }
                    if (i == car_image.Length - 2)
                    {
                        car_image[i].sprite = car_sprite[3];
                        car_image_blueeffeect[i].sprite = car_sprite[3];
                        car_names[i].text = differenames[3];
                    }
                }
                break;
            case 3:
                for (int i = 0; i < car_image.Length; i++)
                {
                    if (i == 0)
                    {
                        car_image[0].sprite = car_sprite[3];
                        car_image_blueeffeect[i].sprite = car_sprite[3];
                        car_names[0].text = differenames[3];
                    }
                    if (i == car_image.Length)
                    {
                        car_image[i].sprite = car_sprite[2];
                        car_image_blueeffeect[i].sprite = car_sprite[2];
                        car_names[i].text = differenames[2];
                    }
                    if (i == car_image.Length - 1)
                    {
                        car_image[i].sprite = car_sprite[1];
                        car_image_blueeffeect[i].sprite = car_sprite[1];
                        car_names[i].text = differenames[1];
                    }
                    if (i == car_image.Length - 2)
                    {
                        car_image[i].sprite = car_sprite[0];
                        car_image_blueeffeect[i].sprite = car_sprite[0];
                        car_names[i].text = differenames[0];
                    }
                }
                break;

        }
    }
    void checkcost()
    {
        switch(selectedcharacter)
        {
            case 0:
                buytext.text = "OWNED";
                checkmark.SetActive(false);
                break;
            case 1:
                if (PlayerPrefs.GetInt("Sold1", 0) > 1)
                {
                    buytext.text = "OWNED";
                    checkmark.SetActive(false);

                }
                else
                {
                    checkmark.SetActive(true);
                    costs.text = "500";
                    buytext.text = "buy";
                    stone.text = "10";
                    trophy.text = "2";
                }
                cost = 500;
                break;
            case 2:

                if (PlayerPrefs.GetInt("Sold2", 0) > 1)
                {
                    buytext.text = "OWNED";
                    checkmark.SetActive(false);

                }
                else
                {
                    checkmark.SetActive(true);
                    costs.text = "1000";
                    buytext.text = "buy";
                    stone.text = "20";
                    trophy.text = "5";
                }
                cost = 1000;
                break;
            case 3:

                if (PlayerPrefs.GetInt("Sold3", 0) > 1)
                {
                    buytext.text = "OWNED";
                    checkmark.SetActive(false);

                }
                else
                {
                    checkmark.SetActive(true);
                    costs.text = "2000";
                    buytext.text = "buy";
                    stone.text = "30";
                    trophy.text = "7";
                }
                cost = 1500;
                break;
            
        }
    }
    public void addshown()
    {
        vehcial.SetActive(true);
        money_needed.SetActive(false);
    }

}
