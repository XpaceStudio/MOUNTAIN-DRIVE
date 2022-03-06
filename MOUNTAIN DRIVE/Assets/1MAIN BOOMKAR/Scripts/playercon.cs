using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playercon : MonoBehaviour
{

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;

    }
    public List<AxleInfo> axleInfos;
    Rigidbody rb;

    
    public float maxMotorTorque;
    private float maxSteeringAngle;
    private float minsteeringAngle;
    public float maxBreakTorque;
    private float heighestspeed;
    private float speedfactor, currentsteer;

    private bool turningleft;
    private bool turningright;
    private bool isbreaking;
    private bool isaccelrating;
    private bool isacid;
    private bool isreverse;
    private bool onlyonce;
    private bool onlyonceaudio;
    private bool onlyoncecrash;
    private bool onlyoncefuel;


   // public GameObject finalindicator;
    public GameObject missionfailed, game;
    //public GameObject watersplash;

    public TextMeshProUGUI cointext,cointext2;
    public TextMeshProUGUI[] coinscollected;

    private int moneycollected;
    private int money;
    private int vibrate;

    public AudioSource[] engine;
    public AudioSource carbreaks;
    public AudioSource carcrash;
    public AudioSource coinsound;
    public AudioSource levelfailed;
    public AudioSource water;
    public AudioSource splash_audio;
    private Vector3 oldvelocity;

    int turn, forward, back,skin;
    [SerializeField]
    public void Awake()
    {
        skin = PlayerPrefs.GetInt("Skin", 0);
        minsteeringAngle = 30;
        maxSteeringAngle = PlayerPrefs.GetFloat("steer", 30);
        Time.timeScale = 1;
    }

    public void Start()
    {
        money = PlayerPrefs.GetInt("gold", 0);
        heighestspeed = PlayerPrefs.GetFloat("highestspeed", 15);
        isbreaking = false;
        turningleft = false;
        turningright = false;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1f, .05f);
        vibrate = PlayerPrefs.GetInt("vibration", 1);
        engine[skin].Play();
        onlyonceaudio = false;
        onlyonce = false;
        onlyoncecrash = false;
        onlyoncefuel = false;


    }
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {

        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;

    }

    public void FixedUpdate()
    {

            speedfactor = rb.velocity.magnitude / heighestspeed;
            currentsteer = Mathf.Lerp(maxSteeringAngle, minsteeringAngle, speedfactor);
            float motor = maxMotorTorque * forward;
            float steering = currentsteer * turn;
            float breaking = maxBreakTorque;

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    if (isreverse == false)
                    {
                        if (isbreaking == false)
                        {
                            axleInfo.leftWheel.motorTorque = motor;
                            axleInfo.rightWheel.motorTorque = motor;
                            axleInfo.leftWheel.brakeTorque = 0;
                            axleInfo.rightWheel.brakeTorque = 0;
                            if (axleInfo.rightWheel.rpm < 0)
                            {
                                if (isaccelrating == true)
                                {
                                    axleInfo.leftWheel.motorTorque = 10 * motor;
                                    axleInfo.rightWheel.motorTorque = 10 * motor;
                                }

                            }
                        }
                        else if (isbreaking == true)
                        {

                            axleInfo.leftWheel.brakeTorque = breaking;
                            axleInfo.rightWheel.brakeTorque = breaking;
                            axleInfo.leftWheel.motorTorque = 0;
                            axleInfo.rightWheel.motorTorque = 0;
                            if (rb.velocity.magnitude <= 1)
                            {
                                isreverse = true;
                                forward = -1;
                            }
                        }
                    }
                    else if (isreverse == true)
                    {
                        axleInfo.leftWheel.brakeTorque = 0;
                        axleInfo.rightWheel.brakeTorque = 0;
                        axleInfo.leftWheel.motorTorque = motor;
                        axleInfo.rightWheel.motorTorque = motor;
                    }
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }
            if (rb.velocity.magnitude > heighestspeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, heighestspeed);
            }


    }
    public void Update()
    {
        
        cointext.text = money.ToString();
        cointext2.text = money.ToString();
        
            if (turningleft == true && turningright == false)
            {
                turn = -1;
            }
            else if (turningright == true && turningleft == false)
            {
                turn = 1;
            }
            else if (turningleft == false && turningright == false)
            {
                turn = 0;
            }

        if (isacid == true)
        {

              //  gameover();
            
        }
        else if(isacid==false)
        {
           // water.Stop();
        }
        playaudio();
       
    }
    public void leftturn()
    {
        turningleft = true;
    }
    public void rightturn()
    {
        turningright = true;
    }
    public void straight()
    {
        turningleft = false;
        turningright = false;
    }
    public void tirebreak()
    {

        isbreaking = true;
        if (!carbreaks.isPlaying)
            carbreaks.Play();
    }
    public void tirebreakoff()
    {

        isbreaking = false;
        isreverse = false;
        forward = 0;
        carbreaks.Stop();
    }
    public void booston()
    {
        forward = 1;
        isaccelrating = true;
    }
    public void boostoff()
    {
        forward = 0;
        isaccelrating = false;
    }
    public void acidin()
    {
        isacid = true;
        //watersplash.SetActive(true);
        if(!water.isPlaying)
        {
            water.Play();
        }
        if(onlyonce==false)
        {
            splash_audio.Play();
            onlyonce = true;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindObjectOfType<tutorial>().tutorial4active();
        }
    }
    public void acidout()
    {
        isacid = false;
       // watersplash.SetActive(false);
        water.Stop();
        onlyonce = false;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            FindObjectOfType<tutorial>().tutorial5active();
        }
    }
    public void increasegold()
    {
        money++;
        moneycollected++;
        PlayerPrefs.SetInt("gold", money);
        if(!coinsound.isPlaying)
        {
            coinsound.Play();
        }

    }
    public void gamepaused()
    {
        Time.timeScale = 0;
        engine[skin].Stop();
    }
    public void gameison()
    {
        Time.timeScale = 1;
        engine[skin].Play();

    }
    public void gameover()
    { 
        game.SetActive(false);
        missionfailed.SetActive(true);
        if (!levelfailed.isPlaying)
        {
            if (onlyonceaudio == false)
            {

              //  FindObjectOfType<unityads>().showad();
                textto();
                levelfailed.Play();
                onlyonceaudio = true;
            }
        }
        rb.mass = 100000;
        gamepaused();
      //  FindObjectOfType<unityads>().bannerad();

    }

    public void playaudio()
    {
        if (speedfactor < .1)
            speedfactor = .1f;
        engine[skin].pitch = speedfactor;
    }

    public void doubleincreasegold()
    {
        money += 10;
        moneycollected += 10;
        PlayerPrefs.SetInt("gold", money);
        if (!coinsound.isPlaying)
        {
            coinsound.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "buidling")
        {
            Debug.Log("budidling");
            if (onlyoncecrash == false)
            {
                if (!carcrash.isPlaying)
                {
                    carcrash.Play();
                    carcrash.volume = speedfactor;
                }
                onlyoncecrash = true;
            }
        }
        else if(other.tag=="water")
        {
            acidin();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water")
        {
            acidout();
        }
        else if (other.tag == "buidling")
        {
            onlyoncecrash = false;
        }
    }
    public void doublecoin()
    {
        money += moneycollected;
        PlayerPrefs.SetInt("gold", money);
    }
    public void revived()
    {
        game.SetActive(true);
        missionfailed.SetActive(false);
        onlyonceaudio = false;
        rb.mass = 3000;
        gameison();
    }
    public void playerstationary()
    {
        Time.timeScale = 0;
    }
    public void textto()
    {
        for (int i = 0; i < coinscollected.Length; i++)
        {
            coinscollected[i].text = moneycollected.ToString();
        }

    }

}

