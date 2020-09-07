using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUDM.Events
{
    [Serializable]
    class StatusUpdateEvent : PUDM.Events.PUDMEvent
    {
        public override string eventType { get { return "status_update"; } }

        PUDM.DataObjects.Camera camera;
        List<PUDM.DataObjects.Lane> lanes;

    }
}
