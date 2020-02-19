using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    #region Anchors
    [SerializeField]
    private GameObject leftAnchor;
    [SerializeField]
    private GameObject rightAnchor;
    [SerializeField]
    private GameObject headAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> controllerSets = null;
    private OVRInput.Controller inputSource = OVRInput.Controller.None;
    private OVRInput.Controller controller = OVRInput.Controller.None;
    private bool inputActive = true;
    #endregion

    private void Start()
    {
        
    }

    private void Update()
    {
        //Check if headset is on

        //Check if a controller exists

        //Check where the input is coming from (controller or headset)
        CheckInputSource();

        //Check for actual input
        Input();
    }

    private void CheckInputSource()
    {
        //Left remote
        //Check for any button pressed on LTrackedRemote (left side of the camera)
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTrackedRemote))
        {

        }

        //Right remote
        //Check for any button pressed on RTrackedRemote (right side of the camera)
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTrackedRemote))
        {

        }

        //Headset (GearVR Only)
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Touchpad))
        {

        }
    }

    private void Input()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {

        }


    }
}
