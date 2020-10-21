using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PUDM.Events;
using UnityEngine;
using WebSocketSharp;

namespace PUDM
{
    class PUDMConsumer
    {

        WebSocket webSocket;
        string hostUri;
        private bool closing;

        public PUDMConsumer(string hostUri) {
            this.hostUri = "ws://" + hostUri + "/socket.io/?EIO=2&transport=websocket";
            Connect();
        }

        private void Connect() {

            webSocket = new WebSocket(this.hostUri);

            webSocket.OnOpen += async (sender, e) => {
                Debug.Log("SocketIO Connected " + sender.ToString() + " " + e.ToString());
            };

            webSocket.OnClose += async (sender, e) => {
                Debug.LogWarning("SocketIO Disconnected " + sender.ToString() + " " + e.Reason);

                if (this.closing == false) {
                    webSocket.Connect();
                }
            };

            webSocket.OnError += async (sender, e) => {
                Debug.LogError(sender);
                Debug.LogError(e.Exception);
                Debug.LogError(e.Message);
            };

            webSocket.OnMessage += async (sender, e) =>
            {
                var data = e.Data;
                
          
                // example string:
                // '42["action",{"evenType":"action","desiredState":[{"laneID":0,"position":0,"kick":false}]}]'
                // target string:
                // {"evenType":"action","desiredState":[{"laneID":0,"position":0,"kick":false}]}

                if (data.Contains("42[\"action\","))
                {
                    // remove '42["action",' from the start
                    data = data.Remove(0, 12);
                    // remove ']' from the end
                    data = data.Remove(data.Length - 1, 1);

                    ConsumeAction(ActionEvent.FromJsonString(data));
                }
            };

            Debug.Log("SocketIO Starting connection");
            webSocket.Connect();
        }

        public void Stop() {
            closing = true;
            this.webSocket.Close(CloseStatusCode.Normal, "Client shutting down");
            this.webSocket = null;
        }

        void ConsumeAction(ActionEvent evt)
        {
            
            
            
        }
    }
}
