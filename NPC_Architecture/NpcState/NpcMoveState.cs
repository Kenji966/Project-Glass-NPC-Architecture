using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class NpcMoveState : MonoBehaviour
{
 [Header("- Current Target Position")]
  public Transform currentTarget;
  
  
# region Move Parameters
    [Header("- Setup Move Parameters")]
    [SerializeField] float moveSpeed;
    [SerializeField] float stopDistance;
    [SerializeField] float baseOffset;
    [SerializeField] float sitCenterY;
  #endregion

# region Other Components
    NpcAnimationControll animationController;
    CapsuleCollider capsuleCollider;
    NavMeshAgent agent;

  #endregion


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<NpcAnimationControll>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void animationSitEvent(){
            if (capsuleCollider != null)
            {
                Vector3 center = capsuleCollider.center;
                center.y = sitCenterY;
                capsuleCollider.center = center;
                Debug.Log($"[NPC][Move] CapsuleCollider center Y set to {sitCenterY}");
            }

            agent.baseOffset = baseOffset;
    }

    public NPCBehaviourState RunState()
    {
        if (currentTarget == null)
        {
            Debug.LogWarning("[NPC][Move] no position, return FindPosition state.");
            return NPCBehaviourState.NPCState_FindPosition;
        }
        agent.speed = moveSpeed;
        agent.SetDestination(currentTarget.position);

        Debug.Log($"[NPC][Move] ：{currentTarget.name}");
        
        if (Vector3.Distance(transform.position, currentTarget.position) < stopDistance)
        {
            Debug.Log("[NPC][Move] reached the position, switching to Sit state.");
           
           
            // Trigger sit animation
            if (animationController != null)
            {
                animationController.TriggerSit();
            }
            
           
            // Set capsule collider center Y to 0.81 before triggering sit animation
        
            
            // Set random rotation between -80 and -100 degrees on Y axis
            float randomRotationY = Random.Range(-100f, -80f);
            // Clamp to ensure it's within -100 to -80 range
            randomRotationY = Mathf.Clamp(randomRotationY, -100f, -80f);
            // Directly set Y rotation, keep X and Z as 0
            transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);
            Debug.Log($"[NPC][Move] Rotation set to Y: {randomRotationY} degrees (clamped to -100 to -80)");
           
            return NPCBehaviourState.NPCState_Order;
        }
        
        return NPCBehaviourState.NPCState_Move;
    }
}
