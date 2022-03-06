using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class complete : MonoBehaviour {

    bool levelcomplete;
    public GameObject missioncomplete,game,dropship;
    public AudioSource levelcompleted,dropplay;
    public GameObject player,dropcamera;
    public Animator drop;
    public Camera dropcam;

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (levelcomplete == true)
        {
            if (other.tag == "player")
            {
                game.SetActive(false);
                dropcamera.SetActive(true);
                dropcam.depth = 2;
                dropship.SetActive(true);
                drop.SetTrigger("level");
                FindObjectOfType<playercon>().textto();
                FindObjectOfType<playercon>().gamepaused();
                dropplay.Play();

            }
        }
    }

    public void completeanim()
    {
        missioncomplete.SetActive(true);
        levelcompleted.Play();
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void done()
    {
        levelcomplete = true;
    }
    public void playeroff()
    {
        player.SetActive(false);
    }
}
