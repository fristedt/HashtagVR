using UnityEngine;
using System.Collections;

public class LeftController : MonoBehaviour {
    SteamVR_TrackedObject trackedObj;
    public GameObject satellitePrefab;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {
        var thisDevice = SteamVR_Controller.Input((int)trackedObj.index);
        if (thisDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Grip))
        {
            GameObject satellite = GameObject.Find("Satellite");
            if (satellite == null)
                satellite = GameObject.Find("Satellite(Clone)");
            Destroy(satellite);
            Instantiate(satellitePrefab);
        }
    }

}
