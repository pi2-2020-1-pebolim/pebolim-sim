using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUDM.DataObjects
{
    [Serializable]
    public class DesiredState
    {
        public int laneID;
        public float position;
        public float rotation;
    }
}
