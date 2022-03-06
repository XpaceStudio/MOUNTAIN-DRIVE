using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitioneffect : MonoBehaviour {

    public Animator effect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void endanim()
    {
        effect.SetTrigger("start");
    }
    public void loadlevel()
    {
        FindObjectOfType<gamemanagar>().menu();
    }
}
