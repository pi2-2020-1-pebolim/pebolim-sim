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

namespace PUDM
{
    public class PUDMClient
    {

        string hostUri;
        // SocketIO from https://github.com/doghappy/socket.io-client-csharp
        WebSocket webSocket;
        public bool Connected { get => this.webSocket.IsAlive;}

        private readonly Queue<Tuple<string, string>> evtQueue;

        public PUDMClient(string hostUri) {
            
            this.hostUri = "ws://" + hostUri + "/socket.io/?EIO=2&transport=websocket";
            evtQueue = new Queue<Tuple<string, string>>();
        }

        public async Task Connect()
        {
    
            webSocket = new WebSocket(this.hostUri);

            webSocket.OnOpen += async (sender, e) =>
            {
                Debug.Log("SocketIO Connected");
                Hail();
            };

            webSocket.OnClose += async (sender, e) =>
            {
                Debug.LogWarning("SocketIO Disconnected");
            };
            
            webSocket.OnError += async (sender, e) =>
            {
                Debug.LogError(e);
            };

            webSocket.OnMessage += async (sender, e) =>
            {
                Debug.Log("Message: " + e.ToString());
            };

            Debug.Log("SocketIO Starting connection");
            webSocket.ConnectAsync();
        }

        public void Disconnect()
        {
            this.webSocket.Close(CloseStatusCode.Normal, "Client shutting down");
            this.webSocket = null;
        }

        public void PrepareEvent(string eventType, string json)
        {
            evtQueue.Enqueue(new Tuple<string, string>(eventType, json));
        }

        public async Task PublishQueue()
        {
            if (evtQueue.Count > 0)
            {
                await this.Emit(evtQueue.Dequeue());
            }
        }

        private async Task Emit(Tuple<string, string> evt)
        {
            var packet = MakePacket(evt);

            //Debug.Log("Emitting: " + packet);
            this.webSocket.SendAsync(MakePacket(evt), null);
        }
        
        private string MakePacket(Tuple<string, string> evt)
        {
            var packet = new [] {evt.Item1, evt.Item2};
            var packetJson = JsonConvert.SerializeObject(packet);

            return "42" + packetJson;
        }

        private void Hail() {
            var regEvent = new PUDM.Events.RegisterEvent(
                new DataObjects.FieldDefinition(100, 100),
                new DataObjects.CameraSettings(30, new Tuple<int, int>(300, 300))
            );
  
           PrepareEvent("register", regEvent.ToJson());
        }

        public void SendUpdate(byte[] cameraImage, List<Lane> currentLanesState) {

            var updateEvent = new StatusUpdateEvent(new DataObjects.Camera(cameraImage), currentLanesState);
            PrepareEvent("status_update", updateEvent.ToJson());
        }

    }

}
