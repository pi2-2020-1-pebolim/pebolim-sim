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
        private List<Kicker> kickers;

        public LaneDefinition(int laneID, float xPosition, float movementLimit, GameObject gameObject) {
            
            this.laneID = laneID;
            this.gameObject = gameObject;

            this.xPosition = xPosition;
            this.movementLimit = movementLimit;

            this.playerCount = GetPlayerCount();
            this.playerDistance = GetDistance();
        }

        private int GetPlayerCount() {
            kickers = gameObject.GetComponentsInChildren<Kicker>().ToList();

            return kickers.Count;
        }

        private float GetDistance() {

            if (playerCount <= 1)
                return 0;

            kickers.OrderBy(kicker => kicker.transform.position.z);
            var fisrt = kickers[0];
            var second = kickers[1];

            var distance = second.transform.position.z - fisrt.transform.position.z;

            return distance;
        }

        public GameObject GetGameObject() {
            return this.gameObject;
        }
    }
}
