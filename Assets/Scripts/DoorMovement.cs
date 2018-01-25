using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour {

    public GameObject blue; 
    public GameObject green; 
    public GameObject red;
    public GameObject blueDoor;
    public GameObject greenDoor;
    public GameObject redDoor;
    public GameObject fakeWall;
    public GameObject fakeWallr;

    private int bcount;
    private int gcount;
    private int rcount;
    private GameObject[] objs;
    
    // Use this for initialization
    void Start () {
        bcount = 0;
        gcount = 0;
        rcount = 0;        
	}
	
	// Update is called once per frame
	void Update () {
		if(!blue.activeSelf && bcount == 0)
        {
            //blueDoor.transform.Translate(-2, 0, 0);
            blueDoor.SetActive(false);
            bcount++;
        } else if(!green.activeSelf && gcount == 0)
        {
            //greenDoor.transform.Translate(-2, 0, 0);
            greenDoor.SetActive(false);
            gcount++;
        } else if(!red.activeSelf && rcount == 0)
        {
            //redDoor.transform.Translate(2, 0, 0);
            redDoor.SetActive(false);
            rcount++;
        }
        objs = GameObject.FindGameObjectsWithTag("Chalice");

        if(objs.Length == 0)
        {
            fakeWallr.SetActive(false);
            fakeWall.SetActive(false);
        }
	}
}
