using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum for NPC emotion state
public enum NPCEmotionState { 
    Happy,
    SlightlyImpatient,
    Impatient,
    Angry,
    Negative
}

public class NPCEmotionManager : MonoBehaviour
{

    // NPC emotion state
    public NPCEmotionState currentEmotionState;
 
    // Other Class
    NpcOrderWaittingState orderWaitingState;
   

     void Start()
     {
        orderWaitingState = GetComponent<NpcOrderWaittingState>();
     }
  
    void Update()
    {
        if (orderWaitingState == null) return;

        float waitingTime = orderWaitingState.waitingTime; // get the waiting time from the order waiting state

        switch(waitingTime) // switch the waiting time to the emotion state
        {
            case > 60f:
                currentEmotionState = NPCEmotionState.Negative;
            break;
            case > 45f:
                currentEmotionState = NPCEmotionState.Angry;
            break;
            case > 35f:
                currentEmotionState = NPCEmotionState.Impatient;
            break;
            case > 15f:
                currentEmotionState = NPCEmotionState.SlightlyImpatient;
            break;
            default:
                currentEmotionState = NPCEmotionState.Happy;
            break;
        }
    }
}
