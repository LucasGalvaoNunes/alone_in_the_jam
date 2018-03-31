using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    


    public float tempoValor;// Tempo em que o relogio vai andar;
    public float _temp;// Tempo, nao usar este para comparações!!!
    public float _auxTemp;// Uso somente para comparar o _temp.

    public float timer; // Este sera zerado toda vez que a cena do jogo for iniciada;
    public float timerAux;// Este sera somente um auxiliar para comparações;

        
    void Start ()
    {
        timer = 0;        
        _auxTemp = tempoValor;
        timerAux = tempoValor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _temp += Time.deltaTime;

        if (_temp >= _auxTemp)
        {
            _auxTemp += tempoValor;
            timer += tempoValor;
        }
    }
}
