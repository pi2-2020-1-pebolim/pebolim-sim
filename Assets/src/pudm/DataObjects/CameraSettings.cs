using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Animations;

namespace PUDM.DataObjects
{
    [Serializable]
    public class CameraSettings
    {
        public int framerate;
        public Tuple<int, int> resolution;

        public CameraSettings(int framerate, Tuple<int, int> resolution) {
            this.framerate = framerate;
            this.resolution = resolution;
        }
    }
}
