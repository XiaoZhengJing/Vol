/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹LTB_ProjectRepository编写代码
 */
using LTB.IRepositories;
using VOL.Core.BaseProvider;
using VOL.Core.EFDbContext;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;

namespace LTB.Repositories
{
    public partial class LTB_ProjectRepository : RepositoryBase<LTB_Project> , ILTB_ProjectRepository
    {
    public LTB_ProjectRepository(VOLContext dbContext)
    : base(dbContext)
    {

    }
    public static ILTB_ProjectRepository Instance
    {
      get {  return AutofacContainerModule.GetService<ILTB_ProjectRepository>(); } }
    }
}
