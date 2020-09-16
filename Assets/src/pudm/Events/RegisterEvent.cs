using PUDM.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PUDM.Events
{
    [Serializable]
    public class RegisterEvent : PUDM.Events.PUDMEvent
    {
        public override string eventType { get { return "register"; } }

        public PUDM.DataObjects.FieldDefinition fieldDefinition;
        public PUDM.DataObjects.CameraSettings cameraSettings;

        public RegisterEvent(FieldDefinition fieldDefinition, CameraSettings cameraSettings) : base() {
            
        }

    }
}
