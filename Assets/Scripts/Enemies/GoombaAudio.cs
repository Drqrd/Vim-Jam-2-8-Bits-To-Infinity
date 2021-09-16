using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(path, gameObject);
    }
}
