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
    public class LaneDefinition
    {
        public int laneID;
        public float xPosition;
        public int playerCount;
        public float playerDistance;
        public float movementLimit;

        private GameObject gameObject;

        public LaneDefinition(int laneID, float xPosition, GameObject gameObject) {
            
            this.laneID = laneID;
            this.gameObject = gameObject;

            this.xPosition = xPosition;
        }
    }
}
