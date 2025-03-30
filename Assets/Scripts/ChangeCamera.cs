using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public LapTimeManager ltmanager;
    public List<GameObject> cameras;
    int currentCameraIndex = 0;

    //Passem a la seguent camera
    public void SeguentCamera(){
        cameras[currentCameraIndex].SetActive(false);
        cameras[currentCameraIndex].GetComponent<Camera>().targetDisplay = 1;

        currentCameraIndex++;
        if(currentCameraIndex >= cameras.Count) currentCameraIndex = 0;

        cameras[currentCameraIndex].GetComponent<Camera>().targetDisplay = 0;
        cameras[currentCameraIndex].SetActive(true);
    }

    //Passem a l'anterior camera
    public void AnteriorCamera(){
        cameras[currentCameraIndex].SetActive(false);
        cameras[currentCameraIndex].GetComponent<Camera>().targetDisplay = 1;
        
        currentCameraIndex--;
        if(currentCameraIndex < 0) currentCameraIndex = cameras.Count -1;

        cameras[currentCameraIndex].GetComponent<Camera>().targetDisplay = 0;
        cameras[currentCameraIndex].SetActive(true);
    }

    //Deixem les cameres en el seu estat original
    public void ResetCameras(){
        for(int i=0; i<cameras.Count; i++){
            cameras[i].SetActive(false);
            cameras[currentCameraIndex].GetComponent<Camera>().targetDisplay = 1;
        }

        cameras[0].SetActive(true);
    }
}
