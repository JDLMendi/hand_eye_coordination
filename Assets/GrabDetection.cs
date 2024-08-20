using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabDetection : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Renderer objectRenderer;
    private Color originalColor;
    public Color grabbedColor = Color.red; // Color to change to when grabbed

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        objectRenderer = GetComponent<Renderer>();
        
        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable component is missing from this GameObject.");
        }

        if (objectRenderer == null)
        {
            Debug.LogError("Renderer component is missing from this GameObject.");
        }
        else
        {
            originalColor = objectRenderer.material.color; // Store the original color
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
        Debug.Log("Object grabbed");
        if (objectRenderer != null)
        {
            objectRenderer.material.color = grabbedColor; // Change color to red
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("Object released");
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor; // Revert to original color
        }
    }
}
