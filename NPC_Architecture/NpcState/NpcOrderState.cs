using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOrderState : MonoBehaviour
{
     [Header("- Setup Order Data")]    
     public OrderData[] orderDatas;

    #region Other Class
        NpcUIManager npcUIManager;
        public NpcOrderWaittingState WaitingState;
    #endregion

   
    void Start()
    {
        npcUIManager = GetComponent<NpcUIManager>();
        WaitingState = GetComponent<NpcOrderWaittingState>();
    }
    // Update is called once per frame
    
    public NPCBehaviourState RunState()
    {
        if(WaitingState.currentOrderData == null)
        {
            WaitingState.currentOrderData = orderDatas[Random.Range(0, orderDatas.Length)];

            FindObjectOfType<OrderManager>().GetOrder(WaitingState.currentOrderData, gameObject);

            npcUIManager.ShowOrderUI(WaitingState.currentOrderData);

            return NPCBehaviourState.NPCState_OrderWaiting;
        }
        

        return NPCBehaviourState.NPCState_Order;
    }
}
