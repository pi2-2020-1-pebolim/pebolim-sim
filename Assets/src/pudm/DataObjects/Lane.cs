using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Animations;

namespace PUDM.DataObjects
{
    [Serializable]
    class Lane
    {
        public int laneID;
        public float yPosition;
        public float rotation;

        public List<float> playerPositions;
        public Tuple<float, float> limits;
        
    }
}
