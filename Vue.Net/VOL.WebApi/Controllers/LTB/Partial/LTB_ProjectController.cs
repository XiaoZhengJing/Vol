/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("LTB_Project",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using VOL.Entity.DomainModels;
using LTB.IServices;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using VOL.Entity.DomainModels.OutPut;
using VOL.Core.Configuration;
using System.ComponentModel.DataAnnotations;

namespace LTB.Controllers
{
    public partial class LTB_ProjectController
    {
        private readonly ILTB_ProjectService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;

        [ActivatorUtilitiesConstructor]
        public LTB_ProjectController(
            ILTB_ProjectService service,
            IHttpContextAccessor httpContextAccessor
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        //Project
        [HttpPost, Route("getProject")]

        public IActionResult GetProject([FromQuery] string pdn)
        {
            return Json(Service.GetProject(pdn));
        }

        [HttpPost, Route("addOrUpdateProject")]
        public IActionResult AddOrUpdateProject([FromQuery] string projectDataInfo, string type)
        {
            return Json(Service.AddOrUpdateProject(projectDataInfo, type));
        }

        [HttpPost, Route("deleteProject")]
        public IActionResult DeleteProject([FromQuery] string pIds)
        {
            return Json(Service.DeleteProject(pIds));
        }
        [HttpPost, Route("changeProjectState")]
        public IActionResult ChangeProjectState(int ProjectID, string State)
        {
            return Json(Service.ChangeProjectState(ProjectID, State));
        }
        [HttpPost, Route("ssGetProject")]
        public IActionResult SSGetProject(string spn, int ProjectID)
        {
            return Json(Service.SSGetProject(spn, ProjectID));
        }
        [HttpPost, Route("copyDemands")]
        public IActionResult CopyDemands([FromQuery] string projectDataInfo)
        {
            return Json(Service.CopyDemands(projectDataInfo));
        }

        //下拉绑定
        [HttpPost, Route("getPlant")]
        public IActionResult GetPlant()
        {
            return Json(Service.GetPlant());
        }
        [HttpPost, Route("getSendingPlant")]
        public IActionResult GetSendingPlant()
        {
            return Json(Service.GetSendingPlant());
        }
        [HttpPost, Route("getCounterType")]
        public IActionResult GetCounterType()
        {
            return Json(Service.GetCounterType());
        }
        [HttpPost, Route("getProjectName")]
        public IActionResult GetProjectName()
        {
            return Json(Service.GetProjectName());
        }
        [HttpPost, Route("getEmployee")]
        public IActionResult GetEmployee()
        {
            return Json(Service.GetEmployee());
        }
        [HttpPost, Route("getProjectNameDropdown")]
        public IActionResult GetProjectNameDropdown()
        {
            return Json(Service.GetProjectNameDropdown());
        }


        //Series
        [HttpPost, Route("getSeriesCommon")]
        public IActionResult GetSeriesCommon([FromQuery] string spn, string ProjectID, string searchMaterial)
        {
            return Json(Service.GetSeriesCommon(spn, ProjectID, searchMaterial));
        }
        [HttpPost, Route("addOrUpdateSeriesCommon")]
        public IActionResult AddOrUpdateSeriesCommon([FromQuery] string seriesInfo) //([FromBody] LTB_SeriesDemands seriesCommonInfo)
        {
            return Json(Service.UpdateSeriesCommon(seriesInfo));
        }
        [HttpPost, Route("addSeriesCommon")]
        public IActionResult AddSeriesCommon(int ProjectID)
        {
            return Json(Service.AddSeriesCommon(ProjectID));
        }
        [HttpPost, Route("deleteSeriesCommon")]
        public IActionResult DeleteSeriesCommon([FromQuery] int ID, int ProjectID)
        {
            return Json(Service.DeleteSeriesCommon(ID, ProjectID));
        }
        [HttpPost, Route("dialogAddMaterial")]
        public IActionResult DialogAddMaterial(string MaterialTextarea, string ProjectID)
        {
            return Json(Service.DialogAddMaterial(MaterialTextarea, ProjectID));
        }




        //Service
        [HttpPost, Route("getServiceCommon")]
        public IActionResult GetServiceCommon([FromQuery] string spn, string ProjectID, string searchMaterial)
        {
            return Json(Service.GetServiceCommon(spn, ProjectID, searchMaterial));
        }
        [HttpPost, Route("addServiceCommon")]
        public IActionResult AddServiceCommon([FromQuery] int ProjectID)
        {
            return Json(Service.AddServiceCommon(ProjectID));
        }
        [HttpPost, Route("updateServiceCommon")]
        public IActionResult UpdateServiceCommon([FromQuery] string serviceInfo) 
        {
            return Json(Service.UpdateServiceCommon(serviceInfo));
        }
        [HttpPost, Route("deleteServiceCommon")]
        public IActionResult DeleteServiceCommon([FromQuery] int ID, int ProjectID)
        {
            return Json(Service.DeleteServiceCommon(ID, ProjectID));
        }
        [HttpPost, Route("dialogAddServiceMaterial")]
        public IActionResult DialogAddServiceMaterial(string MaterialTextarea, string ProjectID)
        {
            return Json(Service.DialogAddServiceMaterial(MaterialTextarea, ProjectID));
        }



        //导出PDF
        [HttpPost, Route("GeneratePDF")]
        [AllowAnonymous]
        public string GeneratePDF([FromQuery][Required] string Id)
        {
            var excelFilePath = AppSetting.GetSection("FilePath")["ExcelFilePath"];
            var noRenoveWatermarkPdfFilePath = AppSetting.GetSection("FilePath")["NoRenoveWatermarkPdfFilePath"];
            var summary = Service.getSummary(Id);
            //Git
            //var renoveWatermarkPdfFilePath = AppSetting.CurrentPath.Split("LTBCode")[0] + "git/Vol.Vue3版本/public/" + summary.Project.SPN + "_"
            //   + summary.Project.StockUpStartdate + ".pdf";

            //LTB
            var renoveWatermarkPdfFilePath = AppSetting.CurrentPath.Split("Vue.Net")[0] + "Vol.Vue3版本/public/" + summary.Project.SPN + "_" + summary.Project.StockUpStartdate + ".pdf";

            if (summary.Project.PDN == null)
                return false + "_" + renoveWatermarkPdfFilePath.Split("public")[1];

            return SummayMethods.TeamplateFillDataToPDF(summary, excelFilePath, noRenoveWatermarkPdfFilePath, renoveWatermarkPdfFilePath);

        }

    }
}
