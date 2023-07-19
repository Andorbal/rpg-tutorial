using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
  public LayerMask movementMask;

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
    if (Input.GetMouseButtonDown(0))
    {
      var ray = cam.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out var hit, 100, movementMask))
      {
        Debug.Log($"We hit {hit.collider.name} at {hit.point}");
        motor.MoveToPoint(hit.point);
        // Move out player to what we hit


        // Stop focusing any objects
      }
    }
    if (Input.GetMouseButtonDown(1))
    {
      var ray = cam.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out var hit, 100))
      {
        // Check if we hit an interactable
        // If we did, set it as our focus
      }
    }
  }
}
