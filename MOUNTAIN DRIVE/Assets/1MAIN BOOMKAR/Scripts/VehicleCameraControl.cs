using UnityEngine;
using System.Collections;

public class VehicleCameraControl : MonoBehaviour
{

	public Transform[] playerCar;
	[Range(0, 3)] public int car;
	private Rigidbody playerRigid;
	public float distance = 10.0f;
	public float height = 5.0f;
	private float defaultHeight = 0f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public float defaultFOV = 60f;
	public float zoomMultiplier = 0.3f;
	public GameObject[] effects;
	//read only
		
	void Start(){
		
		playerRigid = playerCar[car].GetComponent<Rigidbody>();
		FindObjectOfType<inputmanager>().go();
		for (int i = 0; i < effects.Length; i++)
		{
			effects[i].SetActive(true);
		}
	}
	
	void Update(){
		if(playerRigid != playerCar[car].GetComponent<Rigidbody>())
			playerRigid = playerCar[car].GetComponent<Rigidbody>();

		GetComponent<Camera>().fieldOfView = defaultFOV + playerRigid.velocity.magnitude * zoomMultiplier;
		
	}
	
	void FixedUpdate (){
		

		//calculates speed in local space. positive if going forward, negative if reversing
		float speed = (playerRigid.transform.InverseTransformDirection(playerRigid.velocity).z) * 3f;
		speed = Mathf.Abs(speed);
		
		// Calculate the current rotation angles.
		Vector3 wantedRotationAngle = playerCar[car].eulerAngles;
		float wantedHeight = playerCar[car].position.y + height;
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		
		if(speed < -5)
			wantedRotationAngle.y = playerCar[car].eulerAngles.y + 180;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle.y, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = playerCar[car].position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight + defaultHeight, transform.position.z);

		// Always look at the target
		transform.LookAt (playerCar[car]);


	}

}