using PUDM.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUDM.Events
{
    [Serializable]
    public class StatusUpdateEvent : PUDM.Events.PUDMEvent
    {
        public override string eventType { get { return "status_update"; } }

        public PUDM.DataObjects.Camera camera;
        public List<PUDM.DataObjects.LaneUpdate> lanes;

        public StatusUpdateEvent(Camera camera, List<LaneUpdate> lanes) : base(){
            this.camera = camera;
            this.lanes = lanes;
        }

    }
}
