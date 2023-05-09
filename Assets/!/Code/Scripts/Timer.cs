using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] int HOURS;
    [SerializeField] int MINUTES;
    [SerializeField] int SECONDS;

    int hours;
    int minutes;
    int seconds;

    float currentTime = 0.0f;

    public bool pause;

    [SerializeField] UnityEvent endParty;

    public TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        if(this.seconds > 59){
            int modulo = this.seconds/60;
            this.minutes += modulo;
            this.seconds = this.seconds%60;
            this.currentTime = (float)seconds;
        };
        if(this.minutes > 59) {
            int modulo = this.minutes/60;
            this.hours += modulo;
            this.minutes = this.minutes%60;
        }
        if(this.hours > 23) {
            this.hours = 23;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(this.pause) {
            return;
        }
        this.currentTime -= 1 * Time.deltaTime;
        this.seconds = (int)this.currentTime;
        if(this.seconds < 0) {
            this.minutes -= 1;
            this.seconds = 59;
        }
        if(this.minutes < 0) {
            this.hours -= 1;
            this.minutes = 59;
        }
        if(this.hours < 0) {
            EndTimer();
        }
    }

    public void StartTimer() {
        this.pause = false;
    }


    public void PauseTimer() {
        this.pause = true;
    }
    public void ReStartTimer() {
        PauseTimer();
        this.hours = this.HOURS;
        this.minutes = this.MINUTES;
        this.seconds = this.SECONDS;
    }

    private void DisplayTimer() {
        print(string.Format("{0:00}:{1:00}:{2:00}", this.hours, this.minutes, this.seconds));
        textMesh.text = string.Format("{0:00}:{1:00}:{2:00}", this.hours, this.minutes, this.seconds);
    }
    public void EndTimer() {
        PauseTimer();
        this.hours = 0;
        this.minutes = 0;
        this.seconds = 0;
        endParty.Invoke();
    }

    
}
