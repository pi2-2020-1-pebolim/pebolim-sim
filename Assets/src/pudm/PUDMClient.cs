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

        public PUDMClient(string hostUri, FieldDefinition field, CameraSettings camera) {
            publisher = new PUDMPublisher(hostUri);
            consumer = new PUDMConsumer(hostUri);

            Hail(field, camera);
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

        private void Hail(FieldDefinition field, CameraSettings camera) {
            var regEvent = new PUDM.Events.RegisterEvent(
                field,
                camera
            );

            Publish(regEvent);
        }

        public void SendUpdate(byte[] cameraImage, List<LaneUpdate> currentLanesState) {

            var updateEvent = new StatusUpdateEvent(new DataObjects.Camera(cameraImage), currentLanesState);
            Publish(updateEvent);
        }

    }

}
