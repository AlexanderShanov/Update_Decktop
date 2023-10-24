using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DownLoadFilesVersion.Data;
using Newtonsoft.Json;

namespace DownLoadFilesVersion
{
    class Serialize
    {
        public static BuildVersions Deserialize(string json)
        {
            BuildVersions result = JsonConvert.DeserializeObject<BuildVersions>(json);
            return result;
        }

        public static string SerializeChangeVersion(ChangeVersion chanageVersion)
        {
            return JsonConvert.SerializeObject(chanageVersion, Formatting.Indented);
        }

        public static ChangeVersionInfoFiles DeserializeListInfoFile(string json)
        {
            return JsonConvert.DeserializeObject<ChangeVersionInfoFiles>(json);
        }

        public static string SerializeCreateRequest(RequestValue request)
        {
            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

    }
}
