using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CarController carController;

    //Li definim al cotxe quan comença a tocar el terra i la carretera
    void OnTriggerEnter(Collider coll){
        if(coll.tag == "Ground"){
            carController.touchCarretera = true;
        }
        if(coll.tag == "Carretera"){
            carController.touchGround = true;
        }
    }

    //Li definim al cotxe quan deixa de tocar el terra i la carretera
    void OnTriggerExit(Collider coll){
        if(coll.tag == "Ground"){
            carController.touchCarretera = false;
        }
        if(coll.tag == "Carretera"){
            carController.touchGround = false;
        }
    }

}
