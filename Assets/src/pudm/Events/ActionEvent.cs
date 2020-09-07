using PUDM.DataObjects;
using PUDM.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUDM.Events
{
    class ActionEvent: PUDMEvent
    {

         public override string eventType { get { return "register"; } }

        public List<DesiredState> desiredState;
    }
}
