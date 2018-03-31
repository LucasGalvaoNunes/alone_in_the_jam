using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimationListener : MonoBehaviour {

    public AudioSource audioS;
    public AudioClip[] footstep;

    public void PlayFootStep()
    {
        if (PlayerController.instance.canPlayFootStep)
        {
            audioS.clip = footstep[Random.Range(0, 3)];
            audioS.Play();
        }
    }

}
