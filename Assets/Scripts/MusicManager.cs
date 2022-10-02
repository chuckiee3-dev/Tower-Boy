using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class MusicManager : PhasedActor
{
    public AudioClip intro;
    public AudioClip shop;
    public AudioClip battle;
    public AudioClip positioning;

    private AudioSource musicSource;
    protected override void Act(Phase phase)
    {
        if (musicSource != null)
        {
            MMSoundManagerSoundControlEvent.Trigger(MMSoundManagerSoundControlEventTypes.Stop,0,musicSource);
        }
        switch (phase)
        {
            case Phase.Positioning:
                
                musicSource = MMSoundManagerSoundPlayEvent.Trigger(positioning, MMSoundManager.MMSoundManagerTracks.Music, Vector3.zero);
                break;
            case Phase.War:
                musicSource = MMSoundManagerSoundPlayEvent.Trigger(battle, MMSoundManager.MMSoundManagerTracks.Music, Vector3.zero);
                break;
            case Phase.Build:
                musicSource = MMSoundManagerSoundPlayEvent.Trigger(shop, MMSoundManager.MMSoundManagerTracks.Music, Vector3.zero);
                break;
            case Phase.NONE:     
                musicSource = MMSoundManagerSoundPlayEvent.Trigger(intro, MMSoundManager.MMSoundManagerTracks.Music, Vector3.zero);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(phase), phase, null);
        }
    }
}
