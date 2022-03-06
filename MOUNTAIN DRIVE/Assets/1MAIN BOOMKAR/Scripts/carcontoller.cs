using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class carcontoller : MonoBehaviour
{

    [SerializeField]
    private Rigidbody Rigidbody;
    [SerializeField]
    public AnimationCurve enginepower;

    public GameObject[] body;
    public WheelCollider[] car1 = new WheelCollider[4];
    public WheelCollider[] car2 = new WheelCollider[4];
    public WheelCollider[] car3 = new WheelCollider[4];
    public WheelCollider[] car4 = new WheelCollider[4];
    [SerializeField]
    private WheelCollider[] wheels= new WheelCollider[4];
    public GameObject[] wheelmesh_1 = new GameObject[4];
    public GameObject[] wheelmesh_2 = new GameObject[4];
    public GameObject[] wheelmesh_3 = new GameObject[4];
    public GameObject[] wheelmesh_4 = new GameObject[4];
    public float[] gears = new float[5];
    private inputmanager inputmanager;


    public GameObject centerofmass;
    public GameObject needle;


    public Text speedtext;
    public Text geartext;
    private bool isreverse;
    private bool grounded;
    private bool checkground;
    public bool inwater;

    [SerializeField]
     private int boostpower;
    [SerializeField]
    private float totalpower;
    [SerializeField]
    private float wheelsrpm;
    [SerializeField]
    private float radius;
    [SerializeField]
    public float downForcevalue;
    [SerializeField]
    public float breakpower;
    [SerializeField]
    private int gearnum;
    [SerializeField]
    private float smoothTime;
    [SerializeField]
    private float enginerpm;
    [SerializeField]
    private float MaxRPM;
    [SerializeField]
    private float MinRpm;
    [SerializeField]
    private float startpos;
    [SerializeField]
    private float endpos;
    [SerializeField]
    private float KPH;
    private float desiredpos;
    private float vehcicalspeed;
    private float timeinterval;
    private int selectedcar;
    public string carname;

    [SerializeField]
    private ParticleSystem[] boostersmoke;

    [SerializeField]
    private Volume Volume;
    [SerializeField]
    private List<VolumeProfile> volumeProfiles;
    // Start is called before the first frame update
    void Awake()
    {
        inputmanager = GetComponent<inputmanager>();
        selectedcar = PlayerPrefs.GetInt("car", 0);
        carname = gameObject.name;
        getobject();
    }
    private void Start()
    {
        switch (selectedcar)
        {
            case 0:
                wheels = car1;
                break;
            case 1:
                wheels = car2;
                break;
            case 2:
                wheels = car3;
                break;
            case 3:
                wheels = car4;
                break;
        }
        body[selectedcar].SetActive(true);
    }
    private void Update()
    {
        speedtext.text = Mathf.Round(Rigidbody.velocity.magnitude * 10).ToString();
        if (isreverse)
            geartext.text = "R";
        else
            geartext.text = (gearnum + 1).ToString();

        if(inwater==true)
        {
            Volume.profile = volumeProfiles[1];
        }
        else
        {
            if (checkground == false)
            {
                Volume.profile = volumeProfiles[2];
            }
            else
            {
                Volume.profile = volumeProfiles[0];
            }
        }

    }
    public void waterin()
    {
        inwater = true;
    }
    public void waterout()
    {
        inwater = false;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        addownforce();
        animatewheels();
        Movewheel();
        steering();
        calculateEnginepower();
        shiftgear();
        isgrounded();
        updateneedle();
        shootraycast();
    }
    
    void shootraycast()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit,1);
        if(hit.collider!=null)
        {
            if (hit.collider.tag == "ground")
            {
                checkground = true;
            }
            else
            {
                checkground = false;
            }

        }
        else
            checkground = true;

    }
    void updateneedle()
    {
        vehcicalspeed = KPH;
        desiredpos = startpos - endpos;
        float temp = vehcicalspeed / 190;
        needle.transform.eulerAngles = new Vector3(0, 0, (startpos - temp * desiredpos));
    }
    void shiftgear()
    {
        timeinterval += Time.deltaTime;
        if (enginerpm > MaxRPM && gearnum < gears.Length - 1 && !isreverse && grounded && timeinterval>2)
        {
            timeinterval = 0;
                gearnum++;
        }
        if(enginerpm< MinRpm)
        {
            gearnum--;
            if( gearnum < 0)
            {
                gearnum = 0;
            }
        }
    }
    private void isgrounded()
    {
        if (wheels[0].isGrounded && wheels[1].isGrounded && wheels[2].isGrounded && wheels[3].isGrounded)
            grounded = true;
        else
            grounded = false;
    }
    void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; i++)
        {
            sum += wheels[i].rpm;
            R++;
        }
        wheelsrpm = (R != 0) ? sum / R : 0;
        if(wheelsrpm <0 && !isreverse)
        {
            isreverse = true;

        }
        else if(wheelsrpm >0 && isreverse)
        {
            isreverse = false;
        }
    }
    private void calculateEnginepower()
    {
        wheelRPM();
        totalpower = enginepower.Evaluate(enginerpm) * (gears[gearnum]) * inputmanager.vertical;
        float velocity = 0.0f;
        enginerpm = Mathf.SmoothDamp(enginerpm, 1000 + (Mathf.Abs(wheelsrpm)*3.6f * (gears[gearnum])), ref velocity, smoothTime);
    }
    void addownforce()
    {
        Rigidbody.AddForce(-transform.up * downForcevalue * Rigidbody.velocity.magnitude);
    }
    void steering()
    {
        if (inputmanager.horizontal > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * inputmanager.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * inputmanager.horizontal;
        }
        else if (inputmanager.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * inputmanager.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * inputmanager.horizontal;
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }
     void Movewheel()
    {
    if (inputmanager.handbrake)
    {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = breakpower;
                wheels[i].motorTorque = 0;
            }
    }
    else
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].brakeTorque = 0;
        }
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque =  (totalpower / 4);
        }
    }
        KPH = Rigidbody.velocity.magnitude * 3.6f;
        if(inputmanager.boosting)
        {
            Rigidbody.AddRelativeForce(Vector3.forward * boostpower, ForceMode.Acceleration);
            for (int i = 0; i < boostersmoke.Length; i++)
            {
                boostersmoke[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < boostersmoke.Length; i++)
            {
                boostersmoke[i].Stop();
            }
        }
    }
    void animatewheels()
    {
        Vector3 wheelpostion = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;
        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelpostion, out wheelRotation);
            switch(selectedcar)
            {
                case 0:
                    wheelmesh_1[i].transform.position = wheelpostion;
                    wheelmesh_1[i].transform.rotation = wheelRotation;
                    break;
                case 1:
                    wheelmesh_2[i].transform.position = wheelpostion;
                    wheelmesh_2[i].transform.rotation = wheelRotation;
                    break;
                case 2:
                    wheelmesh_3[i].transform.position = wheelpostion;
                    wheelmesh_3[i].transform.rotation = wheelRotation;
                    break;
                case 3:
                    wheelmesh_4[i].transform.position = wheelpostion;
                    wheelmesh_4[i].transform.rotation = wheelRotation;
                    break;
            }

        }
    }
    private void getobject()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.centerOfMass = centerofmass.transform.localPosition;
    }
    public void booston()
    {
        inputmanager.boosting = true;
    }
    public void bootoff()
    {
        inputmanager.boosting = false;
    }

}
