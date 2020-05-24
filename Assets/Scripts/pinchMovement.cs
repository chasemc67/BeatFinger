using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatFinger.Assets.Script {
    public class pinchMovement : MonoBehaviour {

        public Transform GameSpace;
        private OVRHand leftHand;
        private OVRHand rightHand;

        private Boolean pinching = false;

        // distance between hands when the pinch started
        private Vector3 pinchStartedLoc = new Vector3(0f, 0f, 0f);
        private OVRHand pinchHand;
    
        void Start()
        {
            leftHand = GameObject.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor/OVRHandPrefab").GetComponent<OVRHand>();
            rightHand = GameObject.Find("OVRCameraRig/TrackingSpace/RightHandAnchor/OVRHandPrefab").GetComponent<OVRHand>();
        }
        void Update()
        {

            if (pinching) {
                //check for middle finger pinch on the left hand, and make che cube red in this case
                if ((rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) || leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index)) && !(rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index))) {
                    GameSpace.position += (pinchHand.GetComponent<Transform>().position - pinchStartedLoc);
                    pinchStartedLoc = pinchHand.GetComponent<Transform>().position;
                } else {
                    stopPinching();
                }    
            } else {
                if ((rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) || leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index)) && !(rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index))) {
                    startPinching();
                }
            }
        }

        void startPinching() {
            pinching = true;

            if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index)) {
                pinchHand = rightHand;
            } else {
                pinchHand = leftHand;
            }

            pinchStartedLoc =  pinchHand.GetComponent<Transform>().position;
        }

        void stopPinching() {
            pinching = false;
        }

        public static float Sigmoid(float value) {
            float k = (float)Math.Exp((double)value);
            return k / (1.0f + k);
        }
    }
}