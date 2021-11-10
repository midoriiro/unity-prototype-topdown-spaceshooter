using System;
using System.Collections.Generic;
using Systems.Math;
using Behaviours.Physics.Compensators;
using Core.Enums;
using UnityEngine;

namespace Behaviours.Physics.Rigidbody
{
    public class RigidbodyConstraints : MonoBehaviour
    {
        public float speed;
        public FloatRange range;
        public float acceleration;
        public float deceleration;
        public float compensation;
        public bool stabilised;
        public bool clamped;
        public ForceType forceType;
        public ForceMode forceMode;
        public List<Compensator> compensators;
    }
}