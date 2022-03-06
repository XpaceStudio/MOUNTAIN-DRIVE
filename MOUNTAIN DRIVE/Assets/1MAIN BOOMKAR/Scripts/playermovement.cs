using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour 
{
    private float m_steeringangle;
    private float m_horizontalInput;
    private float m_verticalInput;

    public WheelCollider FrontDriverW, FrontPassengerW, ReadDriverW, RearPassengerw;
    public Transform FrontDriverT, FrontPassengerT, ReadDriverT, RearPassengerT;
    public float maxsteerAngle = 300;
    public float motorforce = 5000;

    public void Fixedupdate()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        Accelerate();
        steer();
        UpdateWheelPoses();
    }
    private void steer()
    {
        m_steeringangle = maxsteerAngle * m_horizontalInput;
        FrontDriverW.steerAngle = m_steeringangle;
        FrontPassengerW.steerAngle = m_steeringangle;
    }
    private void Accelerate()
    {
        FrontPassengerW.motorTorque = motorforce;
        FrontDriverW.motorTorque = motorforce;
        RearPassengerw.motorTorque = motorforce;
        ReadDriverW.motorTorque = motorforce;
    }
    private void UpdateWheelPoses()
    {
        updatewheelPos(FrontDriverW, FrontDriverT);
        updatewheelPos(FrontPassengerW, FrontPassengerT);
        updatewheelPos(RearPassengerw, RearPassengerT);
        updatewheelPos(ReadDriverW, ReadDriverT);

    }
    private void updatewheelPos( WheelCollider wheelCollider,Transform transform)
    {
        Vector3 _pos = transform.position;
        Quaternion _quat = transform.rotation;

        wheelCollider.GetWorldPose(out _pos, out _quat);
        transform.position = _pos;
        transform.rotation = _quat;
    }

}
