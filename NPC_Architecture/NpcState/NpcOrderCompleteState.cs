using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOrderCompleteState : MonoBehaviour
{

    public bool orderComplete = false;
    float drinkingTime = 0f,maxDrinkingTime = 0;
    

   public NPCBehaviourState RunState()
    {
        if(maxDrinkingTime == 0)
        {
            maxDrinkingTime = Random.Range(5f, 13f);
        }
        drinkingTime += Time.deltaTime;
        if(drinkingTime >= maxDrinkingTime)
        {
            orderComplete = true;
        }
        if(orderComplete)
        {
            return NPCBehaviourState.NPCState_Leave;
        }
      
        return NPCBehaviourState.NPCState_OrderComplete;
    }
}
