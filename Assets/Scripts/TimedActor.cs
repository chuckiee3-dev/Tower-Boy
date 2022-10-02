using UnityEngine;

public abstract class TimedActor:MonoBehaviour
{
    private void OnEnable()
    {
        GameActions.onTimerTriggered += Act;
    }


    private void OnDisable()
    {
        GameActions.onTimerTriggered -= Act;
    }
    protected abstract void Act();

}
