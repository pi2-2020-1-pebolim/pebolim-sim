using Newtonsoft.Json;
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
using WebSocketSharp;

namespace Assets.src.pudm
{
    class PUDMPublisher
    {
        private readonly BlockingCollection<PUDMEvent> evtQueue;
        private Thread jobThread;
        private bool acceptEvents;
        private const int maxEvents = 180;
        
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
                evtQueue.Add(evt);

                Debug.Log("Added evt to queue");

                if (evtQueue.Count > maxEvents) {
              //      evtQueue.TryDequeue(out _);
                }
            }
        }

        public void Stop() {

            this.acceptEvents = false;

            evtQueue.CompleteAdding();
            // empty the job queue before aborting thread
            //while (evtQueue.TryDequeue(out _)) {}
            jobThread.Abort();
        }

        private void Run() {

           Debug.Log("Starting Publisher Loop");

            // TODO: non-busy waiting
           while (acceptEvents) {
                
                foreach (var evt in evtQueue.GetConsumingEnumerable()) { 

                    Debug.Log("Queue at: " + evtQueue.Count + "/" + maxEvents);
                   
                    try {
                        Emit(evt);
                    }catch (Exception e) {
                        Debug.LogException(e);
                    }
                }
           }
        }

        private void Emit(PUDMEvent evt) {

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
                Debug.Log(result);
            }
        }

    }

}

