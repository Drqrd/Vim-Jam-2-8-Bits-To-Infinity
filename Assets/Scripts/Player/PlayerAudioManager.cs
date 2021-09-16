using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public GameObject player;

    private PlayerMovement playerMovementScript;
    private PlayerState currentPlayerState;
    private float playerVelocity;

    //FMOD Event instances
    private FMOD.Studio.EventInstance player_isMovingInstance;

    // Start is called before the first frame update
    void Start()
    {

        playerMovementScript = player.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update Variables
        currentPlayerState = playerMovementScript.currentState;
        playerVelocity = GetComponentInParent<Rigidbody2D>().velocity.sqrMagnitude;

        //player_isMoving volume depends on speed
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PlayerVelocity", playerVelocity); 
    }

    //for oneshots called from animations
    void PlaySound(string eventPath)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(eventPath, player);
    }


    public static bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
    

}
