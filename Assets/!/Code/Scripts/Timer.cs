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

    float currentTime = 0.0f;

    public bool pause = true;

    ///public boolean variable that determines whether
    // the timer should continue counting down into negative values after it reaches zero. 
    public bool negativeTimerAtTheEnd = true;

    public bool finished {
        get {
            return currentTime <= 0;
        }
    }


    public TextMeshProUGUI textMesh;
    [SerializeField] UnityEvent endParty;


    // Start is called before the first frame update
    void Start() {
        this.currentTime = this.SECONDS + 60 * (this.MINUTES + 60*this.HOURS) + 1;
        StartTimer();
    }
    // Update is called once per frame
    void Update() {
        if(this.pause) {
            return;
        }
        TimerUpdateCountDown();

        DisplayTimer();
    }

    /// <summary>
    /// This function updates a countdown timer by decrementing the time and adjusting the minutes and
    /// hours accordingly, and ends the timer when it reaches zero. If the timer is in netgativeTimerAtTheEnd mode the timer passed in countUp mode
    /// </summary>
    private void TimerUpdateCountDown() {
        this.currentTime -= 1 * Time.deltaTime;
        if(this.finished) {
            if(!negativeTimerAtTheEnd) {
                EndTimer();
            }
            else if(this.currentTime > -1) {
                    this.currentTime -= 1;
            }
        }
    }
    /// <summary>
    /// This function updates a timer by incrementing the current time by one second and updating the
    /// minutes and hours accordingly.
    /// </summary>
    private void TimerUpdateCountUp() {
        this.currentTime += 1 * Time.deltaTime;
    }

    /// <summary>
    /// Start the timer
    /// </summary>
    public void StartTimer() {
        this.pause = false;
    }

    /// <summary>
    /// Pause the timer
    /// </summary>
    public void PauseTimer() {
        this.pause = true;
    }
    /// <summary>
    /// The function restarts the timer and pauses it
    /// </summary>
    public void RestartTimer() {
        Start();
        PauseTimer();
    }

    /// <summary>
    /// The function resets the timer's hours, minutes, and seconds to zero.
    /// </summary>
    public void ResetTimerToZero() {
        this.currentTime = 0;
    }

    /// <summary>
    /// This function displays a timer in the format of hours, minutes, and seconds with an optional
    /// negative sign at the start.
    /// </summary>
    public void DisplayTimer() {
        string n = " ";
        if(this.finished) {
            textMesh.color = new Color(255, 0, 0, 1);
            if(this.negativeTimerAtTheEnd) n = "-";
        }
        else {
            textMesh.color = new Color(255, 255, 255, 1);
        }
        int nbSecond = Mathf.Abs((int)currentTime);
        int seconds_ = nbSecond%60;
        nbSecond -= seconds_;
        int nbMinutes = nbSecond/60;
        int minutes_ = nbMinutes%60;
        nbMinutes -= minutes_;
        int nbHours = nbMinutes/60;
        int hours_ = nbHours%60;

        textMesh.text = n + string.Format("{0:00}:{1:00}:{2:00}", hours_, minutes_, seconds_);
    }
    /// <summary>
    /// Pauses and resets the timer to zero then invokes the "endParty" event.
    /// </summary>
    public void EndTimer() {
        PauseTimer();
        ResetTimerToZero();
        endParty.Invoke();
    }

    /// <summary>
    /// This function removes a specified amount of time (in seconds, minutes, and/or hours) from the
    /// current time.
    /// </summary>
    /// <param name="s">The number of seconds to subtract from the current time</param>
    /// <param name="m">The number of minutes to be removed from the current time. It has a default value of 0</param>
    /// <param name="h">The number of hours to be removed from the current time</param>
    public void RemoveTime(int s, int m=0, int h=0) {
        this.currentTime -= (s + 60 * (m + 60*h));
    }

    /// <summary>
    /// This function removes a specified amount of time in seconds from the current time.
    /// </summary>
    /// <param name="s">The number of seconds to subtract from the current time</param>
    public void RemoveSeconds(int s) {
        this.currentTime -= s;
    }

    /// <summary>
    /// This function removes a specified amount of time in minutes from the current time.
    /// </summary>
    /// <param name="m">The number of minutes to be removed from the current time</param>
    public void RemoveMinutes(int m) {
        this.currentTime -= m*60;
    }

    /// <summary>
    /// This function removes a specified amount of time in hours from the current time.
    /// </summary>
    /// <param name="h">The number of hours to be removed from the current time</param>
    public void RemoveHours(int h) {
        this.currentTime -= h*3600;
    }
    
    public string GetElapsedTime(){
        
        this.currentTime = (this.SECONDS + 60 * (this.MINUTES + 60*this.HOURS)) - this.currentTime;
        int nbSecond = Mathf.Abs((int)currentTime);
        int seconds_ = nbSecond%60;
        nbSecond -= seconds_;
        int nbMinutes = nbSecond/60;
        int minutes_ = nbMinutes%60;
        nbMinutes -= minutes_;
        int nbHours = nbMinutes/60;
        int hours_ = nbHours%60;

        return string.Format("{0:00}:{1:00}:{2:00}", hours_, minutes_, seconds_);
    }

    public void Test() {
        print(this.GetElapsedTime());
    }
    
}
