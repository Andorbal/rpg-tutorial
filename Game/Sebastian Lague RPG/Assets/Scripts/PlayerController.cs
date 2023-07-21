using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
  public LayerMask movementMask;

  public Interactable focus;
  Camera cam;
  PlayerMotor motor;

  // Start is called before the first frame update
  void Start()
  {
    cam = Camera.main;
    motor = GetComponent<PlayerMotor>();
  }

  // Update is called once per frame
  void Update()
  {
    if (EventSystem.current.IsPointerOverGameObject())
    {
      return;
    }

    if (Input.GetMouseButtonDown(0))
    {
      var ray = cam.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out var hit, 100, movementMask))
      {
        Debug.Log($"We hit {hit.collider.name} at {hit.point}");
        motor.MoveToPoint(hit.point);

        RemoveFocus();
        // Move out player to what we hit


        // Stop focusing any objects
      }
    }
    if (Input.GetMouseButtonDown(1))
    {
      var ray = cam.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out var hit, 100))
      {
        var interactable = hit.collider.GetComponent<Interactable>();
        if (interactable != null)
        {
          SetFocus(interactable);
        }
        // Check if we hit an interactable
        // If we did, set it as our focus
      }
    }
  }

  private void RemoveFocus()
  {
    focus?.OnDefocused();
    focus = null;
    motor.StopFollowingTarget();
  }

  private void SetFocus(Interactable newFocus)
  {
    if (newFocus != focus)
    {
      focus?.OnDefocused();
      motor.FollowTarget(newFocus);
      focus = newFocus;
    }

    newFocus.OnFocused(transform);
  }
}
