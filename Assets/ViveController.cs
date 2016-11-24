//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveController : MonoBehaviour
{
    public ViveController otherController;
    public GameObject heldObject;

    private List<GameObject> inTrigger;

    SteamVR_TrackedObject trackedObj;
    SteamVR_TrackedObject otherTrackedObj;
    FixedJoint joint;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        otherTrackedObj = otherController.GetComponent<SteamVR_TrackedObject>();
        inTrigger = new List<GameObject>();

        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Grabbable");
        foreach (GameObject g in allNodes)
        {
            Transform childYeah = g.transform.GetChild(0);
            childYeah.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        var thisDevice = SteamVR_Controller.Input((int)trackedObj.index);

        if (thisDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && heldObject == null)
        {

            float minDist = float.MaxValue;
            GameObject closest = null;
            foreach (GameObject go in inTrigger)
            {
                if (!go.CompareTag("Grabbable"))
                    continue;
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance < minDist)
                {
                    minDist = distance;
                    closest = go;
                }
            }
            if (closest != null)
            {
                closest.transform.parent = transform;
                closest.transform.localPosition = Vector3.zero;
                heldObject = closest;
            }
        }

        try
        {
            var otherDevice = SteamVR_Controller.Input((int)otherTrackedObj.index);
            if (thisDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && otherDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (heldObject == otherController.heldObject)
                {
                    Vector3 offset = transform.position - otherController.transform.position;
                    heldObject.transform.position = transform.position - offset / 2;
                    Vector3 scale = new Vector3(offset.magnitude, offset.magnitude, offset.magnitude);
                    heldObject.transform.localScale = scale;

                    Transform childYeah = heldObject.transform.GetChild(0);
                    childYeah.gameObject.SetActive(true);
                    float childScaleMultiplier = 5f;
                    childYeah.localScale = new Vector3(scale.x * childScaleMultiplier, scale.y * childScaleMultiplier, 0.1f);
                }
            }
        }
        catch (Exception e)
        {

        }


        if (thisDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) && heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.velocity = trackedObj.transform.parent.TransformVector(thisDevice.velocity);
            rb.angularVelocity = trackedObj.transform.parent.TransformVector(thisDevice.angularVelocity);

            Transform childYeah = heldObject.transform.GetChild(0);
            childYeah.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            childYeah.gameObject.SetActive(false);

            heldObject.transform.parent = null;
            heldObject = null;

        }
    }

    void OnTriggerEnter(Collider collider)
    {
        inTrigger.Add(collider.gameObject);
    }
    void OnTriggerExit(Collider collider)
    {
        inTrigger.Remove(collider.gameObject);
    }
}
