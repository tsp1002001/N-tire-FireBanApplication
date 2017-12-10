using FireBanApp.Dto;
using FireBanApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireBanApp.Services
{
    public interface IFireBanAppService
    {
        List<FireBanData> GetAllFireBanData();
    }

    public class FireBanAppService : IFireBanAppService
    {

        private readonly IFireBanRepo _fireBanRepo;
        public FireBanAppService()
            : this(new FireBanRepo())
        { }

        public FireBanAppService(IFireBanRepo fireBanRepo)
        {
            this._fireBanRepo = fireBanRepo;
        }

        public List<FireBanData> GetAllFireBanData()
        {
            return _fireBanRepo.GetAllFireBanData();
        }
    }

}