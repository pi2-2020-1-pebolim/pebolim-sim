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
    public class LaneUpdate
    {
        public int laneID;
        public float currentPosition;
        public int rotation;

        private GameObject gameObject;

        public LaneUpdate(int laneID, float xPosition, GameObject gameObject) {

            this.laneID = laneID;
            this.gameObject = gameObject;
        }
    }
}
