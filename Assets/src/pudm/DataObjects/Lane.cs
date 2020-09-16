using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;

namespace PUDM.DataObjects
{
    [Serializable]
    public class Lane
    {
        public int laneID;
        public float yPosition;
        public float rotation;

        public List<float> playerPositions;
        public Tuple<float, float> limits;

        private GameObject gameObject;

        public Lane(int laneID, float yPosition, GameObject gameObject) {
            
            this.laneID = laneID;
            this.gameObject = gameObject;

            this.yPosition = yPosition;
        }
    }
}
