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
using UnityEditorInternal;
using UnityEngine;
using Unity.Jobs;
using Assets.src.pudm;
using System.Threading;

namespace PUDM
{
    public class PUDMClient
    {
        string hostUri;
        PUDMPublisher publisher;

        public PUDMClient(string hostUri) {
            publisher = new PUDMPublisher(hostUri);

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

        public void SendUpdate(byte[] cameraImage, List<Lane> currentLanesState) {

            var updateEvent = new StatusUpdateEvent(new DataObjects.Camera(cameraImage), currentLanesState);
            Publish(updateEvent);
        }

    }

}
