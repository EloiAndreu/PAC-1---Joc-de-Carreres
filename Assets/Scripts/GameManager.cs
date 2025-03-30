using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentVoltes = 0;
    int maxVoltes = 1;

    public List<GameObject> triggersCarretera;
    public int currentTriggerActive = 1;

    public Timer timer, timer2; 
    public CarController car;
    public LapTimeManager lapTimeManager;

    public GameObject startPanel, gamePanel;
    public GameObject tempsVoltaAnt1, tempsVoltaAnt2;
    public TMP_Text textTemps1, textTemps2;
    public GameObject buttonRepeticio;

    void Start(){
        for(int i=0; i<triggersCarretera.Count; i++){
            triggersCarretera[i].SetActive(false);
        }
        triggersCarretera[currentTriggerActive].SetActive(true);

        tempsVoltaAnt1.SetActive(false); 
        tempsVoltaAnt2.SetActive(false);
        buttonRepeticio.SetActive(false);
    }

    //Funció per començar la carrera
    public void StartGame(){
        tempsVoltaAnt1.SetActive(false); 
        tempsVoltaAnt2.SetActive(false);

        //Activem el temporitzadors i el cotxe
        timer.active = true;
        timer2.active = true;
        car.active = true;

        //Començem a gravar les posicions i rotacions del cotxe
        lapTimeManager.StartLap();
    }

    public void CarIntoTrigger(){
        triggersCarretera[currentTriggerActive].SetActive(false);
        if(currentTriggerActive == 0){ //Ha fet una volta
            currentVoltes++;

            if(currentVoltes == 1){ // Si acabem de començar la 2 volta...

                //Guardem el temps de la primera
                float elapsedTime = timer2.elapsedTime;
                int minutes = Mathf.FloorToInt(elapsedTime / 60f);
                int seconds = Mathf.FloorToInt(elapsedTime % 60f);
                int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

                //Mostrem el text del temps de la primera volta
                textTemps1.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);
                tempsVoltaAnt1.SetActive(true); 
            }
            if(currentVoltes == 2){ // Si acabem de començar la 3 volta...
                
                textTemps2.text = textTemps1.text;
                float elapsedTime = timer2.elapsedTime;
                int minutes = Mathf.FloorToInt(elapsedTime / 60f);
                int seconds = Mathf.FloorToInt(elapsedTime % 60f);
                int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);
                
                //Mostrem el text del temps de la segona volta
                textTemps1.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);
                tempsVoltaAnt2.SetActive(true); 
            }

            timer2.elapsedTime = 0;

            if(currentVoltes == maxVoltes){ //Ha fet totes les voltes

                //Guardem les posicions i rotacions per permetre la repetició
                lapTimeManager.SetValuesRepeticio();
                lapTimeManager.EndLap(timer.elapsedTime); //Guardem el temps del temporitzador
                
                //Asignem les variables als seus valors originals
                timer.elapsedTime = 0; 
                currentVoltes = 0;
                timer.active = false;
                car.active = false;
                car.ResetCar();
                
                //Mostrem el pantell d'inici
                gamePanel.SetActive(false);
                buttonRepeticio.SetActive(true);
                startPanel.SetActive(true);
            }
        }
        currentTriggerActive++;
        if(currentTriggerActive >= triggersCarretera.Count) currentTriggerActive = 0;
        
        triggersCarretera[currentTriggerActive].SetActive(true);
    }
}
