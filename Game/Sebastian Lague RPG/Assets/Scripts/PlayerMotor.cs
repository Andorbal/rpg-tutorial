using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
  [Range(0, 1)]
  public float stoppingDistancePadding = 0.6f;

  NavMeshAgent agent;
  private Transform target;

  // Start is called before the first frame update
  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
  }

  void Update()
  {
    if (target != null)
    {
      agent.SetDestination(target.position);
      FaceTarget();
    }
  }

  private void FaceTarget()
  {
    var direction = (target.position - transform.position).normalized;
    var lookRotation = Quaternion.LookRotation(new(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
  }

  public void MoveToPoint(Vector3 point)
  {
    agent.SetDestination(point);
  }

  public void FollowTarget(Interactable newTarget)
  {
    agent.stoppingDistance = newTarget.radius * stoppingDistancePadding;
    agent.updateRotation = false;
    target = newTarget.interactionTransform;
  }

  public void StopFollowingTarget()
  {
    agent.stoppingDistance = 0;
    agent.updateRotation = true;
    target = null;
  }
}
