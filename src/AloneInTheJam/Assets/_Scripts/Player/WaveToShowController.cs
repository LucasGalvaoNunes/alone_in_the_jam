using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveToShowController : MonoBehaviour
{
    float x;
    float y;
    float z;
    Transform thisTransform;
    public SphereCollider collider;
    float time;
    float auxTime;
    bool expand;
    public float speedOfWave;
    public Vector3 sizeMaxOfWave;
    public AudioSource waveAudio;
    public AudioPlayer audioPlayer;
    bool itsTimeToWave;

    
    public ScannerEffectDemo scanner;
    // Use this for initialization
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
        if (auxTime < time)
        {
            auxTime += speedOfWave;
            expand = true;
        }
        else
            expand = false;
        Open();
        if (GameController.gameController.iniciaPartida && !Phone.phone.recharging)
        {
            if (Input.GetMouseButtonDown(1) && !waveAudio.isPlaying)
            {
                StartCoroutine(WaveShow());
                waveAudio.Play();
            }
        }
        
    }

    /// <summary>
    /// This scale wave.
    /// </summary>
    void Open()
    {
        if (itsTimeToWave)
        {
            if (thisTransform.localScale != sizeMaxOfWave && expand)
            {
                x += 0.1f;
                y += 0.1f;
                z += 0.1f;
                thisTransform.localScale = new Vector3(x, y, z);
            }
            else if (thisTransform.localScale == sizeMaxOfWave)
            {
                x = 0;
                y = 0;
                z = 0;
                thisTransform.localScale = new Vector3(0, 0, 0);
                itsTimeToWave = false;
                collider.enabled = false;
            }
        }
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagsUtil.WAVE_INTERACTABLE || other.tag == TagsUtil.ENEMY)
        {
            audioPlayer.EitaPorra();
            //other.GetComponent<EnemyAIController>().SetEmission();
        }
    }
    IEnumerator WaveShow()
    {
        scanner.Scan();
        collider.enabled = true;
        itsTimeToWave = true;
        yield return new WaitForSeconds(5);
    }
}
