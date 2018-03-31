using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawEnemy : MonoBehaviour
{
    public Timer tempo;
    public Transform playerTransform;

    public int maxEemyWave;
    public bool waveCompleted;

    public List<GameObject> localSpawActived;
    public List<GameObject> localSpawDesactived;

    public List<GameObject> freirasActived;
    public List<GameObject> freirasDesactived;
    public bool ok;
    // Use this for initialization
    void Start()
    {
        localSpawDesactived.AddRange(GameObject.FindGameObjectsWithTag(TagsUtil.LOCAL_SPAW));
        freirasDesactived.AddRange(GameObject.FindGameObjectsWithTag(TagsUtil.ENEMY));
        foreach (var c in freirasDesactived) { if (c.activeSelf) { c.SetActive(false); } }
        maxEemyWave = 5;
        SpawEnemyWave(maxEemyWave);
    }

    // Update is called once per frame
    void Update()
    {

        if (waveCompleted)
        {
            waveCompleted = false;
            if (maxEemyWave < 100)
            {
                maxEemyWave += 5;
                SpawEnemyWave(maxEemyWave);
            }

        }
        if(waveCompleted)
        {
            var rand = Random.Range(0, freirasActived.Count - 1);
            var condition = Random.Range(0, 2);
            if (condition == 0 || condition == 2)
            {
                freirasActived[rand].GetComponent<EnemyAIController>().itsTimeToFollowPlayer = true;
                Debug.Log("freira" + freirasActived[rand].name);
            }
        }
        if(Phone.phone.contFreirasTotal - maxEemyWave == 1)
        {
            Debug.Log("Ultima Freira");
            var rand = Random.Range(0, freirasActived.Count - 1);
            freirasActived[rand].GetComponent<EnemyAIController>().itsTimeToFollowPlayer = true;
            Debug.Log("freira" + freirasActived[rand].name);
        }




        if (!PlayerController.instance.canPlayFootStep && GameController.gameController.iniciaPartida)
        {
            if (tempo.timer > 5)
            {
                //auxTempo += 10;
                var rand = Random.Range(0, freirasActived.Count - 1);
                var condition = Random.Range(0, 3);
                if (condition == 0 || condition == 3)
                {
                    freirasActived[rand].GetComponent<EnemyAIController>().itsTimeToFollowPlayer = true;
                    Debug.Log("freira" + freirasActived[rand].name);
                    tempo.timer = 0;
                }
                else { tempo.timer = 0; }

            }
        }
        else
        {
            tempo.timer = 0;
        }


    }

    void SpawEnemyWave(int maxEnemy)
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            var randPosi = Random.Range(0, localSpawDesactived.Count - 1);
            var randFreira = Random.Range(0, freirasDesactived.Count - 1);

            freirasDesactived[randFreira].SetActive(true);
            freirasDesactived[randFreira].transform.position = localSpawDesactived[randPosi].transform.position;
            freirasActived.Add(freirasDesactived[randFreira]);
            freirasDesactived.Remove(freirasDesactived[randFreira]);
            localSpawActived.Add(localSpawDesactived[randPosi]);
            localSpawDesactived.Remove(localSpawDesactived[randPosi]);
        }

    }


}
