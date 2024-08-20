using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FixedGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isGrabbed;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable component is missing from this GameObject.");
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (isGrabbed) return;

        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        
        // Set the parent to null to avoid positional changes from physics
        transform.SetParent(null, true);
        
        // Lock the position and rotation
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        isGrabbed = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (!isGrabbed) return;

        // Revert back to the original parent if necessary
        transform.SetParent(originalParent, true);
        
        // Reset position and rotation to original values if desired
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;

        isGrabbed = false;
    }
}
