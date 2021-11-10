using System;
using System.Collections.Generic;
using System.Linq;
using Systems.Transforms;
using Systems.Transforms.Extensions;
using Behaviours.Gameplays.Inputs;
using Behaviours.Gameplays.Vehicles.Spaceships.Engines.Propulsion.Settings;
using UnityEngine;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines
{
    public abstract class PropulsionController : MonoBehaviour
    {
        public PropulsionAxisMap axisMap;
        public GamePadInputController controller;
        public List<BaseEngine> propulsors;
    }
}