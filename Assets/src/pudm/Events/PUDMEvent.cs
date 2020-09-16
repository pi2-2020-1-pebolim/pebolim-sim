using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace PUDM.Events
{
    [Serializable]
    public abstract class PUDMEvent
    {

        public long timestamp;
        public const int version = 1;
        public abstract string eventType { get; }

        protected PUDMEvent() {

            var now = (DateTimeOffset)DateTime.UtcNow;
            timestamp = now.ToUnixTimeSeconds();
        }

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

    

}
