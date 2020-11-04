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

        private int player_number;

        public PUDMConsumer(string hostUri, int player_number) {

            this.player_number = player_number;

            this.hostUri = "ws://" + hostUri + "/socket.io/?EIO=2&transport=websocket";

            Connect();
        }

        private void Connect() {

            webSocket = new WebSocket(this.hostUri);
            webSocket.WaitTime = new TimeSpan(0, 0, 1);

            webSocket.OnOpen += (sender, e) => {
                Debug.Log("SocketIO Connected " + sender.ToString() + " " + e.ToString() + " " + webSocket.Protocol + " " + webSocket.Extensions);
            };

            webSocket.OnClose += (sender, e) => {
                Debug.LogError("SocketIO Disconnected " + sender.ToString() + " " + e.Reason);
                
                if (this.closing == false && GameManager.GetInstance(player_number) != null) {
                    webSocket.Connect();
                }
            };

            webSocket.OnError += (sender, e) => {
                Debug.LogError(sender);
                Debug.LogError(e.Exception);
                Debug.LogError(e.Message);
            };

            webSocket.OnMessage += (sender, e) =>
            {
                var data = e.Data;
                //Debug.Log(data);


                // example string:
                // '42["action",{"evenType":"action","desiredState":[{"laneID":0,"position":0,"kick":false}]}]'
                // target string:
                // {"evenType":"action","desiredState":[{"laneID":0,"position":0,"kick":false}]}

                if (data.Contains("42[\"action\""))
                {
                    //Debug.Log(data);
                    
                    data = data.Remove(0, 12);

                    const int removeFromEnd = 1;
                    data = data.Remove(data.Length - removeFromEnd, removeFromEnd);

                    data = data.Replace("\\", "");

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

            GameManager.GetInstance(this.player_number).DoActionEvent(evt);
            
        }
    }
}
