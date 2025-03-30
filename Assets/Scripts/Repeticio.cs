using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeticio : MonoBehaviour
{
    //Variables
    private List<Vector3> lapPosCotxe1 = new List<Vector3>();
    private List<Quaternion> lapRotCotxe1 = new List<Quaternion>();
    private List<Vector3> lapPosCotxe2 = new List<Vector3>();
    private List<Quaternion> lapRotCotxe2 = new List<Quaternion>();  
    Coroutine repeticio;
    bool dosCotxes = false;

    //Elements externs
    public Camera cam;
    public GameObject ghostPrefab, ghostPrefab2, car;

    //Establir valors de posicio/rotació de la cursa anterior dels dos cotxes
    public void SetValues(List<Vector3> _lapPosCotxe1, List<Quaternion> _lapRotCotxe1, List<Vector3> _lapPosCotxe2, List<Quaternion> _lapRotCotxe2, bool _dosCotxes){
        dosCotxes = _dosCotxes;
        lapPosCotxe1 = new List<Vector3>(_lapPosCotxe1);
        lapRotCotxe1 = new List<Quaternion>(_lapRotCotxe1);
        if(dosCotxes){
            lapPosCotxe2 = new List<Vector3>(_lapPosCotxe2);
            lapRotCotxe2 = new List<Quaternion>(_lapRotCotxe2);
        }
    }

    public void PlayRepeticio()
    {
        cam.targetDisplay = 0;
        car.SetActive(false);
        ghostPrefab.SetActive(true);
        if(dosCotxes) ghostPrefab2.SetActive(true);
        repeticio = StartCoroutine(PlayRepeticioCursa());
    }

    //Mostrem la repetició
    private IEnumerator PlayRepeticioCursa()
    {
        //Asignem les posicions i rotacions dels cotxes per simular la cursa
        for (int i = 0; i < lapPosCotxe1.Count; i++)
        {
            ghostPrefab.transform.position = lapPosCotxe1[i];
            ghostPrefab.transform.rotation = lapRotCotxe1[i];

            if(dosCotxes){
                if(i < lapPosCotxe2.Count) ghostPrefab2.transform.position = lapPosCotxe2[i];
                if(i < lapPosCotxe2.Count) ghostPrefab2.transform.rotation = lapRotCotxe2[i];
            }

            yield return null;
        }
    }

    //Funció que es crida en clicar el boto d'exit i sortir de la repetició
    public void ExitRepeticio(){
        ghostPrefab.SetActive(false);
        ghostPrefab2.SetActive(false);
        StopCoroutine(repeticio);
        cam.targetDisplay = 1;
        car.SetActive(true);
    }
}
