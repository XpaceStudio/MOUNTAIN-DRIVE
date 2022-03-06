using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class vehicle
{
    public int node;
    public string name;
    public vehicle(int node, string name)
    {
        this.node = node;
        this.name = name;
    }
}

public class gamemanagar : MonoBehaviour {


    [SerializeField]
    public List<vehicle> presentvehicales;

    [SerializeField]
    public TextMeshPro[] postions;

    [SerializeField]
    public carcontoller carcontoller;

    public GameObject[] levels;
    private List<GameObject> temporaryList;
    [SerializeField]
    public GameObject[] presentGameojectVehicals; // array of ai car driver
    public GameObject[] fullarray; // all the vehical inclduing player

    public GameObject levelfailed_panel;
    public GameObject levelfinished_panel;
    public GameObject game_panel;
    public GameObject pausecamera;
    public GameObject watereffect;

    [SerializeField]
    public Slider fuel;
    [SerializeField]
    public Slider health;

    [SerializeField]
    public List<GameObject> checkpoint = new List<GameObject>();
    [SerializeField]
    public TextMeshProUGUI playerpos;
    [SerializeField]
    public GameObject player;
    private Rigidbody rb;
    [SerializeField]
    public inputmanager inputmanager;

    private int activelevel;
    private Quaternion Quaternion;

    private Quaternion Quaternion_A1;
    private Quaternion Quaternion_A2;
    private Quaternion Quaternion_A3;
    private Vector3 Postion_A1;
    private Vector3 Postion_A2;
    private Vector3 Postion_A3;


    private bool brakes;
    private bool left = false;
    private bool right = false;
    private string pos;

    public float rotationspeed;
    private void Awake()
    {
        presentvehicales = new List<vehicle>();
        foreach (GameObject R in presentGameojectVehicals)
            presentvehicales.Add(new vehicle(R.GetComponent<aiinput>().currentnode, R.GetComponent<aicontroller>().carname));

        presentvehicales.Add(new vehicle(player.gameObject.GetComponent<inputmanager>().currentnode, player.gameObject.GetComponent<carcontoller>().carname));
        temporaryList = new List<GameObject>();
        foreach (GameObject R in presentGameojectVehicals)
            temporaryList.Add(R);
        temporaryList.Add(player.gameObject);
        fullarray = temporaryList.ToArray();
        rb = player.GetComponent<Rigidbody>();
        carcontoller = carcontoller.GetComponent<carcontoller>();

        fuel.maxValue = PlayerPrefs.GetFloat("fuel", 20);
        fuel.value = fuel.maxValue;
        fuel.minValue = 0;
        health.maxValue = PlayerPrefs.GetFloat("health", 20);
        health.value = health.maxValue;
        health.minValue = 0;

    }

    public void Start()
    {
        activelevel = PlayerPrefs.GetInt("levels", 0);
        setpostion();
    }


    public void FixedUpdate()
    {
        SortArray();
        if (brakes==true)
        {
            if (rb.velocity.magnitude > 10)
                inputmanager.handbrake = true;
            else
            {
                inputmanager.vertical = -1;
                inputmanager.handbrake = false;
            }
        }
        if (left == true)
        {
            inputmanager.horizontal = Mathf.Clamp(inputmanager.horizontal - rotationspeed * Time.deltaTime, -1, 0);
        }
        else if (right == true)
        {
            inputmanager.horizontal = Mathf.Clamp(inputmanager.horizontal + rotationspeed * Time.deltaTime, 0, 1);
        }
        Debug.Log(inputmanager.horizontal);

    }
    public void Update()
    {
        if (rb.velocity.magnitude>10 || rb.velocity.magnitude <-10)
        {
            fuel.value -= Time.deltaTime;
        }
        if (fuel.value == 0)
        {
            Debug.Log("fuel low");
            game_panel.SetActive(false);
            levelfailed_panel.SetActive(true);
        }
        if(carcontoller.inwater)
        {
            health.value -= Time.deltaTime;
            watereffect.SetActive(true);
        }
        else
        {
            watereffect.SetActive(false);
            health.value += Time.deltaTime;
        }
        if (health.value == 0)
        {
            Debug.Log("health low");
            game_panel.SetActive(false);
            levelfailed_panel.SetActive(true);
        }
    }

    public void setpostion()
    {
        Vector3 P_pos = new Vector3(levels[activelevel].transform.position.x-1, levels[activelevel].transform.position.y, levels[activelevel].transform.position.z);
        Vector3 AI_1 = new Vector3(levels[activelevel].transform.position.x+2, levels[activelevel].transform.position.y, levels[activelevel].transform.position.z);
        Vector3 AI_2 = new Vector3(levels[activelevel].transform.position.x-3, levels[activelevel].transform.position.y, levels[activelevel].transform.position.z-3);
        Vector3 AI_3 = new Vector3(levels[activelevel].transform.position.x+4, levels[activelevel].transform.position.y, levels[activelevel].transform.position.z -3);
        player.transform.position = P_pos;
        presentGameojectVehicals[0].transform.position = AI_1;
        presentGameojectVehicals[1].transform.position = AI_2;
        presentGameojectVehicals[2].transform.position = AI_3;


    }
    private void SortArray()
    {

        for (int i = 0; i < fullarray.Length; i++)
        {
            if (i > 2)
            {
                presentvehicales[i].name = fullarray[i].GetComponent<carcontoller>().carname;
                presentvehicales[i].node = fullarray[i].GetComponent<inputmanager>().currentnode;
            }
            else
            {
                presentvehicales[i].name = fullarray[i].GetComponent<aicontroller>().carname;
                presentvehicales[i].node = fullarray[i].GetComponent<aiinput>().currentnode;
            }
        }
        for (int i = 0; i < presentvehicales.Count; i++)
        {
            for (int j = i + 1; j < presentvehicales.Count; j++)
            {
                if (presentvehicales[j].node < presentvehicales[i].node)
                {
                    vehicle qq = presentvehicales[i];
                    presentvehicales[i] = presentvehicales[j];
                    presentvehicales[j] = qq;
                }
            }
        }
        presentvehicales.Reverse();
        for (int i = 0; i < presentvehicales.Count; i++)
        {
            if (player.gameObject.GetComponent<carcontoller>().carname == presentvehicales[i].name)
            {
                playerpos.text = ((i + 1) + "/" + presentvehicales.Count).ToString();
                switch (i)
                {
                    case 0:
                        pos = "1st";
                        break;
                    case 1:
                        pos = "2nd";
                        break;
                    case 2:
                        pos = "3rd";
                        break;
                    case 3:
                        pos = "4th";
                        break;

                }
                postions[0].text = pos;
            }
            else if ("green" == presentvehicales[i].name)
            {
                switch (i)
                {
                    case 0:
                        pos = "1st";
                        break;
                    case 1:
                        pos = "2nd";
                        break;
                    case 2:
                        pos = "3rd";
                        break;
                    case 3:
                        pos = "4th";
                        break;

                }
                postions[1].text = pos;
            }
            else if ("blue" == presentvehicales[i].name)
            {
                switch (i)
                {
                    case 0:
                        pos = "1st";
                        break;
                    case 1:
                        pos = "2nd";
                        break;
                    case 2:
                        pos = "3rd";
                        break;
                    case 3:
                        pos = "4th";
                        break;

                }
                postions[2].text = pos;
            }
            else if ("light" == presentvehicales[i].name)
            {
                switch (i)
                {
                    case 0:
                        pos = "1st";
                        break;
                    case 1:
                        pos = "2nd";
                        break;
                    case 2:
                        pos = "3rd";
                        break;
                    case 3:
                        pos = "4th";
                        break;

                }
                postions[3].text = pos;
            }
        }

    }

    public void checkpoint_Reached(GameObject gameObject)
    {
        checkpoint.Add(gameObject);
        Quaternion = player.transform.rotation;
        Quaternion_A1 = presentGameojectVehicals[0].transform.rotation;
        Quaternion_A2 = presentGameojectVehicals[1].transform.rotation;
        Quaternion_A3 = presentGameojectVehicals[2].transform.rotation;
        Postion_A1 = presentGameojectVehicals[0].transform.position;
        Postion_A2 = presentGameojectVehicals[0].transform.position;
        Postion_A3 = presentGameojectVehicals[0].transform.position;
        refill_fuel();
    }


    public void levelreached(Transform gameObject)
    {
        if(checkpoint.Count<1)
        {
            return;
        }
        for (int i = 0; i < levels.Length; i++)
        {
            if(gameObject == levels[i].transform.parent)
            {

                if(activelevel<i)
                {
                     PlayerPrefs.SetInt("levels", i);
                    activelevel = i;
                    Quaternion = player.transform.rotation;
                }
            }
        }
        levelfinished();
    }
    public void levelfinished()
    {
        StartCoroutine("playercam");
        GameObject.Find("Camera").GetComponent<VehicleCameraControl>().enabled = false;
        checkpoint.Clear();
        Debug.Log("list clean");
        game_panel.SetActive(false);
        levelfinished_panel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void refill_fuel()
    {
        fuel.value = fuel.maxValue;
    }

    public void spawn()
    {
        int last = checkpoint.Count;
        if(last>0)
        {
            Vector3 vector3 = new Vector3(checkpoint[last - 1].transform.position.x, checkpoint[last - 1].transform.position.y + 2, checkpoint[last - 1].transform.position.z);
            player.transform.position = vector3;
        }
        else
        {
            player.transform.position = levels[activelevel].transform.position;
        }
        player.transform.rotation = Quaternion;
        presentGameojectVehicals[0].transform.position= Postion_A1;
        presentGameojectVehicals[1].transform.position = Postion_A2;
        presentGameojectVehicals[2].transform.position = Postion_A3;
        presentGameojectVehicals[0].transform.rotation=Quaternion_A1;
        presentGameojectVehicals[1].transform.rotation = Quaternion_A2;
        presentGameojectVehicals[2].transform.rotation = Quaternion_A3;
        rb.velocity = new Vector3(0,0,0);
    }
    public void changescene(int n)
    { 
        SceneManager.LoadScene(n);
    }
    public void gamepause()
    {
        Time.timeScale = 0;
    }
    public void retry()
    {
        Time.timeScale = 1;
        if(checkpoint.Count>0)
        {
            spawn();
        }
        else
        SceneManager.LoadScene(1);
    }
    public void gameresume()
    {
        Time.timeScale = 1;
    }
    public void nextlevel()
    {
        Time.timeScale = 1;
       
    }

    public void accelrationdown()
    {
        inputmanager.vertical = 1;
    }

    public void accelrationup()
    {
        inputmanager.vertical = 0;
    }

    public void breakdown()
    {
        brakes = true;
    }
    public void breakup()
    {
        brakes = false;
        inputmanager.handbrake = false;
        inputmanager.vertical = 0;
    }

    public void leftup()
    {
        inputmanager.horizontal = 0;
        left = false;
    }
    public void leftdown()
    {
        left = true;
    }
    public void rightup()
    {
        inputmanager.horizontal = 0;
        right = false;
    }
    public void rightdown()
    {
        right = true;
    }
    public void menu()
    {
            SceneManager.LoadScene(1);
    }

    public void insta()
    {
        Application.OpenURL("https://www.instagram.com/redcherrygames/");
    }

    public void facebook()
    {
        Application.OpenURL("https://www.facebook.com/redcherrygaming");
    }


}
