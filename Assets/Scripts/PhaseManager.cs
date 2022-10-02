using System.Collections.Generic;

public class PhaseManager : TimedActor
{
    public List<Phase> allPhases;
    private int currentPhaseIndex = 0;

    private void Start()
    {
        GameActions.PhaseChanged(allPhases[currentPhaseIndex % allPhases.Count] );
    }

    protected override void Act()
    {
        currentPhaseIndex++;
        GameActions.PhaseChanged(allPhases[currentPhaseIndex % allPhases.Count] );
    }
}
