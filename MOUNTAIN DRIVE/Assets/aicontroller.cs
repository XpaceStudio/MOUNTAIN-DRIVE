using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aicontroller : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Rigidbody;
    [SerializeField]
    public AnimationCurve enginepower;

    [SerializeField]
    private ParticleSystem[] boostersmoke;

    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelmesh = new GameObject[4];
    public float[] gears = new float[5];
    private aiinput inputmanager;
    public GameObject centerofmass;

    public string carname;
    [SerializeField]
    private float KPH;
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
    private float timeinterval;
    private float spawninginterval = 0;
    private float checkinterval = 2;
    private float collisioncheck = 2;
    private bool isspawning = false;

    [Range(0, 17)] public float countDowntime;
    private float timeleft;
    private float boostimer=0;
    // Start is called before the first frame update
    void Awake()
    {
        carname = gameObject.name;
        getobject();
        timeleft = (float)countDowntime;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        timeleft -= Time.deltaTime;
        if (!calculatecountdowntime()) return;
        addownforce();
        animatewheels();
        Movewheel();
        steering();
        calculateEnginepower();
        shiftgear();
        shootraycast();
        breakcheck();
        KPH = Rigidbody.velocity.magnitude * 3.6f;
    }
    void breakcheck()
    {
        if (KPH > 30 && (Mathf.Abs(inputmanager.horizontal) > 2f))
        {
            inputmanager.handbrake = true;
        }
        else
            inputmanager.handbrake = false;
    }
    private void Update()
    {
        if (isspawning == true)
        {
            gameObject.layer = 8;
            spawninginterval += Time.deltaTime;
            if (spawninginterval > 3)
            {
                isspawning = false;
                spawninginterval = 0;
            }
        }
        else
            gameObject.layer = 0;

        if(Rigidbody.velocity.magnitude<0.1f)
        {
            bool check = false;
            collisioncheck -= Time.deltaTime;
            if (collisioncheck <= 0)
                check = true;
            if(check==true)
            {
                inputmanager.aispwan();
                collisioncheck = 2;
            }
        }
    }

    void shiftgear()
    {
        timeinterval += Time.deltaTime;
        if (enginerpm > MaxRPM && gearnum < gears.Length - 1 && timeinterval > 2)
        {
            timeinterval = 0;
            gearnum++;
        }
        if (enginerpm < MinRpm)
        {
            gearnum--;
            if (gearnum < 0)
            {
                gearnum = 0;
            }
        }
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
    }
    private void calculateEnginepower()
    {
        wheelRPM();
        totalpower = enginepower.Evaluate(enginerpm) * (gears[gearnum]) * inputmanager.vertical;
        float velocity = 0.0f;
        enginerpm = Mathf.SmoothDamp(enginerpm, 1000 + (Mathf.Abs(wheelsrpm) * 3.6f * (gears[gearnum])), ref velocity, smoothTime);
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
                wheels[i].motorTorque = (totalpower / 4);
            }

        }


        if (inputmanager.boosting)
        {
            Rigidbody.AddRelativeForce(Vector3.forward * 14, ForceMode.Acceleration);
            boostimer += Time.deltaTime;
            if(boostimer>2)
            {
                inputmanager.boosting = false;
                boostimer = 0;
            }
            for (int i = 0; i < boostersmoke.Length; i++)
            {
            boostersmoke[i].Play();
            }
        }
        else
        {
            for (int i = 0; i<boostersmoke.Length; i++)
            {
                boostersmoke[i].Stop();
            }
        }

    }
    public void booston()
    {
        inputmanager.boosting = true;
    
    }
    private void getobject()
    {
        inputmanager = GetComponent<aiinput>();
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.centerOfMass = centerofmass.transform.localPosition;
    }
    void animatewheels()
    {
        Vector3 wheelpostion = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;
        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelpostion, out wheelRotation);
            wheelmesh[i].transform.position = wheelpostion;
            wheelmesh[i].transform.rotation = wheelRotation;
        }
    }

    void shootraycast()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 1);
        if (hit.collider != null)
        {
            if (hit.collider.tag != "ground")
            {
                bool check = false;
                checkinterval -= Time.deltaTime;
                if (checkinterval <= 0)
                    check = true;
                if (inputmanager.previouswaypoint != null && check == true)
                {
                    gameObject.transform.position = inputmanager.previouswaypoint.position;
                    Rigidbody.velocity = new Vector3(0, 0, 0);
                    isspawning = true;
                    checkinterval = 2;
                }
            }
        }


    }
    private bool calculatecountdowntime()
    {
        return (timeleft <= 0) ? true : false;
    }
}
