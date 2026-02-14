using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enum for NPC behaviour state
public enum NPCBehaviourState
{
    NPCState_Spawn,
    NPCState_FindPosition,
    NPCState_Move,
    NPCState_Order,
    NPCState_OrderWaiting,
    NPCState_OrderComplete,
    NPCState_Leave
}


public class NPCBehaviourManager : MonoBehaviour
{
    #region State Components Class
        NpcFindPosState findPositionState;
        NpcMoveState moveState;
        NpcOrderState orderState;
        NpcOrderWaittingState orderWaitingState;
        NpcOrderCompleteState orderCompleteState;
        NpcLeaveState leaveState;
    #endregion

    [Header("Initial state")]
    [SerializeField]
    NPCBehaviourState initialBehaviourState = NPCBehaviourState.NPCState_Spawn; // set Default State is Spawn State

    NPCBehaviourState currentBehaviourState; // current NPC behaviour state
    
    #region Delegate for State Handlers
        delegate NPCBehaviourState StateHandler();
        readonly Dictionary<NPCBehaviourState, StateHandler> stateHandlers = new(); 
    
    #endregion


    void Awake()
    {
        RegisterStates();
        SetBehaviourState(initialBehaviourState);

        // Get the state components
        findPositionState = GetComponent<NpcFindPosState>();
        moveState = GetComponent<NpcMoveState>();
        orderState = GetComponent<NpcOrderState>();
        orderWaitingState = GetComponent<NpcOrderWaittingState>();
        orderCompleteState = GetComponent<NpcOrderCompleteState>();
        leaveState = GetComponent<NpcLeaveState>();
    }

    void Update() 
    {
        if (!stateHandlers.TryGetValue(currentBehaviourState, out var handler))
        {
            return;
        }

        var nextState = handler.Invoke();
        if (nextState == currentBehaviourState)
            return;

        SetBehaviourState(nextState);
    }

    // Register the states
    void RegisterStates() 
    {
        stateHandlers[NPCBehaviourState.NPCState_Spawn] = HandleSpawnState;
        stateHandlers[NPCBehaviourState.NPCState_FindPosition] = HandleFindPositionState;
        stateHandlers[NPCBehaviourState.NPCState_Move] = HandleMoveState;
        stateHandlers[NPCBehaviourState.NPCState_Order] = HandleOrderState;
        stateHandlers[NPCBehaviourState.NPCState_OrderWaiting] = HandleOrderWaitingState;
        stateHandlers[NPCBehaviourState.NPCState_OrderComplete] =HandleOrderCompleteState;
        stateHandlers[NPCBehaviourState.NPCState_Leave] = HandleLeaveState;
    }

    // Set the behaviour state
    void SetBehaviourState(NPCBehaviourState newState)
    {
        currentBehaviourState = newState;
        
    }


    #region State Handlers
        NPCBehaviourState HandleSpawnState()
        {
            // TODO: handle the initial logic when NPC spawn
            return NPCBehaviourState.NPCState_FindPosition;
        }

        NPCBehaviourState HandleFindPositionState()
        {
            if (findPositionState == null)
            {
                return NPCBehaviourState.NPCState_FindPosition;
            }

            return findPositionState.RunState();
        }

        NPCBehaviourState HandleMoveState()
        {
            if (moveState == null)
            {
                return NPCBehaviourState.NPCState_Move;
            }
            return moveState.RunState();
        }

        NPCBehaviourState HandleOrderState()
        {
            if (orderState == null)
            {
                return NPCBehaviourState.NPCState_Order;
            }
            return orderState.RunState();
        }

        NPCBehaviourState HandleOrderWaitingState()
        {
            if (orderWaitingState == null)
            {
                return NPCBehaviourState.NPCState_OrderWaiting;
            }
            return orderWaitingState.RunState();
        }
        
        NPCBehaviourState HandleOrderCompleteState()
        {
            if (orderCompleteState == null)
            {
                return NPCBehaviourState.NPCState_OrderComplete;
            }
            return orderCompleteState.RunState();
        }
        NPCBehaviourState HandleLeaveState()
        {
            if (leaveState == null)
            {
                return NPCBehaviourState.NPCState_Leave;
            }
            return leaveState.RunState();
        }
        NPCBehaviourState HandleGenericState(NPCBehaviourState state)
        {
            return state;
        }
    
    #endregion
}

