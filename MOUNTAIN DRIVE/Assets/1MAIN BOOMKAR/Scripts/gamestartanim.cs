using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamestartanim : MonoBehaviour {

    public GameObject player;
    public GameObject gamecanvas;
    public GameObject dropcamera;

	
	// Update is called once per frame
	void Update () {
		
	}

    public void activee()
    {
        player.SetActive(true);
        dropcamera.SetActive(false);
    }

    public void levelcomplete()
    {
        gamecanvas.SetActive(false);
        dropcamera.SetActive(true);
    }
    public void gamecanvason()
    {
        gamecanvas.SetActive(true);
    }
    public void playeroff()
    {
        FindObjectOfType<complete>().playeroff();
    }
    public void animationcompleted()
    {
        Debug.Log("animation complete");
        gameObject.SetActive(false);
    }

}
