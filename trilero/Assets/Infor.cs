using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infor : MonoBehaviour {
    public Text inf;
    public trilero tri;
    public bool borro;
	// Use this for initialization
	void Start () {
        tri = GameObject.Find("Gamemanager").GetComponent<trilero>();
        inf = GameObject.Find("Texto").GetComponent<Text>();
        borro = false;
	}
	
	// Update is called once per frame
	void Update () {

        inf.text = "Nivel :" + tri.nivel + "\n";

 

        if (tri.gana)
        {
            inf.text = "yeah sigue con el siguiente";
            borro = true;
        }
        if (tri.pierde)
        {
            inf.text = "Lacagaste";
            borro = true;
        }


       

        if (borro)
        {
            inf.text = "";
            borro = false;
        }

       

    

    }
}
