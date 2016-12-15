//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveController : MonoBehaviour
{
    public ViveController otherController;
    [HideInInspector]
    public GameObject heldObject;
    [HideInInspector]
    public Vector3 grabbedPosition;
    public GameObject distortionSphere;

    private List<GameObject> inTrigger;

    SteamVR_TrackedObject trackedObj;
    SteamVR_TrackedObject otherTrackedObj;
    FixedJoint joint;
    GameObject camera;

    Vector3[] positions = new Vector3[2];
    int grabbableMask;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        otherTrackedObj = otherController.GetComponent<SteamVR_TrackedObject>();
        inTrigger = new List<GameObject>();

        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("Grabbable");
    }

    void Start()
    {
        camera = GameObject.Find("Camera (eye)");
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.startColor = Color.cyan;
        lr.endColor = Color.cyan;
        grabbableMask = LayerMask.GetMask("Grabbable");

        distortionSphere.SetActive(false);
    }

    void FixedUpdate()
    {
        int myIndex = (int)trackedObj.index;
        var thisDevice = SteamVR_Controller.Input(myIndex);

        LineRenderer lr = GetComponent<LineRenderer>();
        positions[0] = transform.position;

        if (thisDevice.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            positions[1] = transform.position + transform.forward * 1000;
            lr.enabled = true;
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, grabbableMask))
            {
                Vector3 diff = (transform.position - hit.transform.position);
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(diff.normalized * 400);
                    lr.startColor = Color.red;
                    lr.endColor = Color.red;
                    positions[1] = hit.point;
                }
            }
            else
            {
                lr.startColor = Color.cyan;
                lr.endColor = Color.cyan;
            }
            lr.SetPositions(positions);
        }
        else
        {
            lr.enabled = false;
        }

        //if (thisDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    distortionSphere.SetActive(true);
        //    Collider[] objs = Physics.OverlapSphere(transform.position, distortionSphere.transform.localScale.x, grabbableMask);

        //    Vector2 touch = SteamVR_Controller.Input(myIndex).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        //    float scale = touch.y / 100;

        //    foreach (Collider obj in objs)
        //    {
        //        Vector3 diff = transform.position - obj.transform.position;
        //        obj.GetComponent<Rigidbody>().AddForce(diff.normalized * 300);
        //        obj.transform.localScale += new Vector3(scale, scale, scale);
        //        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //    }


        //}
        //else
        //{
        //    distortionSphere.SetActive(false);
        //}

        if (thisDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && heldObject == null)
        {

            float minDist = float.MaxValue;
            GameObject closest = null;
            foreach (GameObject go in inTrigger)
            {
                if (go == null)
                    continue;
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
                heldObject = closest;
                grabbedPosition = transform.position;
                heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Grabbable grabbable = heldObject.GetComponent<Grabbable>();
                if (grabbable.currentlyHeldBy == -1)
                    grabbable.currentlyHeldBy = (myIndex);

            }
        }

        if (heldObject != null && thisDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touch = SteamVR_Controller.Input(myIndex).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            float scale = touch.y / 50;
            Debug.Log(scale);

            heldObject.transform.localScale += new Vector3(scale, scale, scale);
        }

        //try
        //{
        //    var otherDevice = SteamVR_Controller.Input((int)otherTrackedObj.index);
        //    if (thisDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && otherDevice.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        //    {
        //        Grabbable grabbable = heldObject.GetComponent<Grabbable>();
        //        if (heldObject == otherController.heldObject)
        //        {
        //            float origOffset = (grabbedPosition - otherController.grabbedPosition).magnitude;
        //            float offset = (transform.position - otherController.transform.position).magnitude;
        //            float scaleFactor = offset / origOffset;
        //            scaleFactor = Mathf.Clamp(scaleFactor, .99f, 1.01f);
        //            heldObject.transform.localScale = new Vector3(offset, offset, offset);

        //            //Transform childYeah = heldObject.transform.GetChild(0);
        //            //if (Vector3.Distance(camera.transform.position, childYeah.position) > 0.35f)
        //            //{
        //            //    childYeah.gameObject.SetActive(true);
        //            //}
        //            //else
        //            //{
        //            //    childYeah.gameObject.SetActive(false);
        //            //}
        //            //float childScaleMultiplier = 2f;
        //            //childYeah.localScale = new Vector3(scale.x * childScaleMultiplier, scale.y * childScaleMultiplier, 0.1f);
        //        }
        //    }
        //}
        //catch (Exception e)
        //{

        //}


        if (thisDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) && heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.velocity = trackedObj.transform.parent.TransformVector(thisDevice.velocity);
            rb.angularVelocity = trackedObj.transform.parent.TransformVector(thisDevice.angularVelocity);

            Grabbable grabbable = heldObject.GetComponent<Grabbable>();
            if (grabbable.currentlyHeldBy == myIndex)
                heldObject.GetComponent<Grabbable>().currentlyHeldBy = -1;
            heldObject.transform.parent = null;
            heldObject = null;
            grabbedPosition = Vector3.zero;

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
