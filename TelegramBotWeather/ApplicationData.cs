using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot
{
    public class ApplicationData
    {
        string outResponce;
        public Data myData;
        public ApplicationData( string _outResponce)
        {
            outResponce = _outResponce;
            JObject json = JObject.Parse(outResponce);
            myData = JsonConvert.DeserializeObject<Data>(json
                ["data"].First.ToString());
        }
    }
}
