using UnityEngine;

public class Interactable : MonoBehaviour
{
  public float radius = 3f;
  public Transform interactionTransform;

  bool isFocus = false;

  Transform player;

  bool hasInteracted = false;

  public virtual void Interact()
  {
    // Meant to be overridden
    Debug.Log($"Interacting with {transform.name}");
  }

  void Update()
  {
    if (isFocus && !hasInteracted)
    {
      var distance = Vector3.Distance(player.position, interactionTransform.position);
      if (distance <= radius)
      {
        Interact();
        hasInteracted = true;
      }
    }
  }

  public void OnFocused(Transform playerTransform)
  {
    hasInteracted = false;
    isFocus = true;
    player = playerTransform;
  }

  public void OnDefocused()
  {
    hasInteracted = false;
    isFocus = false;
    player = null;
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(interactionTransform.position, radius);
  }
}
