using System;
using System.Collections.Generic;

namespace PUDM.DataObjects
{
    [Serializable]
    public class FieldDefinition
    {
        public Tuple<float, float> dimensions;
        public List<LaneDefinition> lanes;

        public FieldDefinition(float width, float length, List<LaneDefinition> lanes) {
            dimensions = new Tuple<float, float>(width, length);
            this.lanes = lanes;
        }

        public FieldDefinition(Tuple<float, float> dimensions, List<LaneDefinition> lanes) {
            this.dimensions = dimensions;
            this.lanes = lanes;
        }
    }
}
