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
        private float initialPosition;
        private float initialRotation;

        public LaneUpdate(int laneID, float xPosition, GameObject gameObject) {

            this.laneID = laneID;
            this.gameObject = gameObject;

            this.initialPosition = gameObject.transform.position.z;
            this.initialRotation= gameObject.transform.rotation.eulerAngles.x;
        }

        private void UpdateCurrentPosition() {
            this.currentPosition = initialPosition - gameObject.transform.position.z * -1;
        }

        private void UpdateRotation() {
            this.rotation = Mathf.Abs(
                    Mathf.RoundToInt(
                        Mathf.DeltaAngle(initialRotation, gameObject.transform.rotation.eulerAngles.x)
                    )
                );
        }

        public void Update() {
            UpdateCurrentPosition();
            UpdateRotation();
        }

    }
}
