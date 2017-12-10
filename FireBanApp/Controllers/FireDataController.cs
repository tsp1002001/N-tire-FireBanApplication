using FireBanApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;

namespace FireBanApp.Controllers
{
    public class FireDataController : ApiController
    {
        IFireBanAppService _mService;
        public FireDataController(): this(new FireBanAppService())
        {

        }

        public FireDataController(IFireBanAppService service)
        {
            _mService = service;
        }

        [CacheOutputUntilToday(23, 55)]
        public IHttpActionResult Get()
        {
            bool isSuccess = false;
            string msg = "FireBan data is Empty";

            Dto.FireBanResult fireBanResult = new Dto.FireBanResult();
            fireBanResult.fireBanData = new List<Dto.FireBanData>();
            fireBanResult.message = msg;
            fireBanResult.success = isSuccess;

            List<Dto.FireBanData> fireBanData = _mService.GetAllFireBanData();
            if (fireBanData.Count > 0)
            {
                isSuccess = true;

                fireBanResult.fireBanData = fireBanData;
                fireBanResult.message = "Success";
                fireBanResult.success = isSuccess;

            }

            return Ok(fireBanResult);
        }
    }
}
