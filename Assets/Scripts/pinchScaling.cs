using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatFinger.Assets.Script {
    public class pinchScaling : MonoBehaviour {

        public Transform GameSpace;
        private OVRHand leftHand;
        private OVRHand rightHand;

        private Boolean pinching = false;

        // distance between hands when the pinch started
        private float pinchStartedDist = 0f;
    
        void Start()
        {
            leftHand = GameObject.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor/OVRHandPrefab").GetComponent<OVRHand>();
            rightHand = GameObject.Find("OVRCameraRig/TrackingSpace/RightHandAnchor/OVRHandPrefab").GetComponent<OVRHand>();

            
        }
        void Update()
        {

            if (pinching) {
                //check for middle finger pinch on the left hand, and make che cube red in this case
                if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index)) {
                    float newPinchDistance = Vector3.Distance(leftHand.GetComponent<Transform>().position, rightHand.GetComponent<Transform>().position);
                    GameSpace.localScale *= 0.5f + Sigmoid(newPinchDistance - pinchStartedDist);
                    pinchStartedDist = newPinchDistance;
                } else {
                    stopPinching();
                }    
            } else {
                if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index)) {
                    startPinching();
                }
            }
        }

        void startPinching() {
            pinching = true;
            pinchStartedDist =  Vector3.Distance(leftHand.GetComponent<Transform>().position, rightHand.GetComponent<Transform>().position);
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