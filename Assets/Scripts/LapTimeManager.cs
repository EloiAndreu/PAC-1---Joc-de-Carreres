using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class LapTimeManager : MonoBehaviour
{
    //Variables
    public float bestLapTime = Mathf.Infinity; // Inicialment cap volta ha estat registrada
    private List<Vector3> lapPositions = new List<Vector3>();
    private List<Quaternion> lapRotations = new List<Quaternion>(); 
    private List<Vector3> bestLapPositions = new List<Vector3>(); 
    private List<Quaternion> bestLapRotations = new List<Quaternion>();
    private bool isLapActive = false; // Si la volta actual està en curs
    bool cotxeFantasma = false;
    public bool dosCotxes = false;

    //Elements externs
    public TMP_Text bestTimeText;
    public GameObject carController; // Referència al controlador del cotxe
    public GameObject ghostPrefab;
    public Repeticio repeticio;
    public AudioListener audioList;

    void Start(){
        bestTimeText.text = "BEST TIME: NULL";
        ghostPrefab.SetActive(false);
    }

    void Update(){
        if (isLapActive){ //Si ha começat la cursa... anem guardant posicions i rotacions del cotxe
            lapPositions.Add(carController.transform.position);
            lapRotations.Add(carController.transform.rotation);
        }
    }

    //Establim els valors per permetre la repetició
    public void SetValuesRepeticio(){
        repeticio.SetValues(lapPositions, lapRotations, bestLapPositions, bestLapRotations, dosCotxes);
        dosCotxes = true;
    }

    //En acabar la volta.. comprovem resultats
    public void EndLap(float timeLap)
    {
        ghostPrefab.SetActive(false);
        audioList.enabled = true;
        isLapActive = false; // Fi de la volta

        // Comprovar si és la millor volta
        if (timeLap < bestLapTime){

            cotxeFantasma = true;
            bestLapTime = timeLap; // Actualitzar el millor temps
            bestLapPositions = new List<Vector3>(lapPositions);
            bestLapRotations = new List<Quaternion>(lapRotations);

            int minutes = Mathf.FloorToInt(bestLapTime / 60f);
            int seconds = Mathf.FloorToInt(bestLapTime % 60f);
            int milliseconds = Mathf.FloorToInt((bestLapTime * 1000f) % 1000f);
            bestTimeText.text = "BEST TIME: " + string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);
        }
    }

    public void StartLap(){
        // Netejar les dades de la volta anterior
        lapPositions.Clear();
        lapRotations.Clear();
        isLapActive = true; // Començar una nova volta

        // Si ja s'ha registrat una cursa anterior... mostrem fantasma
        if(cotxeFantasma){
            PlayGhostCar();
        }
    }

    public void PlayGhostCar(){
        // Crear el "fantasma" i reproduir-lo
        audioList.enabled = false;
        ghostPrefab.SetActive(true);
        StartCoroutine(PlayGhost());
    }

    //Mostrem fantasma amb les posicions / rotacions de la millor cursa
    private IEnumerator PlayGhost(){
        for (int i = 0; i < bestLapPositions.Count; i++){
            ghostPrefab.transform.position = bestLapPositions[i];
            ghostPrefab.transform.rotation = bestLapRotations[i];

            yield return null;
        }
    }
}
