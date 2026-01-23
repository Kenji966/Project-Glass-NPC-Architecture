using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFindPosState : MonoBehaviour
{
    [Header("- Setup Target Positions")]
        public Transform[] targets; // List targets for NPC to move Position

    #region Other Class
        NpcMoveState moveState;
        TableManager tableManager;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        moveState = GetComponent<NpcMoveState>();
        // Use FindObjectOfType to automatically find TableManager in scene
        tableManager = FindObjectOfType<TableManager>(); 
        
        if (tableManager == null)
        {
            Debug.LogError("TableManager not found in scene!");
        }
    }
    public NPCBehaviourState RunState()
    {
        if (targets == null || targets.Length == 0)
        {
            Debug.LogWarning("No targets");
            return NPCBehaviourState.NPCState_FindPosition;
        }

        if (tableManager == null)
        {
            Debug.LogWarning("TableManager is null, cannot find available table.");
            return NPCBehaviourState.NPCState_FindPosition;
        }

        if (moveState.currentTarget == null)
        {
            int randomIndex = Random.Range(0, targets.Length);
            while(!tableManager.isTableAvailable[randomIndex]){
                randomIndex = Random.Range(0, targets.Length);
            }
            moveState.currentTarget = targets[randomIndex];
            tableManager.isTableAvailable[randomIndex] = false;
            Debug.Log($"Find the position: {moveState.currentTarget.name}");
        }

        return moveState.currentTarget != null 
            ? NPCBehaviourState.NPCState_Move 
            : NPCBehaviourState.NPCState_FindPosition;
    }
}
