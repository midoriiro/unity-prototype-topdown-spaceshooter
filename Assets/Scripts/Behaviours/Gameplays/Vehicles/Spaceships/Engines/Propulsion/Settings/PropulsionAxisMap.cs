using System;
using Systems.Transforms;

namespace Behaviours.Gameplays.Vehicles.Spaceships.Engines.Propulsion.Settings
{
    [Serializable]
    public class PropulsionAxisMap
    {
        public AxisMap velocity;
        public AxisMap pitch;
        public AxisMap yaw;
        public AxisMap roll;
        public AxisMap strafe; // TODO delete this
    }
}