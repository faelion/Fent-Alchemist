using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnerBox : XRBaseInteractable
{
    [Tooltip("Prefab to spawn when this box is clicked/grabbed")]
    public GameObject itemToSpawn;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        GameObject spawned = Instantiate(itemToSpawn);

        XRGrabInteractable grabInteractable = spawned.GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogWarning("Spawned item needs an XRGrabInteractable component.");
            return;
        }

        IXRSelectInteractor interactor = (IXRSelectInteractor)args.interactorObject;
        IXRSelectInteractable interactable = (IXRSelectInteractable)grabInteractable;

        XRInteractionManager manager = this.interactionManager;
        manager.SelectEnter(interactor, interactable);
    }
}
