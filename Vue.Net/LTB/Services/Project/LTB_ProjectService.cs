/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下LTB_ProjectService与ILTB_ProjectService中编写
 */
using LTB.IRepositories;
using LTB.IServices;
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;
using VOL.Entity.DomainModels.OutPut;

namespace LTB.Services
{
    public partial class LTB_ProjectService : ServiceBase<LTB_Project, ILTB_ProjectRepository>
    ,ILTB_ProjectService, IDependency
    {
    public LTB_ProjectService(ILTB_ProjectRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static ILTB_ProjectService Instance
    {
      get { return AutofacContainerModule.GetService<ILTB_ProjectService>(); } }

        public Summary getSummary(string Id)
        {
            string msg = string.Empty;
            try
            {
                Summary summary = new Summary();
                string projectSql = $"SELECT * FROM [dbo].[LTB_Project] where 1=1";

                if (!string.IsNullOrEmpty(Id))
                {
                    projectSql += $" and ID = '{Id}'";
                }

                List<LTB_Project> projectList = repository.DapperContext.QueryList<LTB_Project>(projectSql, null);

                return getSummaryByprojectID(projectList, summary);
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                return null;
            }
        }


    }
}
