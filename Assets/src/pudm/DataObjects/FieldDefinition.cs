using System;
using System.Collections.Generic;

namespace PUDM.DataObjects
{
    [Serializable]
    public class FieldDefinition
    {
        public List<float> dimensions;
        public List<LaneDefinition> lanes;

        public FieldDefinition(float width, float length, List<LaneDefinition> lanes) {
            dimensions = new List<float>();
            dimensions.Add(width);
            dimensions.Add(length);
            this.lanes = lanes;
        }

        public FieldDefinition(Tuple<float, float> dimensions, List<LaneDefinition> lanes) {
            
            this.dimensions = new List<float>();
            this.dimensions.Add(dimensions.Item1);
            this.dimensions.Add(dimensions.Item2);

            this.lanes = lanes;
        }
    }
}
