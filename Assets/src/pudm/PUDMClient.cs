using PUDM.DataObjects;
using PUDM.Events;
using WebSocketSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Unity.Jobs;
using Assets.src.pudm;
using System.Threading;

namespace PUDM
{
    public class PUDMClient
    {
        private PUDMPublisher publisher;
        private PUDMConsumer consumer;

        public PUDMClient(string hostUri) {
            publisher = new PUDMPublisher(hostUri);
            consumer = new PUDMConsumer(hostUri);

            Hail();
        }

        public void End()
        {
            this.publisher.Stop();
            this.publisher = null;
        }
        
        public void Publish(PUDMEvent evt)
        {
            publisher.Publish(evt);
        }

        private void Hail() {
            var regEvent = new PUDM.Events.RegisterEvent(
                new PUDM.DataObjects.FieldDefinition(100, 100),
                new PUDM.DataObjects.CameraSettings(30, new Tuple<int, int>(300, 300))
            );

            Publish(regEvent);
        }

        public void SendUpdate(byte[] cameraImage, List<LaneUpdate> currentLanesState) {

            var updateEvent = new StatusUpdateEvent(new DataObjects.Camera(cameraImage), currentLanesState);
            Publish(updateEvent);
        }

    }

}
