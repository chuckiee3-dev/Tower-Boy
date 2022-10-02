using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhasedActor : MonoBehaviour
{
        protected virtual void OnEnable()
        {
            GameActions.onPhaseChanged += Act;
        }


        protected virtual void OnDisable()
        {
            GameActions.onPhaseChanged -= Act;
        }
        protected abstract void Act(Phase phase);
}
