using FireBanApp.Data;
using FireBanApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireBanApp.Repo
{
    public interface IFireBanRepo
    {
        List<FireBanData> GetAllFireBanData();
    }

    public class FireBanRepo : IFireBanRepo
    {
        public List<FireBanData> GetAllFireBanData()
        {
                var context = FireBanDataContext.GetFireBanDataContext();
                var distsFireData = context.FireBanDatas().ToList();
                List<FireBanData> result = new List<FireBanData>();
                foreach (var dt in distsFireData)
                {
                    result.Add(new FireBanData()
                    {
                        district = dt.district,
                        regionNumber = dt.regionNumber,
                        councils = dt.councils,
                        dangerLevelToday = dt.dangerLevelToday,
                        dangerLevelTomorrow = dt.dangerLevelTomorrow,
                        fireBanToday = dt.fireBanToday,
                        fireBanTomorrow = dt.fireBanTomorrow
                    });
                }
                return result;
        }
    }
}