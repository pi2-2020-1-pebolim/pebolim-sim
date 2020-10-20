using PUDM.DataObjects;
using PUDM.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PUDM.Events
{
    public class ActionEvent: PUDMEvent
    {

       public override string eventType { get { return "register"; } }

        public List<DesiredState> desiredState;

        public ActionEvent() : base(){
            
        }


        public static ActionEvent FromJsonString(string data)
        {
            
            var evtObjetc = JsonConvert.DeserializeObject<ActionEvent>(data);
            
            return evtObjetc;
        }
    }
}
