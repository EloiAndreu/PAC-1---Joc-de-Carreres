using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    
    public float elapsedTime = 0f;

    public bool active = false;

    void Update()
    {
        if(active){
            // Augmentar el temps
            elapsedTime += Time.deltaTime;
        }

        if(timerText != null){

            // Convertir el temps en minuts, segons i milisegons
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

            // Mostrar format de temps: "MM:SS:MMM"
            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);
        }
    }

}
