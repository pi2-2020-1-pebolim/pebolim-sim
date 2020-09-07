using SocketIOClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUDM
{
    public class PUDMClient
    {

        string hostUri;
        // SocketIO from https://github.com/doghappy/socket.io-client-csharp
        SocketIO client;

        public PUDMClient(string hostUri) {
            this.hostUri = hostUri;

            // TODO: connect in the background
            
            this.Connect();
        }

        bool Connected() {
            return this.client == null || this.client.Connected;
        }

        private void Connect() {

            var options = new SocketIOOptions();
            options.Reconnection = true;

            client = new SocketIO(hostUri, options);
        }

        private void Hail() {

        }

    }

}
