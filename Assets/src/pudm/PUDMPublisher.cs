using Newtonsoft.Json;
using PUDM;
using PUDM.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using WebSocketSharp;

namespace Assets.src.pudm
{
    class PUDMPublisher
    {
        private readonly BlockingCollection<PUDMEvent> evtQueue;
        private Thread jobThread;
        private bool acceptEvents;
        private const int maxEvents = 30;
        
        string hostUri;

        public PUDMPublisher(string hostUri) {

            this.hostUri = "http://" + hostUri + "/api/";
            this.evtQueue = new BlockingCollection<PUDMEvent>();

            this.acceptEvents = true;

            jobThread = new Thread(new ThreadStart(Run));
            jobThread.Start();
        }

        public void Publish(PUDMEvent evt) {
            if (acceptEvents) {

                if (evtQueue.Count > maxEvents) {
                    Debug.LogWarning("Queue is full, trying to remove older evt...");
                    var removedEvent = evtQueue.Take();

                    if (removedEvent.eventType == "register") {
                        Debug.LogWarning("Removed event was of type Register, adding it back");
                        evtQueue.Add(removedEvent);
                    }
                }

                evtQueue.Add(evt);

                // Debug.Log("Added evt to queue");

            }
        }

        public void Stop() {

            this.acceptEvents = false;

            evtQueue.CompleteAdding();
            jobThread.Abort();
        }

        private void Run() {

           Debug.Log("Starting Publisher Loop");

            // TODO: non-busy waiting
           while (acceptEvents) {
                
                foreach (var evt in evtQueue.GetConsumingEnumerable()) { 

                    if (evtQueue.Count > 1) {

                        //Debug.Log("Queue at: " + evtQueue.Count + "/" + maxEvents);
                    }
                   
                    try {
                        Emit(evt);
                    }catch (Exception e) {
                        Debug.LogException(e);
                    }
                }
           }
        }

        private void Emit(PUDMEvent evt) {

            if (GameManager.Instance.delayEmit) {
                Thread.Sleep(100);
            }

            // maps the endpoint to the eventType on the server
            var endpoint = hostUri + evt.eventType;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
            
                string json = evt.ToJson();
                //Debug.Log(json);
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                var result = streamReader.ReadToEnd();
                //Debug.Log(result);
            }
        }

    }

}

