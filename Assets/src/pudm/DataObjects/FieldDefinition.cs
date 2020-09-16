using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUDM.DataObjects
{
    [Serializable]
    public class FieldDefinition
    {
        public Tuple<float, float> dimensions;

        public FieldDefinition(float width, float length) {
            dimensions = new Tuple<float, float>(width, length);
        }

        public FieldDefinition(Tuple<float, float> dimensions) {
            this.dimensions = dimensions;
        }
    }
}
