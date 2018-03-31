using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Tags = TagsUtil;
public class EnemyAIController : MonoBehaviour
{
   
    AudioSource ghost;                      // Audio to be play .
    public SpawEnemy spawEnemy;             // Spaw Enemy script.
    NavMeshAgent navMeshAgent;              // NavMeshAgent of this GameObject.
    bool audioPlay;                         
    public bool itsTimeToFollowPlayer;      // Verify if is time to follow player.
    public Transform playerTransform;       // Transform of player GameObject.

    float tempo;
    float auxTempo;
    bool moveTo;

    public AudioPlayer audioPlayer;
    // Use this for initialization
    void Start()
    {
        ghost = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        tempo = Time.time;
        if (auxTempo < tempo)
        {
            auxTempo += 5;
            moveTo = true;
        }
        else
        {
            moveTo = false;
        }
        
        if (!GameController.gameController.gameOver)
        {
            // Set destination to Enemy is Player position.
            if (itsTimeToFollowPlayer)
            {
                navMeshAgent.destination = playerTransform.position;
                navMeshAgent.transform.LookAt(new Vector3(playerTransform.transform.localPosition.x, navMeshAgent.transform.localPosition.y, playerTransform.transform.localPosition.z));
                

            }
            else if (moveTo)
                MoveTo();
        }
        else
        {
            navMeshAgent.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagsUtil.PLAYER)
        {
            if (!ghost.isPlaying)
            {
                ghost.Play();
            }
            itsTimeToFollowPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagsUtil.PLAYER)
        {
            itsTimeToFollowPlayer = false;
        }
    }

    public void Died()
    {
        audioPlayer.Matei();
        gameObject.SetActive(false);
    }

    void MoveTo()
    {
        var randLocal = Random.Range(0, spawEnemy.localSpawDesactived.Count - 1);
        navMeshAgent.destination = spawEnemy.localSpawDesactived[randLocal].transform.position;
    }
}
