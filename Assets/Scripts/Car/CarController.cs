using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Propietats del cotxe
    public float speed = 10f;
    public float turnSpeed = 50f;

    //Inputs
    private float moveInput;
    private float turnInput;

    //Altres elements
    public GameObject spawnPoint;
    public Rigidbody rb;

    //Variables
    public bool touchCarretera = true;
    public bool touchGround = true;
    public bool active = false;

    void Start(){
        ResetCar();
    }

    //Reinicia la posició i rotació del cotxe
    public void ResetCar(){
        transform.position = spawnPoint.transform.position;
        transform.rotation = spawnPoint.transform.rotation;
    }


    void Update(){
        if(active){
            // Obtenir les entrades d'Inputs
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");

            //En cas que el cotxe estigui per sota la posició -5 de l'eix y reiniciem la posició.
            if(this.transform.position.y <= -5f || Input.GetKey(KeyCode.Space)) ResetCar();
        }
    }

    void FixedUpdate(){
        if(active){ //En cas que el cotxe estigui actiu...
            float muliplicador = 1f;
 
            if(touchCarretera){ // Si toca la carretera (velocitat normal)
                muliplicador = 1f;
            }
            else{ // Si no toca la carretera (velocitat a la meitat)
                muliplicador = 0.5f;
            }

            if(touchCarretera || touchGround){ //Només permetem moure si toca al terra o la carretera
                
                //Moviment
                transform.Translate(Vector3.forward * moveInput * speed * Time.deltaTime * muliplicador);
                rb.AddForce(transform.forward * moveInput * speed * 0.3f, ForceMode.Acceleration);
                
                //Rotació
                transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
            }
        }
    }
}
