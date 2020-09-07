using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUDM.Events
{
    [Serializable]
    class RegisterEvent : PUDM.Events.PUDMEvent
    {
        public override string eventType { get { return "register"; } }

        public PUDM.DataObjects.FieldDefinition fieldDefinition;
        public PUDM.DataObjects.CameraSettings cameraSettings;

    }
}
