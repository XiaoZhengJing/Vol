/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹LTB_ProjectController编写
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.Controllers.Basic;
using VOL.Entity.AttributeManager;
using LTB.IServices;
namespace LTB.Controllers
{
    [Route("api/LTB_Project")]
    [PermissionTable(Name = "LTB_Project")]
    public partial class LTB_ProjectController : ApiBaseController<ILTB_ProjectService>
    {
        public LTB_ProjectController(ILTB_ProjectService service)
        : base(service)
        {
        }
    }
}

