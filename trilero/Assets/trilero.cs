using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class trilero : MonoBehaviour {
    public bool vuelta;
    public bool decision;
    public Transform[] puntos;
    public Transform[] cartas;
    public float grados;
    public float velocidad ;
    public float contador=0;
    public Vector3[]posicion;
    
    public int[,] movi;
    public bool cambiogiro;

    public int cart;
    public int donde;

    public bool turno;

    public int nivel = 1;
    public int jugadas = 3;
    public int nujugadas = 0;
    public GameObject botono;
    public int ganado = 0;
    public bool gana = false;
    public bool pierde = false;
    public bool intro = false;
    public Animator cubo;
    public Animation animo;

    // Use this for initialization
    void Start()
    {
        posicion = new Vector3[3] { new Vector3(-3, 0, 0),
                                    new Vector3(0, 0, 0),
                                    new Vector3(3, 0, 0)
                                  };

        movi = new int[3,2]
        {
           { 1 ,2 },
           { 0,2 },
           { 0,1 }
        };



        vuelta = false;
        decision = true;
        turno = false;
        velocidad = 400f;
        intro = true;
        botono = GameObject.Find("Boton");
        cubo = GameObject.Find("Carta2").GetComponent<Animator>();
        
            
    }
	
	// Update is called once per frame
	void Update () {


        if (intro)
        {
            //Animacion del muñeco
            cubo.SetBool("animado", true);

        }
        else
        {
            if (!gana || !pierde)
            {

                if (turno)
                {
                    if (nujugadas < jugadas)
                    {
                        if (!cambiogiro) giro(1);
                        else giro(-1);

                    }
                    else
                    {
                        turno = false;

                    }

                }
                else
                {
                    tocodedo();
                }
            }
            else
            {
                //Resultado de cuando gana o pierde
                if (gana)
                {

                }
                if (pierde)
                {

                }


            }

        }







    }


    public void tocodedo()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Carta2")
                {
                    Debug.Log("Acertaste");
                    ganado = 1;
                    subirNivel();
                    ganado = 2;
                    botono.SetActive(true);
                    gana = true;
                    intro = true;
                }
                if (hit.transform.name == "Carta1" || hit.transform.name == "Carta3")
                {
                    pierde = true;
                    intro = true;
                }
            }
        }
    }

    public void subirNivel()
    {
        nivel++;
        float porcen = velocidad * 0.2f;
        velocidad += porcen;
        jugadas++;
    }



    public void giro(int num)
    {

        if (decision)
        {
            cart= Random.Range(1,30);

            donde = Random.Range(1, 10);
            //Debug.Log(cart + " " + donde);
            if (cart < 11) cart = 0;
            if (cart > 10 && cart < 20) cart = 1;
            if (cart > 19 && cart < 31) cart = 2;
            
            if (donde < 6) donde = 0;
            if (donde >5) donde = 1;

            
           // Debug.Log("La carta " + cart + "se va a " + donde+ "     Resultado  : "+movi[cart, donde]);
            //Debug.Log("paranoya reves " + cart + "se va a" + movi[donde, cart]);
            decision = false;

        }

        if (vuelta)
        {
            
            if (contador < 180)
            {
              

                grados = velocidad * Time.fixedDeltaTime;
                contador += grados;

                int puntsuma = cart + movi[cart,donde] - 1;
                //Debug.Log("Roto en  el punto " + puntsuma + "carta "+cart+ "Hacia la carta"+movi[cart,donde]);

                if (cart == 0)
                {
                    cartas[cart].RotateAround(puntos[cart + movi[cart, donde] - 1].position, Vector3.up * num, grados);
                    cartas[movi[cart, donde]].RotateAround(puntos[cart + movi[cart, donde] - 1].position, -Vector3.up * num, grados * -1);
                    cartas[cart].rotation = Quaternion.Euler(0, 0, 0);
                    cartas[movi[cart, donde]].rotation = Quaternion.Euler(0, 0, 0);
                }

                if (cart == 1)
                {
                    cartas[cart].RotateAround(puntos[cart + movi[cart, donde] - 1].position, -Vector3.up * num, grados);
                    cartas[movi[cart, donde]].RotateAround(puntos[cart + movi[cart, donde] - 1].position, Vector3.up * num, grados * -1);
                    cartas[cart].rotation = Quaternion.Euler(0, 0, 0);
                    cartas[movi[cart, donde]].rotation = Quaternion.Euler(0, 0, 0);
                }


                if (cart == 2)
                {
                    cartas[cart].RotateAround(puntos[cart + movi[cart, donde] - 1].position, Vector3.up * num, grados);
                    cartas[movi[cart, donde]].RotateAround(puntos[cart + movi[cart, donde] - 1].position, Vector3.up * num, grados * -1);
                    cartas[cart].rotation = Quaternion.Euler(0, 0, 0);
                    cartas[movi[cart, donde]].rotation = Quaternion.Euler(0, 0, 0);
                }


            }
            else
            {
                vuelta = false;
                Transform variable;
                variable = cartas[cart];
                cartas[cart] = cartas[movi[cart, donde]];
                cartas[movi[cart, donde]] = variable;
                nujugadas++;
            
                reajusteposi();

                //cambio
                if (nujugadas < jugadas)
                {
                    turno = true;
                    
                        cambiogiro = !cambiogiro;
                        vuelta = true;
                        contador = 0;
                        decision = true;
                    }
            }
        }


    }







    public void reiniciar()
    {
        turno = true;


        vuelta = true;
        contador = 0;
        decision = true;
        nujugadas = 0;
        nivel = 1;
        velocidad = 400f;

    }   

    public void reajusteposi()
    {
        cartas[0].position = posicion[0];
        cartas[1].position = posicion[1];
        cartas[2].position = posicion[2];
    }

    public void boton()
    {
        turno = true;
        cubo.SetBool("animado", false);
        intro = false;
        gana = false;
        if (!vuelta)
        {
            cambiogiro = !cambiogiro;
            vuelta = true;
            contador = 0;
            decision = true;
            nujugadas = 0;
        }
        botono.SetActive(false);
    }
}
