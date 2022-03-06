using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoptoplayer : MonoBehaviour {

    public GameObject[] cars;
	public Material[] skins;
	public GameObject[] body;
    int selectedcar, selectedskins;
	private SkinnedMeshRenderer SkinnedMeshRenderer;

	// Use this for initialization
	void Start () {
		selectedcar = PlayerPrefs.GetInt("car", 0);
		selectedskins= PlayerPrefs.GetInt("Skin", 0);
		cars[selectedcar].SetActive(true);
		SkinnedMeshRenderer=body[selectedcar].transform.GetComponent<SkinnedMeshRenderer>();
		SkinnedMeshRenderer.material = skins[selectedskins];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
