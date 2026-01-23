using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOrderWaitingState : MonoBehaviour
{

    #region Other Class
        [Header("- Other Class")]
        public NPCEmotionManager npcEmotionManager;
        public NpcUIManager npcUIManager;
        Animator animator;
    #endregion

    #region Order Waiting State Variables
        [Header("- Order Waiting State Variables")]
        public OrderData currentOrderData; // current Npc drink order
        public float waitingTime;
        bool getDrinkCompleted = false; // check if the Npc has completed the drink
    
    #endregion


    void Start()
    {
        animator = GetComponent<Animator>();
        npcEmotionManager = GetComponent<NPCEmotionManager>();
        npcUIManager = GetComponent<NpcUIManager>();
    }

    // Update the waiting time for Order Waiting State
    public float UpdateWaitingTime()
    {
        waitingTime += Time.deltaTime;;
        npcUIManager.showTimeTxt.text = waitingTime.ToString("F0");
        return waitingTime;
    }

    // Get the drink from the order
    public void getDrink(OrderData orderData)
    {
        if(currentOrderData != null)
        if(currentOrderData == orderData)
        getDrinkCompleted = true;
       
    }

    // RunState for Order Waiting State
   public NPCBehaviourState RunState()
    {
        if(getDrinkCompleted)
        {
            animator.SetBool("Drunk",true);
            FindObjectOfType<OrderManager>().RemoveOrder(gameObject, currentOrderData); 
            npcUIManager.UIPanel.GetComponent<Animator>().Play("CloseOrderPanel");
            FindObjectOfType<BarStatusManager>().AddGold(currentOrderData.Gold);
            FindObjectOfType<BarStatusManager>().ratingUpdate(9.5f);
            return NPCBehaviourState.NPCState_OrderComplete;
        }
        else if (npcEmotionManager.currentEmotionState == NPCEmotionState.Negative)
        {
            npcUIManager.UIPanel.GetComponent<Animator>().Play("CloseOrderPanel");
            FindObjectOfType<BarStatusManager>().ratingUpdate(0.1f);
            return NPCBehaviourState.NPCState_Leave;
        }
        return NPCBehaviourState.NPCState_OrderWaiting;
    }
}
