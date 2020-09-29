using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Debug.Log("SocketIO Starting connection");
            webSocket.Connect();
        }

        public void Stop() {
            closing = true;
            this.webSocket.Close(CloseStatusCode.Normal, "Client shutting down");
            this.webSocket = null;
        }
    }
}
