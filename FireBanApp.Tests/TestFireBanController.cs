using System;

using NUnit.Framework;
using FireBanApp.Repo;
using FireBanApp.Services;
using FireBanApp.Controllers;
using NSubstitute;
using System.Collections.Generic;
using FireBanApp.Dto;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace FireBanApp.Tests
{
    [TestFixture]
    public class TestFireBanController
    {
        #region variables
        private FireDataController _fireBanCtlMoq;
        private FireDataController _fireBanCtlwithEmptyServ;
        private FireDataController _fireBanCtl;
        private IFireBanAppService _fireBanService;
        private IFireBanAppService _fireBanServiceMoq;
        private IFireBanAppService _fireBanEmptyServiceMoq;
        private IFireBanRepo _fireBanRepository;
        private HttpClient _client;
        private HttpResponseMessage UrlResponse;
        private const string ServiceBaseURL = "http://localhost:30687/";
        #endregion


        [TestFixtureSetUp]
        public void initializeTest(){
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
            _fireBanCtlMoq = new FireDataController(_fireBanServiceMoq);


            _fireBanEmptyServiceMoq = Substitute.For<IFireBanAppService>();
            _fireBanEmptyServiceMoq.GetAllFireBanData().Returns(new List<FireBanData> { });
            _fireBanCtlwithEmptyServ = new FireDataController(_fireBanEmptyServiceMoq);

            _fireBanRepository = new FireBanRepo();
            _fireBanService = new FireBanAppService(_fireBanRepository);
            _fireBanCtl = new FireDataController(_fireBanService);


            _client = new HttpClient();

            _client.BaseAddress = new Uri(ServiceBaseURL);

            UrlResponse = _client.GetAsync("api/FireData").Result;
        }

        [TestFixtureTearDown]
        public void disposeAtEndofTest()
        {
            
        }


        [Test]
        public void validateOutputCacheUntillToday()
        {
            TimeSpan? maxAge = UrlResponse.Headers.CacheControl.MaxAge;

            TimeSpan t = new TimeSpan(23, 55, 59);
            TimeSpan diff = t - maxAge.Value;

            // Cache for Web API
            Assert.IsTrue(maxAge.HasValue);
            Assert.AreEqual(diff.Hours, DateTime.Now.Hour);
            Assert.IsFalse(UrlResponse.Headers.CacheControl.MustRevalidate);
        }

        [Test]
        public void GetAllFireDataViaMoqServiceWithValue()
        {
            IHttpActionResult actionResult = _fireBanCtlMoq.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<Dto.FireBanResult>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.Content.success, true);
            Assert.AreEqual(contentResult.Content.fireBanData.Count, 2);
            Assert.AreEqual(contentResult.Content.message, "Success");
        }

        [Test]
        public void GetEmptyFireDataViaMoqedService()
        {
            IHttpActionResult actionResult = _fireBanCtlwithEmptyServ.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<Dto.FireBanResult>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.Content.success, false);
            Assert.GreaterOrEqual(contentResult.Content.fireBanData.Count, 0);
            Assert.AreEqual(contentResult.Content.message, "FireBan data is Empty");
        }

        [Test]
        public void GetAllFireDataViaService()
        {
            IHttpActionResult actionResult = _fireBanCtl.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<Dto.FireBanResult>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.Content.success, true);
            Assert.GreaterOrEqual(contentResult.Content.fireBanData.Count, 0);
            Assert.AreEqual(contentResult.Content.message, "Success");
        }

    }
}
