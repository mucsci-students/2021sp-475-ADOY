using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;

    public AudioSource soundPlayer;
    public AudioClip[] clips;

    float soundTime;
    float playSoundTime;

    void Start()
    {
        soundTime = 0;
        playSoundTime = 0;    
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);

        soundTime += Time.deltaTime;

        if (soundTime >= playSoundTime && !soundPlayer.isPlaying)
        {
            soundTime = 0;
            playSoundTime = Random.Range(5, 20);
            int randex = Random.Range(0, clips.Length);
            soundPlayer.clip = clips[randex];
            soundPlayer.Play();
        }
    }

}
