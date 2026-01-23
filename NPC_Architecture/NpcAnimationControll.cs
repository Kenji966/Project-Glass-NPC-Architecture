using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAnimationControl : MonoBehaviour
{
    #region Get Components
        Animator animator;
        NavMeshAgent agent;
    #endregion

    #region Animation parameter names
        const string PARAM_HORIZONTAL = "Horizontal";
        const string PARAM_VERTICAL = "Vertical";
        const string PARAM_MAGNITUDE = "Magnitude";
        const string PARAM_IS_SIT = "isSit";
        const string TRIGGER_STAND_TO_SIT = "StandToSit";
        const string TRIGGER_SIT_TO_STAND = "SitToStand";
        
    #endregion

    // State tracking to prevent animation conflicts
    bool isTransitioning = false;
    bool currentSitState = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
        {
            Debug.LogError("[NPC][Animation] Animator component not found!");
        }
    }

    void Update()
    {
        if (animator == null) return;

        UpdateMovementParameters();
    }

    void UpdateMovementParameters()
    {
        if (agent == null) return;

        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float horizontal = localVelocity.x;
        float vertical = localVelocity.z;
        float magnitude = velocity.magnitude;

        animator.SetFloat(PARAM_HORIZONTAL, horizontal);
        animator.SetFloat(PARAM_VERTICAL, vertical);
        animator.SetFloat(PARAM_MAGNITUDE, magnitude);
    }

    // Trigger sit animation 
    public void TriggerSit()
    {
        if (animator == null || isTransitioning) return;

        if (!currentSitState)
        {
            isTransitioning = true;
            currentSitState = true;
            animator.SetBool(PARAM_IS_SIT, true);
            animator.SetTrigger(TRIGGER_STAND_TO_SIT);
            StartCoroutine(ResetTransitionFlag());
        }
    }

    // Trigger stand animation
    public void TriggerStand()
    {
        if (animator == null || isTransitioning) return;

        if (currentSitState)
        {
            isTransitioning = true;
            currentSitState = false;
            animator.SetBool(PARAM_IS_SIT, false);
            animator.SetTrigger(TRIGGER_SIT_TO_STAND);
            StartCoroutine(ResetTransitionFlag());
        }
    }

    IEnumerator ResetTransitionFlag()
    {
        // Wait for transition to complete (adjust time based on your animation length)
        yield return new WaitForSeconds(0.5f);
        isTransitioning = false;
    }
}
