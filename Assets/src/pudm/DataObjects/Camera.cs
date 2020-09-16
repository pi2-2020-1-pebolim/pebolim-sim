using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Animations;

namespace PUDM.DataObjects
{
    [Serializable]
    public class Camera
    {
        public string image;

        public Camera(byte[] image) {
            this.image = Convert.ToBase64String(image);
        }

        public Camera(string base64Image) {
            this.image = base64Image;
        }
    }
}
