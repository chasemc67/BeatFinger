using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatFinger.Assets.Script {
    public class pinchMovement {

        public Transform GameSpace;
        private OVRHand leftHand;
        private OVRHand rightHand;
    
        void Start()
        {
            leftHand = GameObject.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor/OVRHandPrefab").GetComponent<OVRHand>();
            rightHand = GameObject.Find("OVRCameraRig/TrackingSpace/RightHandAnchor/OVRHandPrefab").GetComponent<OVRHand>();

            
        }
        void Update()
        {
            //check for middle finger pinch on the left hand, and make che cube red in this case
            if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) || leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index)) {
                GameSpace.localScale += new Vector3(1f,1f,1f);
            }

            if (leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index)) {
                GameSpace.localScale -= new Vector3(1f,1f,1f);
            }
        }
    }
}