using FireBanApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FireBanApp.Data
{

    public class FireBanDataContext
    {
        private static IList<FireBanData> listFireBan = new List<FireBanData>();
        private static string lastUpdated = "";
        private static FireBanDataContext _context = null;
        private FireBanDataContext()
        {

        }

        public static FireBanDataContext GetFireBanDataContext()
        {
            if(_context == null)
            {
                _context = new FireBanDataContext();
            }

            return _context;

        }



        public IQueryable<FireBanData> FireBanDatas()
        {
            buildFireBanData();

            return listFireBan.AsQueryable();
        }

        private bool buildFireBanData()
        {
            bool isDataRead = false;
            DateTime lastUpdatedTime = (string.IsNullOrEmpty(lastUpdated)) ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(lastUpdated);
            DateTime dtNow = DateTime.Now;
            int difference = (dtNow - lastUpdatedTime).Days;
            if (difference > 0)
            {
                XmlDocument doc1 = new XmlDocument();
                doc1.Load("http://www.rfs.nsw.gov.au/feeds/fdrToban.xml");
                XmlElement root = doc1.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/FireDangerMap/District");

                foreach (XmlNode n in nodes)
                {
                    FireBanData d = getDistrict(n);
                    listFireBan.Add(d);
                }
            }

            return isDataRead;
        }

        public FireBanData getDistrict(XmlNode n)
        {
            FireBanData districtData = null;
            try
            {
                districtData = new FireBanData();
                districtData.councils = n["Councils"].InnerText;
                districtData.dangerLevelToday = n["DangerLevelToday"].InnerText;
                districtData.dangerLevelTomorrow = n["DangerLevelTomorrow"].InnerText;
                districtData.district = n["Name"].InnerText;
                districtData.fireBanToday = n["FireBanToday"].InnerText == "No" ? false : true;
                districtData.fireBanTomorrow = n["FireBanTomorrow"].InnerText == "No" ? false : true;
                districtData.regionNumber = Convert.ToInt32(n["RegionNumber"].InnerText);
            }
            catch (Exception ex)
            {
                //error reporting mech.
            }
            return districtData;
        }


    }

}
