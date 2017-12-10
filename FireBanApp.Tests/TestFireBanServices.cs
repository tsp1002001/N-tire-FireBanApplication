using NUnit.Framework;
using FireBanApp.Repo;
using FireBanApp.Services;
using NSubstitute;
using System.Collections.Generic;
using FireBanApp.Dto;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;


namespace FireBanApp.Tests
{
    [TestFixture]
    class TestFireBanServices
    {
        #region variables
        private IFireBanAppService _fireBanService;
        private IFireBanAppService _fireBanServiceMoq;
        private IFireBanAppService _fireBanEmptyServiceMoq;
        private IFireBanRepo _fireBanRepository;
        private List<FireBanData> _fireBanData;
        #endregion


        [TestFixtureSetUp]
        public void initializeTest()
        {
            _fireBanServiceMoq = Substitute.For<IFireBanAppService>();
            _fireBanServiceMoq.GetAllFireBanData().Returns(new List<FireBanData>{
                new FireBanData{
                    district = "Far North Coast",
                    regionNumber = 1,
                    councils = "Ballina; Byron; Clarence Valley; Kyogle; Lismore; Richmond Valley; Tweed",
                    dangerLevelToday = "LOW MODERATE",
                    dangerLevelTomorrow = "LOW MODERATE",
                    fireBanToday =  false,
                    fireBanTomorrow = false

                },
                new FireBanData{
                    district = "Illawarra/Shoalhaven",
                    regionNumber = 5,
                    councils = "Kiama; Shellharbour; Shoalhaven; Wingecarribee; Wollondilly; Wollongong",
                    dangerLevelToday = "LOW MODERATE",
                    dangerLevelTomorrow = "LOW MODERATE",
                    fireBanToday =  false,
                    fireBanTomorrow = false
                }
            });

            _fireBanEmptyServiceMoq = Substitute.For<IFireBanAppService>();
            _fireBanEmptyServiceMoq.GetAllFireBanData().Returns(new List<FireBanData> { });

            _fireBanRepository = new FireBanRepo();
            _fireBanService = new FireBanAppService(_fireBanRepository);


        }

        [TestFixtureTearDown]
        public void disposeAtEndofTest()
        {

        }

        [Test]
        public void GetAllFireDataViaMoqServiceWithValue()
        {
            _fireBanData = _fireBanServiceMoq.GetAllFireBanData();
            
            Assert.AreEqual(_fireBanData.Count, 2);
            Assert.AreEqual(_fireBanData[0].district, "Far North Coast");
        }

        [Test]
        public void GetEmptyFireDataViaMoqedService()
        {
            _fireBanData = _fireBanEmptyServiceMoq.GetAllFireBanData();
            Assert.IsTrue(true);
            Assert.AreEqual(_fireBanData.Count, 0);
        }

        [Test]
        public void GetAllFireDataViaRepo()
        {
            _fireBanData = _fireBanService.GetAllFireBanData();
            Assert.GreaterOrEqual(_fireBanData.Count, 0);
            
        }


    }
}
