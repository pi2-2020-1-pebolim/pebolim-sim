using PUDM.DataObjects;
using PUDM.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;

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
            try {

                //JObject evtJson = JObject.Parse(data);
                
                var evt = JsonConvert.DeserializeObject<ActionEvent>(data);
                /*
                evt.timestamp = evtJson["timestamp"].ToObject<int>();
                evt.desiredState = new List<DesiredState>();

                var jStates = evtJson["desiredState"].Children().ToList();
                foreach (var jState in jStates) {
                    var desiredState = new DesiredState();
                    desiredState.laneID = jState["laneID"].ToObject<int>();
                    desiredState.position = jState["position"].ToObject<float>();
                    desiredState.kick= jState["kick"].ToObject<bool>();

                    evt.desiredState.Add(desiredState);
                }
                */
                return evt;
            } catch (Exception e) {
                Debug.LogException(e);
            }

            return null;
        }
    }
}
