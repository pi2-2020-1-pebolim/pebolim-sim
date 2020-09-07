using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUDM.Events
{
    [Serializable]
    public abstract class PUDMEvent
    {

        public int timestamp;
        public const int version = 1;
        public abstract string eventType { get; }

        public string ToJson() {
            // this will serialize all public attributes to json
            return JsonUtility.ToJson(this); 
        }

    }

    

}
