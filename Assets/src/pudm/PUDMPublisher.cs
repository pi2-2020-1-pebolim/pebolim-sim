using Newtonsoft.Json;
using PUDM.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;

namespace Assets.src.pudm
{
    class PUDMPublisher
    {
        private readonly ConcurrentQueue<PUDMEvent> evtQueue;
        private Thread jobThread;
        private bool acceptEvents;

        WebSocket webSocket;

        string hostUri;

        public PUDMPublisher(string hostUri) { 

            this.hostUri = "ws://" + hostUri + "/socket.io/?EIO=2&transport=websocket";
            this.evtQueue = new ConcurrentQueue<PUDMEvent>();

            this.acceptEvents = true;

            jobThread = new Thread(new ThreadStart(Run));
            jobThread.Start();
        }

        public void Publish(PUDMEvent evt) {
            if (acceptEvents)
                evtQueue.Enqueue(evt);
        }

        public void Stop() {
            this.acceptEvents = false;           
            this.webSocket.Close(CloseStatusCode.Normal, "Client shutting down");


            while (evtQueue.TryDequeue(out _)) {}

            jobThread.Abort();
            
            this.webSocket = null;
        }

        private async void Run() {

            this.Connect();

           while (acceptEvents) {
                PUDMEvent evt;

                while (evtQueue.TryDequeue(out evt)) {
                    Emit(evt);
                }
            }
        }

        private void Connect() {

            webSocket = new WebSocket(this.hostUri);

            webSocket.OnOpen += async (sender, e) => {
                Debug.Log("SocketIO Connected");
                Hail();
            };

            webSocket.OnClose += async (sender, e) => {
                Debug.LogWarning("SocketIO Disconnected");
            };

            webSocket.OnError += async (sender, e) => {
                Debug.LogError(e);
            };

            webSocket.OnMessage += async (sender, e) => {

                Debug.Log("Message: " + e.Data);
            };

            Debug.Log("SocketIO Starting connection");
            webSocket.Connect();
        }

        private void Hail() {
            var regEvent = new PUDM.Events.RegisterEvent(
                new PUDM.DataObjects.FieldDefinition(100, 100),
                new PUDM.DataObjects.CameraSettings(30, new Tuple<int, int>(300, 300))
            );

            Publish(regEvent);
        }

        private void Emit(PUDMEvent evt) {
            var packet = MakePacket(evt);

            Debug.Log(packet);
            this.webSocket.Send(packet);
        }

        private string MakePacket(PUDMEvent evt) {
            var packet = new[] { evt.eventType, evt.ToJson()};
            var packetJson = JsonConvert.SerializeObject(packet);

            return "42" + packetJson;
        }

    }

}

