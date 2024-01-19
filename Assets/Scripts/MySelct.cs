using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySelct : MonoBehaviour
{
    public OVRInput.Controller controller;
    private float triggerValue;
    [SerializeField] private bool isInCollider;
    [SerializeField] private bool isSelected;
    private GameObject selectedObj;

    // Update is called once per frame
    void Update()
    {
        // Here we called the IndexTrigger value from controller,
        // so the Primary will map to right hand when the inspectoris RTouch in Unity.
        triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

        if(isInCollider)
        {
            // not selected and pull the trigger
            if (!isSelected && triggerValue > 0.95f)
            {
                isSelected = true;
                selectedObj.transform.parent = this.transform;
                Rigidbody rb = selectedObj.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else if (isSelected && triggerValue <0.95f)
            {
                isSelected = false;
                selectedObj.transform.parent = null;
                Rigidbody rb = selectedObj.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.velocity = OVRInput.GetLocalControllerVelocity(controller);
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "roll-a-ball")
        {
            isInCollider = true;
            selectedObj = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "roll-a-ball")
        {
            isInCollider = false;
            selectedObj = null;
        }
    }
}
