using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcLeaveState : MonoBehaviour
{
    #region Get Other Components
        Animator animator;
        NavMeshAgent agent;
        CapsuleCollider capsuleCollider;
        NpcAnimationControll animationController;
    #endregion

    #region Leave State Variables
        private Transform leaveTarget;
        private bool hasInitialized = false;
        private bool hasTriggeredStand = false;
    #endregion


    #region Leave State Constants
        const string PARAM_IS_SIT = "isSit";
        const float STAND_CENTER_Y = 0.81f;
        const float STAND_BASE_OFFSET = 0f;
        const float STOP_DISTANCE = 0.5f;
    #endregion
    
    void Start()
    {
        animator = GetComponent<Animator>(); 
        agent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animationController = GetComponent<NpcAnimationControll>();
        
        // Find LeavePos in scene
        GameObject leavePosObj = GameObject.Find("LeavePos");
        if (leavePosObj != null)
            leaveTarget = leavePosObj.transform;
        else
            Debug.LogWarning("LeavePos not found in scene!");
      
    }
    
    public NPCBehaviourState RunState()
    {
        // Initialize on first run
        if (!hasInitialized)
        {
            hasInitialized = true;
            hasTriggeredStand = false;
        }
        
        // Check if sitting and trigger stand animation
        if (animator != null && !hasTriggeredStand)
        {
            bool isSitting = animator.GetBool(PARAM_IS_SIT);
            
            if (isSitting)
            {
                // Trigger stand animation
                if (animationController != null)
                {
                    animationController.TriggerStand();
                }
                
                // Reset capsule collider center Y
                if (capsuleCollider != null)
                {
                    Vector3 center = capsuleCollider.center;
                    center.y = STAND_CENTER_Y;
                    capsuleCollider.center = center;
                    Debug.Log($"[NPC][Leave] CapsuleCollider center Y set to {STAND_CENTER_Y}");
                }
                
                // Reset NavMeshAgent baseOffset
                if (agent != null)
                {
                    agent.baseOffset = STAND_BASE_OFFSET;
                    Debug.Log("[NPC][Leave] NavMeshAgent baseOffset set to 0");
                }
                
                hasTriggeredStand = true;
                Debug.Log("[NPC][Leave] Triggered stand animation and reset collider/agent");
            }
        }
        
        // Move to LeavePos
        if (leaveTarget == null)
        {
            Debug.LogWarning("[NPC][Leave] LeavePos target not found!");
            return NPCBehaviourState.NPCState_Leave;
        }
        
        if (agent != null)
        {
            agent.SetDestination(leaveTarget.position);
            
            // Check if reached destination
            if (Vector3.Distance(transform.position, leaveTarget.position) < STOP_DISTANCE)
            {
                Debug.Log("[NPC][Leave] Reached LeavePos, NPC should be destroyed or disabled.");
                // TODO: Destroy or disable NPC here
                return NPCBehaviourState.NPCState_Leave;
            }
        }
        
        
        return NPCBehaviourState.NPCState_Leave;
    }
}
