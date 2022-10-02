using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float duration = 10;

    private float timer;
    void Start()
    {
        timer = duration;
        GameActions.SetDuration(duration);
    }

    void Update()
    {
        if(GameManager.I.state != GameState.Playing) return;
        timer = timer - Time.deltaTime;
        if (timer <= 0)
        {
            timer = duration;
            GameActions.TimerTriggered();
        }
    }
}
