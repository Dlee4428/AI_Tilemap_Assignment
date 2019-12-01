using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    GameObject player;
    [Header("Allies")]
    [SerializeField]
    GameObject[] allies;

    public enum Behaviour
    {
        IDLE, //if the agent has no target in range
        CHASING, //if the agent has a target in range, and an ally in tange 
        EVADING //if the target has a target in range and no ally 
    }
    [Header("Agent Patterns")]
    [SerializeField]
    PerlinNoise idleMove;

    public float distance;
    [Header("Agent Behaviour")]
    [SerializeField]
    Behaviour behaviour;
    [SerializeField]
    float agressionRadius;
    [SerializeField]
    float sosRadius;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(behaviour == Behaviour.IDLE)
        {
            idleMove.GenerateIdleMovement();//generates a random movement when idle
        }
        if(behaviour == Behaviour.CHASING)
        {
            Debug.Log("Chasing");   //Todo: Generate a tile towards the player
        }
        if(behaviour == Behaviour.EVADING)
        {
            Debug.Log("Fleeing"); //todo: Generates a tile away from the player
        }

        CheckDistFromPlayer();
    }

    private void CheckDistFromPlayer()
    {
        float nearestAlly;
        distance = Vector3.Magnitude(player.transform.position - transform.position);
        if(distance <= agressionRadius)
        {
            nearestAlly = CheckDistFromAlly(allies); //gets the magnitude of  the nearest ally from the agent 
            if(nearestAlly <= sosRadius)
                behaviour = Behaviour.CHASING; //if there is an ally within the Help Radius, the enemy will begin to chase the player

            if (nearestAlly > sosRadius) // if there is no ally within the Help Radius, the enemy will instead flee from the player
                behaviour = Behaviour.EVADING;
        }
        else
            behaviour = Behaviour.IDLE; //if the player is not within the agents agression range, the agent will wander around the map
        

    }
    private float CheckDistFromAlly(GameObject[] allies_)
    {
        //this function loops through the other (Hostile)allies on the map, looking for the closest ally to it, returning the magnitude of said closest ally
        float closestAlly = Mathf.Infinity;
        float checkedMag;
        for(int i = 0;  i < allies_.Length; i++)
        {
            checkedMag = Vector3.Magnitude(allies_[i].transform.position - transform.position);
            if(checkedMag < closestAlly)
            {
                closestAlly = checkedMag;
            }
        }
        return closestAlly;
    }
}
