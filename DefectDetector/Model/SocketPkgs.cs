using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DefectDetector.Model
{
    public class CommandPkg
    {
        [JsonPropertyName("pkg_type")]
        public string Pkg_type { get; set; } = "command";

        [JsonPropertyName("command")]
        public string Command { get; set; }

        [JsonPropertyName("params")]
        public string Params { get; set; }
    }

    public class ResultPkg
    {
        private List<List<double>> final_boxes;
        private List<int> final_class_ids;
        private string pkg_type;

        [JsonPropertyName("pkg_type")]
        public string Pkg_type
        {
            get { return pkg_type; }
            set
            {
                pkg_type = value;
            }
        }

        [JsonPropertyName("final_boxes")]
        public List<List<double>> Final_boxes
        {
            get { return final_boxes; }
            set
            {
                final_boxes = value;
            }
        }

        [JsonPropertyName("final_class_ids")]
        public List<int> Final_class_ids
        {
            get { return final_class_ids; }
            set
            {
                final_class_ids = value;
            }
        }

    }
}
