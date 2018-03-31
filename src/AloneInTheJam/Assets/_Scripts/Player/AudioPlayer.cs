using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public float tempo;
    public float auxTempo;
    public AudioSource gritoBruxa;

    public AudioSource[] frasesAleatorias;
    public AudioSource[] aiai;//
                              // public AudioSource[] beleza;
                              //public AudioSource[] cade;
    public AudioSource[] caraca;//
    public AudioSource[] deuPau;//
    public AudioSource[] eitaPorra;//
    //public AudioSource[] freirasMocada;
    public AudioSource[] matei;//
    //public AudioSource[] seraMocada;
    //public AudioSource[] vamoLa;
    //public AudioSource[] vamosSala;

    // Use this for initialization
    void Start()
    {
        tempo = Time.time;
        auxTempo += Time.time+ 60;
    }
    public void Update()
    {
        tempo = Time.time;
        if (auxTempo < tempo)
        {
            auxTempo += 60;
            var random = Random.Range(0, frasesAleatorias.Length);
            frasesAleatorias[random].Play();

        }
    }
    public void GritoBruxa()
    {
       
        if (!gritoBruxa.isPlaying)
        {
            gritoBruxa.Play();
            return;
        }
    }

    public void AiAi()
    {
        var rand = Random.Range(0, aiai.Length);
        if (!aiai[0].isPlaying && !aiai[1].isPlaying)
        {
            aiai[rand].Play();
            return;
        }
    }


    public void Caraca()
    {
        var rand = Random.Range(0, caraca.Length);
        if (!caraca[0].isPlaying && !caraca[1].isPlaying && !caraca[2].isPlaying)
        {
            caraca[rand].Play();
            return;
        }
    }
    public void DeuPau()
    {
        var rand = Random.Range(0, deuPau.Length);
        if (!deuPau[0].isPlaying && !deuPau[1].isPlaying)
        {
            deuPau[rand].Play();
            return;
        }
    }
    public void EitaPorra()
    {
        var rand = Random.Range(0, eitaPorra.Length);
        if (!eitaPorra[0].isPlaying && !eitaPorra[1].isPlaying && !eitaPorra[2].isPlaying)
        {
            eitaPorra[rand].Play();
            return;
        }
    }

    public void Matei()
    {
        var rand = Random.Range(0, matei.Length);
        if (!matei[0].isPlaying && !matei[1].isPlaying && !matei[2].isPlaying)
        {
            matei[rand].Play();
            return;
        }
    }

}
