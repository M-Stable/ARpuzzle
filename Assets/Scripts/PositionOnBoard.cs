using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * This script is applied to the pieces
 * It detects event related to the piece
 */
public class PositionOnBoard : MonoBehaviour, IMixedRealityGestureHandler<Vector3>
{

    private GameObject HoldIndicator = null;
    private GameObject ManipulationIndicator = null;
    private GameObject NavigationIndicator = null;
    private GameObject SelectIndicator = null;

    private Material DefaultMaterial = null;
    private Material HoldMaterial = null;
    private Material ManipulationMaterial = null;
    private Material NavigationMaterial = null;
    private Material SelectMaterial = null;

    private GameObject RailsAxisX = null;
    private GameObject RailsAxisY = null;
    private GameObject RailsAxisZ = null;

    private GameObject hitbox = null;

    private Transform camera;

    public float rotationSpeed = 180.0f;

    public Behaviour yellowHalo;
    public Behaviour greenHalo;

    private bool hasCollided = false;
    public bool droppedPiece = false;
    public bool isHeld = false;
    public bool pieceInPlace = false;

    private void OnEnable()
    {
        HideRails();
    }


    // Start is called before the first frame update
    void Start() 
    {
        yellowHalo.enabled = false;
        greenHalo.enabled = false;
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * This is triggered when the user starts to make a pinching gesture
     */
    public void OnGestureStarted(InputEventData eventData)
    {
        droppedPiece = false;

        // Debug.Log($"OnGestureStarted [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

        var action = eventData.MixedRealityInputAction.Description;
        if (action == "Hold Action")
        {
            FindObjectOfType<AudioManager>().Play("PickupPiece");

            yellowHalo.enabled = true;
            isHeld = true;
            SetIndicator(HoldIndicator, "Hold: started", HoldMaterial);
        }
        else if (action == "Manipulate Action")
        {
            FindObjectOfType<AudioManager>().Play("PickupPiece");

            yellowHalo.enabled = true;
            isHeld = true;
            SetIndicator(ManipulationIndicator, $"Manipulation: started {Vector3.zero}", ManipulationMaterial, Vector3.zero);
        }
        else if (action == "Navigation Action")
        {
            FindObjectOfType<AudioManager>().Play("PickupPiece");

            yellowHalo.enabled = true;
            SetIndicator(NavigationIndicator, $"Navigation: started {Vector3.zero}", NavigationMaterial, Vector3.zero);
            ShowRails(Vector3.zero);
        }

        SetIndicator(SelectIndicator, "Select:", DefaultMaterial);
    }

    /**
     * Depending on input type this is triggered when the user is holding the pinch
     */
    public void OnGestureUpdated(InputEventData eventData)
    {
        droppedPiece = false;

        // Debug.Log($"OnGestureUpdated [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

        var action = eventData.MixedRealityInputAction.Description;
        if (action == "Hold Action")
        {
            SetIndicator(HoldIndicator, "Hold: updated", DefaultMaterial);
        }
    }

    /**
     * Depending on input type this is triggered when the user is holding the pinch
     */
    public void OnGestureUpdated(InputEventData<Vector3> eventData)
    {
        droppedPiece = false;

        // Debug.Log($"OnGestureUpdated [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

        var action = eventData.MixedRealityInputAction.Description;
        if (action == "Manipulate Action")
        {
            SetIndicator(ManipulationIndicator, $"Manipulation: updated {eventData.InputData}", ManipulationMaterial, eventData.InputData);
        }
        else if (action == "Navigation Action")
        {
            SetIndicator(NavigationIndicator, $"Navigation: updated {eventData.InputData}", NavigationMaterial, eventData.InputData);
            ShowRails(eventData.InputData);
        }
    }

    /**
    * Depending on input type this is triggered when the user has let go of the piece
    */
    public void OnGestureCompleted(InputEventData eventData)
    {
        droppedPiece = true;
        // Debug.Log($"OnGestureCompleted [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

        var action = eventData.MixedRealityInputAction.Description;
        if (action == "Hold Action")
        {
            yellowHalo.enabled = false;
            isHeld = false;
            SetIndicator(HoldIndicator, "Hold: completed", DefaultMaterial);
        }
        else if (action == "Select")
        {
            yellowHalo.enabled = false;
            SetIndicator(SelectIndicator, "Select: completed", SelectMaterial);
        }
    }

    /**
    * This is triggered when the user has let go of the piece
    */
    public void OnGestureCompleted(InputEventData<Vector3> eventData)
    {
        droppedPiece = true;

        // Debug.Log($"OnGestureCompleted [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");


        var action = eventData.MixedRealityInputAction.Description;
        if (action == "Manipulate Action")
        {
            yellowHalo.enabled = false;
            isHeld = false;
            SetIndicator(ManipulationIndicator, $"Manipulation: completed {eventData.InputData}", DefaultMaterial, eventData.InputData);
        }
        else if (action == "Navigation Action")
        {
            yellowHalo.enabled = false;
            isHeld = false;
            SetIndicator(NavigationIndicator, $"Navigation: completed {eventData.InputData}", DefaultMaterial, eventData.InputData);
            HideRails();
        }
    }

    /**
    * This is triggered when the user stops pinching gesture before completing it
    */
    public void OnGestureCanceled(InputEventData eventData)
    {
        // Debug.Log($"OnGestureCanceled [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");
        droppedPiece = false;

        var action = eventData.MixedRealityInputAction.Description;
        if (action == "Hold Action")
        {
            yellowHalo.enabled = false;
            SetIndicator(HoldIndicator, "Hold: canceled", DefaultMaterial);
        }
        else if (action == "Manipulate Action")
        {
            yellowHalo.enabled = false;
            SetIndicator(ManipulationIndicator, "Manipulation: canceled", DefaultMaterial);
        }
        else if (action == "Navigation Action")
        {
            yellowHalo.enabled = false;
            SetIndicator(NavigationIndicator, "Navigation: canceled", DefaultMaterial);
            HideRails();
        }
    }

    private void SetIndicator(GameObject indicator, string label, Material material)
    {
        if (indicator)
        {
            var renderer = indicator.GetComponentInChildren<Renderer>();
            if (material && renderer)
            {
                renderer.material = material;
            }
            var text = indicator.GetComponentInChildren<TextMeshPro>();
            if (text)
            {
                text.text = label;
            }
        }
    }

    private void SetIndicator(GameObject indicator, string label, Material material, Vector3 position)
    {
        SetIndicator(indicator, label, material);
        if (indicator)
        {
            indicator.transform.localPosition = position;
        }
    }

    private void ShowRails(Vector3 position)
    {
        var gestureProfile = CoreServices.InputSystem.InputSystemProfile.GesturesProfile;
        var useRails = gestureProfile.UseRailsNavigation;

        if (RailsAxisX)
        {
            RailsAxisX.SetActive(!useRails || position.x != 0.0f);
        }
        if (RailsAxisY)
        {
            RailsAxisY.SetActive(!useRails || position.y != 0.0f);
        }
        if (RailsAxisZ)
        {
            RailsAxisZ.SetActive(!useRails || position.z != 0.0f);
        }
    }

    private void HideRails()
    {
        if (RailsAxisX)
        {
            RailsAxisX.SetActive(false);
        }
        if (RailsAxisY)
        {
            RailsAxisY.SetActive(false);
        }
        if (RailsAxisZ)
        {
            RailsAxisZ.SetActive(false);
        }
    }



}
