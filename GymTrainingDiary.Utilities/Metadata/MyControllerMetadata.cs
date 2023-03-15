using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Utilities.Metadata
{
    public class MyControllerMetadata
    {
        public string ControllerName { get; set; }

        public List<MyEndpointMetadata> EndpointsInfo { get; set; }
    }
}
