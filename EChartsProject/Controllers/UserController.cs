using EChartsProject.Common;
using EChartsProject.Models;
using EChartsProject.Services;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace EChartsProject.Controllers
{
    public class UserController : Controller
    {
        private readonly UserInfoService _userInfoService;

        public UserController(UserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Get
        public ActionResult UserReport()
        {
            return View();
        }

        public ActionResult UserColumnReport()
        {
            return View();
        }

        public ActionResult UserLineReport()
        {
            return View();
        } 
        #endregion

        #region Post
        [HttpPost]
        public UserReportModel GetUserReport()
        {
            return _userInfoService.GetUserReport();
        }

        [HttpPost]
        public UserColumnReportModel GetUserColumnReport()
        {
            return _userInfoService.GetUserColumnReport();
        }

        [HttpPost]
        public UserColumnReportModel GetUserLineReport()
        {
            return _userInfoService.GetUserLineReport();
        } 
        #endregion
    }
}
