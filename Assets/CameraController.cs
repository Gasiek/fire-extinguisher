using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform VRHeadset;
    private Vector3 posOffset;
    private Quaternion rotOffset;
    private bool VRHeadsetPosChanged = false;
    private Vector3 initialVRHeadsetPos;

    void Start()
    {
        Debug.Log(VRHeadset.position);
        initialVRHeadsetPos = VRHeadset.position;
    }
    void Update()
    {
        if (!VRHeadsetPosChanged && VRHeadset.position != initialVRHeadsetPos)
        {
            VRHeadsetPosChanged = true;
            posOffset = transform.position - VRHeadset.position;
            rotOffset = transform.rotation * Quaternion.Inverse(VRHeadset.rotation);
        }
        transform.position = VRHeadset.position + posOffset;
        transform.rotation = VRHeadset.rotation * rotOffset;
    }
}
