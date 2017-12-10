using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBanApp.Dto
{
    public class FireBanData
    {
        public string district { get; set; }
        public int regionNumber { get; set; }
        public string councils { get; set; }
        public string dangerLevelToday { get; set; }
        public string dangerLevelTomorrow { get; set; }
        public bool fireBanToday { get; set; }
        public bool fireBanTomorrow { get; set; }

    }


    public class FireBanResult
    {
        public string message { get; set; }
        public bool success { get; set; }
        public List<FireBanData> fireBanData { get; set; }
    }
}
