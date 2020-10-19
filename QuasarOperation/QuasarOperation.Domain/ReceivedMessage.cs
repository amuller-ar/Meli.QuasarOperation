using System;
using System.Collections.Generic;
using System.Text;

namespace QuasarOperation.Domain
{
    public class ReceivedMessage
    {
        public string SatelliteName { get; set; }
        public double Distance { get; set; }
        public string[] Message { get; set; }
    }
}
