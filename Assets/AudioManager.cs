using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    FMOD.Studio.Bus MasterBus;

    void Awake()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
    }

    // Start is called before the first frame update
    void Start()
    {

        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
