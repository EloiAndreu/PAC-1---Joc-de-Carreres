using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCarretera : MonoBehaviour
{
    public GameManager gm;

    //Permet stablir en el gameManager per quina part del circuit passa el cotxe
    void OnTriggerEnter(Collider coll){
        if(coll.tag == "Car"){
            gm.CarIntoTrigger();
        }
    }
}
