/*
 *所有关于LTB_Project类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*LTB_ProjectService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using VOL.Core.BaseProvider;
using VOL.Core.Extensions.AutofacManager;
using VOL.Entity.DomainModels;
using System.Linq;
using VOL.Core.Utilities;
using System.Linq.Expressions;
using VOL.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using LTB.IRepositories;
using VOL.Core.Services;
using VOL.Core.Enums;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using VOL.Entity.DomainModels.OutPut;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.RegularExpressions;
using Azure;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using static Azure.Core.HttpHeader;

namespace LTB.Services
{
    public partial class LTB_ProjectService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILTB_ProjectRepository _repository;//访问数据库
        WebResponseContent responseContent = new WebResponseContent();

        [ActivatorUtilitiesConstructor]
        public LTB_ProjectService(
            ILTB_ProjectRepository dbRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        /// <summary>
        /// WebApi通过Windows登陆
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public List<LTB_Project> GetProject(string pdn) // LTB_Project projectInfo
        {
            string msg = string.Empty;
            try
            {
                string sql = $"SELECT * FROM [dbo].[LTB_Project] where 1=1";
                if (!string.IsNullOrEmpty(pdn))
                {
                    //sql += $" and (PDN='{pdn}' or SPN='{pdn}')";
                    sql += $" and (PDN like '%{pdn}%' or SPN like '%{pdn}%')";
                }
                sql += " order by spn,pdn,StockUpStartdate ";

                List<LTB_Project> list = repository.DapperContext.QueryList<LTB_Project>(sql, null);

                return list;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return null;
            }
        }

        public int AddOrUpdateProject([FromQuery] string projectDataInfo,string type)
        {
            string msg = string.Empty;
            try
            {
                LTB_Project projectInfo = JsonConvert.DeserializeObject<LTB_Project>(projectDataInfo);
                string spn = projectInfo.SPN; //用来查询有否存在该project
                DateTime stockupStartdate = (DateTime)projectInfo.StockUpStartdate;
                string createder = projectInfo.Createder;
                string app1 = projectInfo.Approvaler1;
                string app2 = projectInfo.Approvaler2;
                string app3 = projectInfo.Approvaler3;
                int isOk = 0;

                //后期会有一个SPN继续追加单子 所以用SPN和StockUpStartdate一起作为唯一标识
                string getSql = $"SELECT * FROM [dbo].[LTB_Project] where  ID='{projectInfo.ID}' "; //SPN='{spn}' AND StockUpStartdate='{stockupStartdate}' and
                List<LTB_Project> list = repository.DapperContext.QueryList<LTB_Project>(getSql, null);

                //获取Department
                string sql = $"select department,email from [dbo].[employee] where displayname like '{createder}%'";
                List<Employee> employeeList = repository.DapperContext.QueryList<Employee>(sql, null);
                string sql1 = $"select department,email from [dbo].[employee] where displayname like '{app1}%'";
                List<Employee> appList1 = repository.DapperContext.QueryList<Employee>(sql1, null);
                string sql2 = $"select department,email from [dbo].[employee] where displayname like '{app2}%'";
                List<Employee> appList2 = repository.DapperContext.QueryList<Employee>(sql2, null);
                string sql3 = $"select department,email from [dbo].[employee] where displayname like '{app3}%'";
                List<Employee> appList3 = repository.DapperContext.QueryList<Employee>(sql3, null);

                string createderDepartment = "", approvalerDepartment1 = "", approvalerDepartment2 = "", approvalerDepartment3 = "";
                if (employeeList.Count != 0 && !string.IsNullOrEmpty(createder))
                {
                    createderDepartment = employeeList.FirstOrDefault().Department.ToString();
                }
                if (appList1.Count != 0 && !string.IsNullOrEmpty(app1))
                {
                    approvalerDepartment1 = appList1.FirstOrDefault().Department.ToString();
                }
                if (appList2.Count != 0 && !string.IsNullOrEmpty(app2))
                {
                    approvalerDepartment2 = appList2.FirstOrDefault().Department.ToString();
                }
                if (appList3.Count != 0 && !string.IsNullOrEmpty(app3))
                {
                    approvalerDepartment3 = appList3.FirstOrDefault().Department.ToString();
                }

                //单价Price可能是,形式  如：6.6 或 6,6 ;导入数据时才考虑这一步
                if (projectInfo.Price == null)
                {
                    projectInfo.Price = (decimal?)0.00;
                }
                if (projectInfo.TotalSeriesDemand == null)
                {
                    projectInfo.TotalSeriesDemand = (decimal?)0.00;
                }
                if (projectInfo.TotalSeriesCosts == null)
                {
                    projectInfo.TotalSeriesCosts = (decimal?)0.00;
                }
                if (projectInfo.TotalServiceDemand == null)
                {
                    projectInfo.TotalServiceDemand = (decimal?)0.00;
                }
                if (projectInfo.TotalServiceCosts == null)
                {
                    projectInfo.TotalServiceCosts = (decimal?)0.00;
                }

                if (type.Contains("Update"))  //(list.Count != 0)
                {
                    //进行update
                    string updateSql = $"update [dbo].[LTB_Project] set PDN='{projectInfo.PDN}',SPN='{projectInfo.SPN}', BL='{projectInfo.BL}',ObsoleteMaterialDescription='{projectInfo.ObsoleteMaterialDescription}',AffectedBLs='{projectInfo.AffectedBLs}',Manufacturer='{projectInfo.Manufacturer}',StockUpStartdate='{projectInfo.StockUpStartdate}',Price='{projectInfo.Price}',PriceTypeID='{projectInfo.PriceTypeID}',CreatederDepartment='{createderDepartment}',Createder='{projectInfo.Createder}',CreateDate='{projectInfo.CreateDate}',Approvaler1='{projectInfo.Approvaler1}',ApprovalerDepartment1='{approvalerDepartment1}',ApprovalDate1='{projectInfo.ApprovalDate1}',Approvaler2='{projectInfo.Approvaler2}',ApprovalerDepartment2='{approvalerDepartment2}',ApprovalDate2='{projectInfo.ApprovalDate2}',Approvaler3='{projectInfo.Approvaler3}',ApprovalerDepartment3='{approvalerDepartment3}',ApprovalDate3='{projectInfo.ApprovalDate3}' where ID='{projectInfo.ID}' "; // SPN='{projectInfo.SPN}'  AND StockUpStartdate='{projectInfo.StockUpStartdate}'  
                    isOk = repository.DapperContext.ExcuteNonQuery(updateSql, null);
                }
                else
                {
                    //进行add 或者 copy
                    string insertSql = "insert into [dbo].[LTB_Project] (PDN,SPN,BL,ObsoleteMaterialDescription,AffectedBLs,Manufacturer,StockUpStartdate,Price,PriceTypeID,TotalSeriesDemand,TotalSeriesCosts,TotalServiceDemand,TotalServiceCosts,CreatederDepartment,Createder,CreateDate,Approvaler1,ApprovalerDepartment1,ApprovalDate1,Approvaler2,ApprovalerDepartment2,ApprovalDate2,Approvaler3,ApprovalerDepartment3,ApprovalDate3,State) ";
                    insertSql += $" values ('{projectInfo.PDN}','{projectInfo.SPN}','{projectInfo.BL}','{projectInfo.ObsoleteMaterialDescription}','{projectInfo.AffectedBLs}','{projectInfo.Manufacturer}','{projectInfo.StockUpStartdate}','{projectInfo.Price}','{projectInfo.PriceTypeID}','{projectInfo.TotalSeriesDemand}','{projectInfo.TotalSeriesCosts}','{projectInfo.TotalServiceDemand}','{projectInfo.TotalServiceCosts}','{createderDepartment}','{projectInfo.Createder}','{projectInfo.CreateDate}','{projectInfo.Approvaler1}','{approvalerDepartment1}','{projectInfo.ApprovalDate1}','{projectInfo.Approvaler2}','{approvalerDepartment2}','{projectInfo.ApprovalDate2}','{projectInfo.Approvaler3}','{approvalerDepartment3}','{projectInfo.ApprovalDate3}','New')";
                    isOk = repository.DapperContext.ExcuteNonQuery(insertSql, null);
                }

                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }

        /// <summary>
        /// copy 某一project的 series & service demand
        /// </summary>
        /// <param name="selectedproID">数据源（从这里来）</param>
        /// <returns></returns>
        public int CopyDemands([FromQuery] string projectDataInfo)  // string projectId
        {
            string msg = string.Empty;
            int isOk = 0;
            try
            {
                LTB_Project projectInfo = JsonConvert.DeserializeObject<LTB_Project>(projectDataInfo);
                decimal seriesTotalDemand = 0, serviceTotalDemand = 0;
                int projectId = 0; //添加到这里去

                string getProjectTotal =$" select * from [dbo].[LTB_Project] where ID='{projectInfo.ID}'";
                List<LTB_Project> projectTotalList = repository.DapperContext.QueryList<LTB_Project>(getProjectTotal, null);
                decimal totalSeriesDemand = 0, totalSeriesCosts = 0, totalServiceDemand = 0, totalServiceCosts = 0;
                if (projectTotalList.Count ==1)
                {
                    totalSeriesDemand = (decimal)projectTotalList[0].TotalSeriesDemand;
                    totalSeriesCosts = (decimal)projectTotalList[0].TotalSeriesCosts;
                    totalServiceDemand = (decimal)projectTotalList[0].TotalServiceDemand;
                    totalServiceCosts = (decimal)projectTotalList[0].TotalServiceCosts;
                }
                string isSeriesData = $" select * from [dbo].[LTB_SeriesDemands] where ProjectID='{projectInfo.ID}' ";
                List<LTB_SeriesDemands> seriesList = repository.DapperContext.QueryList<LTB_SeriesDemands>(isSeriesData, null);
                string isServiceData = $" select * from [dbo].[LTB_ServiceDemands] where ProjectID='{projectInfo.ID}' ";
                List<LTB_ServiceDemands> serviceList = repository.DapperContext.QueryList<LTB_ServiceDemands>(isServiceData, null);

                //1：先copy 最外面的 project 信息
                if (projectInfo!=null)
                {
                    string insertSql = "insert into [dbo].[LTB_Project] (PDN,SPN,BL,ObsoleteMaterialDescription,AffectedBLs,Manufacturer,StockUpStartdate,Price,PriceTypeID,TotalSeriesDemand,TotalSeriesCosts,TotalServiceDemand,TotalServiceCosts,CreatederDepartment,Createder,CreateDate,Approvaler1,ApprovalerDepartment1,ApprovalDate1,Approvaler2,ApprovalerDepartment2,ApprovalDate2,Approvaler3,ApprovalerDepartment3,ApprovalDate3,State) ";
                    insertSql += $" values ('{projectInfo.PDN}','{projectInfo.SPN}','{projectInfo.BL}','{projectInfo.ObsoleteMaterialDescription}','{projectInfo.AffectedBLs}','{projectInfo.Manufacturer}','{projectInfo.StockUpStartdate}','{projectInfo.Price}','{projectInfo.PriceTypeID}','{totalSeriesDemand}','{totalSeriesCosts}','{totalServiceDemand}','{totalServiceCosts}','{projectInfo.CreatederDepartment}','{projectInfo.Createder}','{projectInfo.CreateDate}','{projectInfo.Approvaler1}','{projectInfo.ApprovalerDepartment1}','{projectInfo.ApprovalDate1}','{projectInfo.Approvaler2}','{projectInfo.ApprovalerDepartment2}','{projectInfo.ApprovalDate2}','{projectInfo.Approvaler3}','{projectInfo.ApprovalerDepartment3}','{projectInfo.ApprovalDate3}','New')";

                    bool add = repository.DapperContext.ExcuteNonQuery(insertSql, null) == 1 ? true : false;
                    if (add)
                    {
                        //添加成功，则获取该条的projectId，下面的series和service的信息都要添加进这里
                        string getProjectId = $"select top 1 * from [dbo].[LTB_Project] where StockUpStartdate='{projectInfo.StockUpStartdate}' order by id desc";
                        List<LTB_Project> getProjectIdList = repository.DapperContext.QueryList<LTB_Project>(getProjectId, null);
                        projectId = getProjectIdList[0].ID;
                    }
                }

                if (seriesList.Count == 0 && serviceList.Count == 0)
                {
                    return -1;
                }
                else
                {
                    //添加Series demand
                    if (seriesList.Count > 0 && projectId != 0)
                    {
                        foreach (var series in seriesList)
                        {
                            string insertSeriesSql = " insert into [dbo].[LTB_SeriesDemands] (ProjectID,CounterMaterial,CounterMaterialDescription,PlantID,SendingPlantID,Customer,CounterTypeID,CoverageType,OnStockFrom,OnStockUntil,ProjectName,ProjectNames,PlanRestOfCurrentFY,PlanFirstFollowingFY,PlanSecondFollowingFY,PlanThirdFollowingFY,SumPlan,ComponentQuantity,TotalDemand) ";  
                            insertSeriesSql += $" values ('{projectId}','{series.CounterMaterial}','{series.CounterMaterialDescription}','{series.PlantID}','{series.SendingPlantID}','{series.Customer}','{series.CounterTypeID}','{series.CoverageType}','{series.OnStockFrom}','{series.OnStockUntil}','{series.ProjectName}','{series.ProjectNames}','{series.PlanRestOfCurrentFY}','{series.PlanFirstFollowingFY}','{series.PlanSecondFollowingFY}','{series.PlanThirdFollowingFY}','{series.SumPlan}','0','0') ";
                            isOk += repository.DapperContext.ExcuteNonQuery(insertSeriesSql, null);
                            seriesTotalDemand += (decimal)series.TotalDemand;
                        }
                    }
                    //添加Service demand
                    if (serviceList.Count > 0 && projectId != 0)
                    {
                        foreach (var service in serviceList)
                        {
                            string insertServiceSql = " insert into [dbo].[LTB_ServiceDemands] (ProjectID,CounterMaterial,CounterMaterialDescription,PlantID,SendingPlantID,Customer,CounterTypeID,CoverageType,OnStockFrom,OnStockUntil,ProjectName,ProjectNames,RepairPart,RepairsUntil_EOSOrESD,EndOfService_EOSOrESD,SumServiceDemand,SumPercentage,ComponentQuantity,TotalDemand) ";
                            insertServiceSql += $" values ('{projectId}','{service.CounterMaterial}','{service.CounterMaterialDescription}','{service.PlantID}','{service.SendingPlantID}','{service.Customer}','{service.CounterTypeID}','{service.CoverageType}','{service.OnStockFrom}','{service.OnStockUntil}','{service.ProjectName}','{service.ProjectNames}','{service.RepairPart}','{service.RepairsUntil_EOSOrESD}','{service.EndOfService_EOSOrESD}','{service.SumServiceDemand}','{service.SumPercentage}','0','0') ";
                            isOk += repository.DapperContext.ExcuteNonQuery(insertServiceSql, null);
                            serviceTotalDemand += (decimal)service.TotalDemand;
                        }
                    }

                    //update project（到这里去）的TotalDemand等值, 由于两边单价可能不一样，所以需要重新计算 ===> component quantity=0 ===> 所有值都为0，不需要下面的计算了
                    //string getProject = $"SELECT * FROM [dbo].[LTB_Project] where ID='{projectId}'";
                    //List<LTB_Project> projectDataList = repository.DapperContext.QueryList<LTB_Project>(getProject, null);
                    //decimal price = (decimal)projectDataList[0].Price;
                    //decimal totalSeriesDemand = seriesTotalDemand;
                    //decimal totalSeriesCosts = seriesTotalDemand * price;
                    //decimal totalServiceDemand = serviceTotalDemand;
                    //decimal totalServiceCosts = serviceTotalDemand * price;

                    //string updateSql = $"update [dbo].[LTB_Project] set TotalSeriesDemand='{totalSeriesDemand}',TotalSeriesCosts='{totalSeriesCosts}',TotalServiceDemand='{totalServiceDemand}',TotalServiceCosts='{totalServiceCosts}' where ID='{projectId}' ";
                    //isOk += repository.DapperContext.ExcuteNonQuery(updateSql, null);

                    return isOk;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }
        public int DeleteProject(string pIds)
        {
            string msg = string.Empty;
            //string pdns = JsonConvert.DeserializeObject<string>(pdn);
            string[] delPIds = JsonConvert.DeserializeObject<string[]>(pIds); // pdns.Split(',');
            int isOk = 0;
            try
            {
                foreach (var delpid in delPIds)
                {
                    string sql = $"delete [dbo].[LTB_Project] where ID='{delpid}'";
                    isOk += repository.DapperContext.ExcuteNonQuery(sql, null);
                }
                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }
        public string ChangeProjectState(int ProjectID, string State)
        {
            string msg = string.Empty;
            try
            {
                //四个状态：New , Series Done , Service Done ,  All Done  
                int isOk = 0;
                string successInfo = "";
                string sql = $"SELECT * FROM [dbo].[LTB_Project] where ID='{ProjectID}'";
                List<LTB_Project> list = repository.DapperContext.QueryList<LTB_Project>(sql, null);
                var proState = list[0].State;
                if (proState== "New")
                {
                    string updateState = $"update [dbo].[LTB_Project] set State='{State}' where ID='{ProjectID}'";
                    isOk = repository.DapperContext.ExcuteNonQuery(updateState, null);
                    successInfo = State;
                }
                else
                {
                    if (proState != State && proState != "All Done")
                    {
                        string updateState = $"update [dbo].[LTB_Project] set State='All Done' where ID='{ProjectID}'";
                        isOk = repository.DapperContext.ExcuteNonQuery(updateState, null);
                        successInfo = "All Done, You can export the PDF now";
                    }
                    else if (proState == "Series Done" && State == "Series Done")
                    {
                        successInfo = "Series completed";
                    }
                    else if (proState == "Service Done" && State == "Service Done")
                    {
                        successInfo = "Service completed";
                    }
                    else if (proState == "All Done")
                    {
                        successInfo = "It's all done now";
                    }
                }
                return successInfo;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return msg;
            }
        }


        //下拉绑定
        public List<LTB_Plant> GetPlant()
        {
            string sql = "SELECT * FROM [dbo].[LTB_Plant]";
            List<LTB_Plant> list = repository.DapperContext.QueryList<LTB_Plant>(sql, null);
            return list;
        }
        public List<LTB_SendingPlant> GetSendingPlant()
        {
            string sql = "SELECT * FROM [dbo].[LTB_SendingPlant]";
            List<LTB_SendingPlant> list = repository.DapperContext.QueryList<LTB_SendingPlant>(sql, null);
            return list;
        }
        public List<LTB_CounterType> GetCounterType()
        {
            string sql = "SELECT * FROM [dbo].[LTB_CounterType]";
            List<LTB_CounterType> list = repository.DapperContext.QueryList<LTB_CounterType>(sql, null);
            return list;
        }
        public List<ProjectNameOption> GetProjectName()
        {
            string productGroupSql = "select distinct [ProductGroup] as label  FROM [dbo].[LTB_ProjectNameDropdown]";
            List<ProjectNameOption> productGroupList = repository.DapperContext.QueryList<ProjectNameOption>(productGroupSql, null);
            int index = 100; //无论父子 value不能有重复的

            foreach (ProjectNameOption option in productGroupList)
            {
                string sql = $"SELECT distinct rowID as value,[ProjectName] as label FROM [dbo].[LTB_ProjectNameDropdown] where [ProductGroup]='{option.label}'";
                List<ProjectNameOptionChildren> list = repository.DapperContext.QueryList<ProjectNameOptionChildren>(sql, null);
                option.children = list;
                index++;
                option.value = index;
            }

            return productGroupList;
        }
        public List<SearchEmployee> GetEmployee()
        {
            string sql = " select LastName+', '+FirstName as value,DisplayName,Department  from [dbo].[employee]  where DisplayName not like '%$%' ";
            List<SearchEmployee> list = repository.DapperContext.QueryList<SearchEmployee>(sql, null);
            return list;
        }
        public List<ProjectNameDropdown> GetProjectNameDropdown()
        {
            //string sql = "select distinct DENSE_RANK()over(order by ProjectName) as id,ProjectName from LTB_ProjectNameDropdown";
            string sql = "select distinct rowID,ProjectName from LTB_ProjectNameDropdown";
            List<ProjectNameDropdown> list = repository.DapperContext.QueryList<ProjectNameDropdown>(sql, null);
            return list;
        }

        public List<LTB_Project> SSGetProject(string spn, int ProjectID) // LTB_Project projectInfo
        {
            string msg = string.Empty;
            try
            {
                string sql = $"SELECT * FROM [dbo].[LTB_Project] where 1=1";
                if (!string.IsNullOrEmpty(spn))
                {
                    sql += $" and SPN = '{spn}' ";
                }
                if (!string.IsNullOrEmpty(ProjectID.ToString()))
                {
                    sql += $" and ID = '{ProjectID}' ";
                }

                List<LTB_Project> list = repository.DapperContext.QueryList<LTB_Project>(sql, null);

                return list;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return null;
            }
        }

        //Series
        public List<LTB_SeriesDemands> GetSeriesCommon(string spn, string ProjectID,string searchMaterial)
        {
            string msg = string.Empty;
            try
            {
                string sql = $" select s.*,pl.Plant,sp.SendingPlant,c.CounterType from [dbo].[LTB_Project] p left join [dbo].[LTB_SeriesDemands] s on p.ID=s.ProjectID left join [dbo].[LTB_Plant] pl on pl.ID=s.PlantID left join [dbo].[LTB_SendingPlant] sp on sp.ID=s.SendingPlantID  left join [dbo].[LTB_CounterType] c on c.ID=s.CounterTypeID where 1=1  and s.ID is not null ";
                if (!string.IsNullOrEmpty(spn))
                {
                    sql += $" and SPN='{spn}' ";
                }
                if (!string.IsNullOrEmpty(ProjectID))
                {
                    sql += $" and ProjectID='{ProjectID}' ";
                }
                if (!string.IsNullOrEmpty(searchMaterial))
                {
                    sql += $" and CounterMaterial like '%{searchMaterial}%' ";
                }

                List<LTB_SeriesDemands> list = repository.DapperContext.QueryList<LTB_SeriesDemands>(sql, null);
                if (list.Count != 0)
                {
                    foreach (var series in list)
                    {
                        string projectNameID = series.ProjectName;
                        if (!string.IsNullOrEmpty(projectNameID))
                        {
                            string[] nameIDs = projectNameID.Split(';');
                            int[] proNameID = Array.ConvertAll<string, int>(nameIDs, Convert.ToInt32); //将string[]转为int[]，用于回显project的多选拉下
                            series.ProjectNameID = proNameID;

                            #region 拼接ProjectName
                            //string names = "";
                            //if (nameIDs.Length != 0)
                            //{
                            //    foreach (var nameID in nameIDs)
                            //    {
                            //        //string getProjectNameSql = $"SELECT * FROM [dbo].[LTB_ProjectName] where ID='{nameID}'"; //级联多选下拉
                            //        //List<LTB_ProjectName> namelist = repository.DapperContext.QueryList<LTB_ProjectName>(getProjectNameSql, null);

                            //        string getProjectNameSql = $"SELECT * FROM [dbo].[LTB_ProjectNameDropdown] where rowID='{nameID}'";  //多选下拉
                            //        List<LTB_ProjectNameDropdown> namelist = repository.DapperContext.QueryList<LTB_ProjectNameDropdown>(getProjectNameSql, null);
                            //        if (namelist.Count != 0)
                            //        {
                            //            string pname = namelist[0].ProjectName.ToString();
                            //            names += pname + ";";
                            //        }

                            //    }
                            //}
                            //names = names.Substring(0, names.Length - 1); //去掉最后一个;
                            ////若同时选中 P81A和P81B 显示成 P81A/B
                            //if (names.Contains("P81A") && names.Contains("P81B"))
                            //{
                            //    names = names.Replace("P81A", "P81A/B").Replace("P81B", "P81A/B");
                            //    //先将names转成数组再去重
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    //再转成字符串
                            //    names = String.Join(";", arrNames);
                            //}
                            ////同上操作 Denali LP1&Denali LP2 ==> Denali LP1/LP2
                            //if (names.Contains("Denali LP1") && names.Contains("Denali LP2"))
                            //{
                            //    names = names.Replace("Denali LP1", "Denali LP1/LP2").Replace("Denali LP2", "Denali LP1/LP2");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            ////同上操作 P60A & P60B ==> P60A/B
                            //if (names.Contains("P60A") && names.Contains("P60B"))
                            //{
                            //    names = names.Replace("P60A", "P60A/B").Replace("P60B", "P60A/B");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// P82A/B & PB2C ==> P82A/B/C
                            //if (names.Contains("P82A/B") && names.Contains("P82C"))
                            //{
                            //    names = names.Replace("P82A/B", "P82A/B/C").Replace("P82C", "P82A/B/C");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// T2;T6;T16 --> T2/6/16
                            //if (names.Contains("T2") && names.Contains("T6") && !names.Contains("T16"))
                            //{
                            //    names = names.Replace("T2", "T2/6").Replace("T6", "T2/6");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("T2") && !names.Contains("T6") && names.Contains("T16"))
                            //{
                            //    names = names.Replace("T2", "T2/16").Replace("T16", "T2/16");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("T2") && names.Contains("T6") && names.Contains("T16"))
                            //{
                            //    names = names.Replace("T6", "T6/16").Replace("T16", "T6/16");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("T2") && names.Contains("T6") && names.Contains("T16"))
                            //{
                            //    names = names.Replace("T2", "T2/6/16").Replace("T6", "T2/6/16").Replace("T16", "T2/6/16");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// P68A; P68B; P68G --> P68A/B/G
                            //if (names.Contains("P68A") && names.Contains("P68B") && !names.Contains("P68G"))
                            //{
                            //    names = names.Replace("P68A", "P68A/B").Replace("P68B", "P68A/B");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P68A") && names.Contains("P68G") && !names.Contains("P68B"))
                            //{
                            //    names = names.Replace("P68A", "P68A/G").Replace("P68G", "P68A/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P68B") && names.Contains("P68G") && !names.Contains("P68A"))
                            //{
                            //    names = names.Replace("P68B", "P68B/G").Replace("P68G", "P68B/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P68A") && names.Contains("P68B") && names.Contains("P68G"))
                            //{
                            //    names = names.Replace("P68A", "P68A/B/G").Replace("P68B", "P68A/B/G").Replace("P68G", "P68A/B/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// P15A; P15P; P15F; P15G --> P15A/P/F/G
                            //if (names.Contains("P15A") && names.Contains("P15P") && names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/P/F/G").Replace("P15P", "P15A/P/F/G").Replace("P15F", "P15A/P/F/G").Replace("P15G", "P15A/P/F/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && names.Contains("P15P") && names.Contains("P15F") && !names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/P/F").Replace("P15P", "P15A/P/F").Replace("P15F", "P15A/P/F");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && names.Contains("P15P") && !names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/P/G").Replace("P15P", "P15A/P/G").Replace("P15G", "P15A/P/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && !names.Contains("P15P") && names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/F/G").Replace("P15F", "P15A/F/G").Replace("P15G", "P15A/F/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("P15A") && names.Contains("P15P") && names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15P", "P15P/F/G").Replace("P15F", "P15P/F/G").Replace("P15G", "P15P/F/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && names.Contains("P15P") && !names.Contains("P15F") && !names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/P").Replace("P15P", "P15A/P");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && !names.Contains("P15P") && names.Contains("P15F") && !names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/F").Replace("P15F", "P15A/F");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && !names.Contains("P15P") && !names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/G").Replace("P15G", "P15A/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("P15A") && names.Contains("P15P") && names.Contains("P15F") && !names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15P", "P15P/F").Replace("P15F", "P15P/F");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("P15A") && names.Contains("P15P") && !names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15P", "P15P/G").Replace("P15G", "P15P/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("P15A") && !names.Contains("P15P") && names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15F", "P15F/G").Replace("P15G", "P15F/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// P15S & P15T --> P15S/T
                            //if (names.Contains("P15S") && names.Contains("P15T"))
                            //{
                            //    names = names.Replace("P15S", "P15S/T").Replace("P15T", "P15S/T");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}

                            //series.ProjectNames = names;
                            #endregion

                        }
                        series.isEdit = true; //用于控制前台table行编辑的开启与关闭
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return null;
            }
        }
        //根据多选下拉计算财年对应的值  FYYearTotal
        public List<FYYearTotal> GetFYYearTotal(string rowID, int FYYear, int form, int until)
        {
            until = until + 1;//若until到6.2或6.29  是包含6月的，所以+1
            string sql = "SELECT SUM([Quantities]) AS Total ,a.FYYear FROM (SELECT  [Quantities] ,LEFT(DATEADD(MONTH, 3, CAST((CAST([year] AS VARCHAR) + '-' + CAST([month] AS VARCHAR) + '-01') AS DATE)), 4) AS FYYear ";
            sql += " FROM shai438a.[SmartMES].[dbo].[MPM_MMP_MaterialMasterPlan_System_Features] WHERE FK_Id = (SELECT TOP 1 id  FROM shai438a.[SmartMES].[dbo].[MPM_MMP_MaterialMasterPlan_HistoryVer] WHERE BusinessLine = 'CT' AND VersionType = 'Publish' ";
            sql +=$" AND FYYear = {FYYear} "; //条件1：当前财年
            sql += " ORDER BY CreateDate DESC) AND Type = N'发运' ";
            sql+= $" and (year*100+MONTH between {form} and {until}) "; //条件3：between 202205 and 202312 --> between OnStockFrom and OnStockUntil
            sql += $" AND Product IN (select SCMProjectName from [dbo].[LTB_ProjectNameDropdown] where rowID in ({rowID}))) a GROUP BY FYYear "; //条件3：SCM Project Name
            List<FYYearTotal> SCMProjectNameList = repository.DapperContext.QueryList<FYYearTotal>(sql, null);

            return SCMProjectNameList;
        }

        public int UpdateSeriesCommon([FromQuery] string seriesInfo)
        {
            string msg = string.Empty;
            try
            {
                LTB_SeriesDemands seriesCommonInfo = JsonConvert.DeserializeObject<LTB_SeriesDemands>(seriesInfo);
                int isOk = 0;
                if (seriesCommonInfo != null)
                {
                    //第一版：级联多选下拉
                    //string updateSeriesSql = $"update [dbo].LTB_SeriesDemands set CounterMaterial='{seriesCommonInfo.CounterMaterial}',CounterMaterialDescription='{seriesCommonInfo.CounterMaterialDescription}',PlantID='{seriesCommonInfo.PlantID}',SendingPlantID='{seriesCommonInfo.SendingPlantID}',Customer='{seriesCommonInfo.Customer}',CounterTypeID='{seriesCommonInfo.CounterTypeID}',CoverageType='{seriesCommonInfo.CoverageType}',OnStockFrom='{seriesCommonInfo.OnStockFrom}',OnStockUntil='{seriesCommonInfo.OnStockUntil}',ProjectName='{seriesCommonInfo.ProjectName}',PlanRestOfCurrentFY='{seriesCommonInfo.PlanRestOfCurrentFY}',PlanFirstFollowingFY='{seriesCommonInfo.PlanFirstFollowingFY}',PlanSecondFollowingFY='{seriesCommonInfo.PlanSecondFollowingFY}',PlanThirdFollowingFY='{seriesCommonInfo.PlanThirdFollowingFY}',SumPlan='{seriesCommonInfo.SumPlan}',ComponentQuantity='{seriesCommonInfo.ComponentQuantity}',TotalDemand='{seriesCommonInfo.TotalDemand}' where ID='{seriesCommonInfo.ID}' and  ProjectID='{seriesCommonInfo.ProjectID}' ";
                    //isOk += repository.DapperContext.ExcuteNonQuery(updateSeriesSql, null);

                    //先查询一下原多选下拉的个数，判断是否有变化   
                    string getProNumber = $"select * from [dbo].[LTB_SeriesDemands] where ID='{seriesCommonInfo.ID}' and  ProjectID='{seriesCommonInfo.ProjectID}' ";
                    List<LTB_SeriesDemands> ProNumberList= repository.DapperContext.QueryList<LTB_SeriesDemands>(getProNumber, null);
                    int oldProNumber = 0, newProNumber = 0;   
                    if (!string.IsNullOrEmpty(ProNumberList[0].ProjectName.ToString()))
                    {
                        oldProNumber = ProNumberList[0].ProjectName.ToString().Split(';').Length;
                    }
                    if (!string.IsNullOrEmpty(seriesCommonInfo.ProjectName.ToString()))
                    {
                        newProNumber = seriesCommonInfo.ProjectName.Split(';').Length;  //seriesCommonInfo.ProjectNameID.Length;
                    }
                    DateTime oldOnStockFrom = Convert.ToDateTime(ProNumberList[0].OnStockFrom);
                    DateTime oldOnStockUntil = Convert.ToDateTime(ProNumberList[0].OnStockUntil);

                    //第二版：多选下拉
                    //string projectName = String.Join(";", seriesCommonInfo.ProjectNameID);
                    //根据多选下拉计算财年对应的值
                    string rowID = String.Join(",", seriesCommonInfo.ProjectName.Split(';')); //seriesCommonInfo.ProjectNameID
                    //根据多选下拉的ID，获取拼接后的值
                    string changedProjectNames = GetchangedProjectNames(seriesCommonInfo.ProjectName);
                    //当前财年
                    var dt = DateTime.Now;
                    var nowYear = dt.Year;
                    var nowMonth = dt.Month;
                    int FYYear = 0;
                    if (nowMonth < 10)
                    {
                        FYYear = nowYear - 2000;
                    }
                    else if(nowMonth >= 10)
                    {
                        FYYear = nowYear - 2000 + 1;
                    }
                    string fromMonth = "", untilMonth = "";
                    if (Convert.ToDateTime(seriesCommonInfo.OnStockFrom).Month<10)
                    {
                        fromMonth = "0" + Convert.ToDateTime(seriesCommonInfo.OnStockFrom).Month.ToString();
                    }
                    else
                    {
                        fromMonth = Convert.ToDateTime(seriesCommonInfo.OnStockFrom).Month.ToString();
                    }
                    if (Convert.ToDateTime(seriesCommonInfo.OnStockUntil).Month < 10)
                    {
                        untilMonth = "0" + Convert.ToDateTime(seriesCommonInfo.OnStockUntil).Month.ToString();
                    }
                    else
                    {
                        untilMonth = Convert.ToDateTime(seriesCommonInfo.OnStockUntil).Month.ToString();
                    }
                    var OnStockFrom = Convert.ToDateTime(seriesCommonInfo.OnStockFrom).Year.ToString()+ fromMonth;
                    var OnStockUntil = Convert.ToDateTime(seriesCommonInfo.OnStockUntil).Year.ToString() + untilMonth;
                    int form = Convert.ToInt32(OnStockFrom);
                    int until = Convert.ToInt32(OnStockUntil);
                    List<FYYearTotal> FYYearTotalList = GetFYYearTotal(rowID, FYYear, form, until);

                    //判断rest的值究竟是用下拉计算出来的  还是用手填的   
                    decimal rest = 0, frist = 0;
                    //若多选下拉数量有变化，用计算出来的，否则用手填的    时间变化也用计算的
                    if (oldProNumber != newProNumber || oldOnStockFrom!= seriesCommonInfo.OnStockFrom || oldOnStockUntil!= seriesCommonInfo.OnStockUntil)
                    {
                        rest = FYYearTotalList.Count >= 1 ? FYYearTotalList[0].Total : 0;
                        //frist = FYYearTotalList[1].Total;
                        frist = FYYearTotalList.Count >= 2 ? FYYearTotalList[1].Total : 0;
                    }
                    else
                    {
                        rest = (decimal)seriesCommonInfo.PlanRestOfCurrentFY;
                        frist = (decimal)seriesCommonInfo.PlanFirstFollowingFY;
                    }

                    //计算sumPlan和totalDemand的值
                    //decimal rest = FYYearTotalList[0].Total;
                    //decimal frist = FYYearTotalList[1].Total;
                    decimal second = (decimal)seriesCommonInfo.PlanSecondFollowingFY;
                    decimal third = (decimal)seriesCommonInfo.PlanThirdFollowingFY;
                    decimal sumPlan = rest + frist + second + third;
                    decimal totalDemand = sumPlan * (decimal)seriesCommonInfo.ComponentQuantity;

                    //string updateSeriesSql = $"update [dbo].LTB_SeriesDemands set CounterMaterial='{seriesCommonInfo.CounterMaterial}',CounterMaterialDescription='{seriesCommonInfo.CounterMaterialDescription}',PlantID='{seriesCommonInfo.PlantID}',SendingPlantID='{seriesCommonInfo.SendingPlantID}',Customer='{seriesCommonInfo.Customer}',CounterTypeID='{seriesCommonInfo.CounterTypeID}',CoverageType='{seriesCommonInfo.CoverageType}',OnStockFrom='{seriesCommonInfo.OnStockFrom}',OnStockUntil='{seriesCommonInfo.OnStockUntil}',ProjectName='{projectName}',PlanRestOfCurrentFY='{seriesCommonInfo.PlanRestOfCurrentFY}',PlanFirstFollowingFY='{seriesCommonInfo.PlanFirstFollowingFY}',PlanSecondFollowingFY='{seriesCommonInfo.PlanSecondFollowingFY}',PlanThirdFollowingFY='{seriesCommonInfo.PlanThirdFollowingFY}',SumPlan='{seriesCommonInfo.SumPlan}',ComponentQuantity='{seriesCommonInfo.ComponentQuantity}',TotalDemand='{seriesCommonInfo.TotalDemand}' where ID='{seriesCommonInfo.ID}' and  ProjectID='{seriesCommonInfo.ProjectID}' ";
                    string updateSeriesSql = $"update [dbo].LTB_SeriesDemands set CounterMaterial='{seriesCommonInfo.CounterMaterial}',CounterMaterialDescription='{seriesCommonInfo.CounterMaterialDescription}',PlantID='{seriesCommonInfo.PlantID}',SendingPlantID='{seriesCommonInfo.SendingPlantID}',Customer='{seriesCommonInfo.Customer}',CounterTypeID='{seriesCommonInfo.CounterTypeID}',CoverageType='{seriesCommonInfo.CoverageType}',OnStockFrom='{seriesCommonInfo.OnStockFrom}',OnStockUntil='{seriesCommonInfo.OnStockUntil}',ProjectName='{seriesCommonInfo.ProjectName}',ProjectNames='{changedProjectNames}',PlanRestOfCurrentFY='{rest}',PlanFirstFollowingFY='{frist}',PlanSecondFollowingFY='{seriesCommonInfo.PlanSecondFollowingFY}',PlanThirdFollowingFY='{seriesCommonInfo.PlanThirdFollowingFY}',SumPlan='{sumPlan}',ComponentQuantity='{seriesCommonInfo.ComponentQuantity}',TotalDemand='{totalDemand}' where ID='{seriesCommonInfo.ID}' and  ProjectID='{seriesCommonInfo.ProjectID}' ";
                    isOk += repository.DapperContext.ExcuteNonQuery(updateSeriesSql, null);


                    if (isOk > 0)
                    {
                        SetProjectSeriesCostsAndDemand((int)seriesCommonInfo.ProjectID);
                    }
                    #region MyRegion
                    //先判断是否添加过
                    //string isaddSeries = $"select * from [dbo].[LTB_SeriesDemands] where CounterMaterial='{seriesCommonInfo.CounterMaterial}'";
                    //List<LTB_SeriesDemands> list = repository.DapperContext.QueryList<LTB_SeriesDemands>(isaddSeries, null);
                    //decimal restFY = seriesCommonInfo.PlanRestOfCurrentFY == null ? 0 : seriesCommonInfo.PlanRestOfCurrentFY;
                    //decimal SumPlan = (decimal)(seriesCommonInfo.PlanRestOfCurrentFY + seriesCommonInfo.PlanFirstFollowingFY + seriesCommonInfo.PlanSecondFollowingFY + seriesCommonInfo.PlanThirdFollowingFY);
                    //decimal TotalDemand = SumPlan * (decimal)seriesCommonInfo.ComponentQuantity;
                    //if (list.Count == 0)
                    //{
                    //未添加过 可以新增
                    ////先添加comm
                    //string insertcommSql = "insert into [dbo].[LTB_SSCommon] (ProjectID,CounterMaterial,CounterMaterialDescription,Plant,SendingPlant,CounterType,CoverageType,ProjectName,ComponentQuantity) ";
                    //insertcommSql += $" values ('{seriesCommonInfo.ProjectID}','{seriesCommonInfo.CounterMaterial}','{seriesCommonInfo.CounterMaterialDescription}','{seriesCommonInfo.Plant}','{seriesCommonInfo.SendingPlant}','{seriesCommonInfo.CounterType}','{seriesCommonInfo.CoverageType}','{seriesCommonInfo.ProjectName}','{seriesCommonInfo.ComponentQuantity}')";
                    //isOk = repository.DapperContext.ExcuteNonQuery(insertcommSql, null);
                    ////获取刚刚新增的comm的最新的ID
                    //string newsql = "SELECT top(1) * FROM [dbo].[LTB_SSCommon] order by ID desc";
                    //List<SeriesCommon> newlist = repository.DapperContext.QueryList<SeriesCommon>(newsql, null);
                    //int commID = newlist[0].ID;
                    //再添加Series
                    //string insertSeriesSql = " insert into [dbo].[LTB_SeriesDemands] (ProjectID,CounterMaterial,CounterMaterialDescription,PlantID,SendingPlantID,Customer,CounterType,CoverageType,OnStockFrom,OnStockUntil,ProjectName,PlanRestOfCurrentFY,PlanFirstFollowingFY,PlanSecondFollowingFY,PlanThirdFollowingFY,SumPlan,ComponentQuantity,TotalDemand) ";
                    //insertSeriesSql += $"  values ('{seriesCommonInfo.ProjectID}','{seriesCommonInfo.CounterMaterial}','{seriesCommonInfo.CounterMaterialDescription}','{seriesCommonInfo.PlantID}','{seriesCommonInfo.SendingPlantID}','{seriesCommonInfo.Customer}','{seriesCommonInfo.CounterType}','{seriesCommonInfo.CoverageType}','{seriesCommonInfo.OnStockFrom}','{seriesCommonInfo.OnStockUntil}','{seriesCommonInfo.ProjectName}','{seriesCommonInfo.PlanRestOfCurrentFY}','{seriesCommonInfo.PlanFirstFollowingFY}','{seriesCommonInfo.PlanSecondFollowingFY}','{seriesCommonInfo.PlanThirdFollowingFY}','{seriesCommonInfo.SumPlan}','{seriesCommonInfo.ComponentQuantity}','{seriesCommonInfo.TotalDemand}')";
                    //isOk += repository.DapperContext.ExcuteNonQuery(insertSeriesSql, null);
                    //}
                    //else if (list.Count == 1)
                    //{
                    //添加过 可以编辑
                    ////先编辑comm
                    //string updatecommSql = $"update [dbo].[LTB_SSCommon] set CounterMaterialDescription='{seriesCommonInfo.CounterMaterialDescription}',Plant='{seriesCommonInfo.Plant}',SendingPlant='{seriesCommonInfo.SendingPlant}',CounterType='{seriesCommonInfo.CounterType}',CoverageType='{seriesCommonInfo.CoverageType}',ProjectName='{seriesCommonInfo.ProjectName}',ComponentQuantity='{seriesCommonInfo.ComponentQuantity}' where ProjectID='{seriesCommonInfo.ProjectID}' and CounterMaterial='{seriesCommonInfo.CounterMaterial}' ";
                    //isOk = repository.DapperContext.ExcuteNonQuery(updatecommSql, null);
                    //在编辑Series
                    //string updateSeriesSql = $"update [dbo].LTB_SeriesDemands set CounterMaterialDescription='{seriesCommonInfo.CounterMaterialDescription}',PlantID='{seriesCommonInfo.PlantID}',SendingPlantID='{seriesCommonInfo.SendingPlantID}',Customer='{seriesCommonInfo.Customer}',CounterType='{seriesCommonInfo.CounterType}',CoverageType='{seriesCommonInfo.CoverageType}',OnStockFrom='{seriesCommonInfo.OnStockFrom}',OnStockUntil='{seriesCommonInfo.OnStockUntil}',ProjectName='{seriesCommonInfo.ProjectName}',PlanRestOfCurrentFY='{seriesCommonInfo.PlanRestOfCurrentFY}',PlanFirstFollowingFY='{seriesCommonInfo.PlanFirstFollowingFY}',PlanSecondFollowingFY='{seriesCommonInfo.PlanSecondFollowingFY}',PlanThirdFollowingFY='{seriesCommonInfo.PlanThirdFollowingFY}',SumPlan='{seriesCommonInfo.SumPlan}',ComponentQuantity='{seriesCommonInfo.ComponentQuantity}',TotalDemand='{seriesCommonInfo.TotalDemand}' where CounterMaterial='{seriesCommonInfo.CounterMaterial}' and  ProjectID='{seriesCommonInfo.ProjectID}' ";
                    //isOk += repository.DapperContext.ExcuteNonQuery(updateSeriesSql, null);
                    //}
                    #endregion
                }

                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }

        }
        public int AddSeriesCommon(int ProjectID)
        {
            string msg = string.Empty;
            try
            {
                string insertSeriesSql = " insert into [dbo].[LTB_SeriesDemands] (ProjectID,CounterMaterial,CounterMaterialDescription,PlantID,SendingPlantID,Customer,CounterTypeID,CoverageType,OnStockFrom,OnStockUntil,ProjectName,ProjectNames,PlanRestOfCurrentFY,PlanFirstFollowingFY,PlanSecondFollowingFY,PlanThirdFollowingFY,SumPlan,ComponentQuantity,TotalDemand) ";
                insertSeriesSql += $" values ('{ProjectID}','','','','','','','','','','','','0','0','0','0','0','0','0')";
                int isOk = repository.DapperContext.ExcuteNonQuery(insertSeriesSql, null);

                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }
        public int DialogAddMaterial(string MaterialTextarea, string ProjectID)
        {
            string msg = string.Empty;
            int isOk = 0;
            try
            {
                List<string> value = new List<string>();
                //Regex reg1 = new Regex(@"<[^>]+>([^<>]+)<[^>]+>", RegexOptions.IgnoreCase);//此用于取一个时，不是集合时应用。  
                //string str = reg1.Match(MaterialTextarea).Groups[1].Value;

                //Regex reg12 = new Regex(@"<td.*>([^<>]+)</td>", RegexOptions.IgnoreCase);
                //string str1 = reg12.Match(MaterialTextarea).Groups[1].Value;

                //MatchCollection match = Regex.Matches(MaterialTextarea, @"<td[^>].*?>(?<content>[^<>]+)</td>", RegexOptions.Compiled);
                //foreach (Match m in match)
                //{
                //    value.Add(m.Groups["content"].Value);
                //}

                //MatchCollection match3 = Regex.Matches(MaterialTextarea, @"<td[^>]+>(?<content>[^<>]+)</td>", RegexOptions.Compiled);
                //foreach (Match m in match3)
                //{
                //    value.Add(m.Groups["content"].Value);
                //}

                //正则表达式获取table里的值 以下三种都可以获得
                //最准确的一种
                MatchCollection match2 = Regex.Matches(MaterialTextarea, @"<[^>]+>(?<content>[^<>]+)<[^>]+>", RegexOptions.Compiled);  //Success  
                foreach (Match m in match2)
                {
                    value.Add(m.Groups["content"].Value);
                }
                //添加Material
                if (value.Count != 0)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        string Material = value[i].ToString();
                        string DescriptionMaterial = value[i + 1].ToString();
                        i++; //直接到下一个Material
                        string insertSeriesSql = " insert into [dbo].[LTB_SeriesDemands] (ProjectID,CounterMaterial,CounterMaterialDescription,PlantID,SendingPlantID,Customer,CounterTypeID,CoverageType,OnStockFrom,OnStockUntil,ProjectName,PlanRestOfCurrentFY,PlanFirstFollowingFY,PlanSecondFollowingFY,PlanThirdFollowingFY,SumPlan,ComponentQuantity,TotalDemand) ";
                        insertSeriesSql += $" values ('{ProjectID}','{Material}','{DescriptionMaterial}','','','','','','','','','0','0','0','0','0','0','0')";
                        isOk += repository.DapperContext.ExcuteNonQuery(insertSeriesSql, null);
                    }
                }

                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }

        public int DeleteSeriesCommon(int ID, int ProjectID)
        {
            string msg = string.Empty;
            try
            {
                string delSql = $"delete [dbo].[LTB_SeriesDemands] where id='{ID}' and ProjectID='{ProjectID}'";
                int isOk = repository.DapperContext.ExcuteNonQuery(delSql, null);
                if (isOk > 0)
                {
                    SetProjectSeriesCostsAndDemand(ProjectID);
                }
                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }
        //无论是添加、修改还是删除，project的Total Series Costs和Total Series Demand都会跟着变化
        public int SetProjectSeriesCostsAndDemand(int ProjectID)
        {
            string msg = string.Empty;
            try
            {
                int isOk = 0;
                string getProject = $"SELECT * FROM [dbo].[LTB_Project] where ID='{ProjectID}'";
                List<LTB_Project> projectList = repository.DapperContext.QueryList<LTB_Project>(getProject, null);
                decimal price = (decimal)projectList[0].Price;
                string getSeriesDemands = $"select * from [dbo].[LTB_SeriesDemands] where ProjectID='{ProjectID}'";
                List<LTB_SeriesDemands> list = repository.DapperContext.QueryList<LTB_SeriesDemands>(getSeriesDemands, null);
                if (list.Count > 0)
                {
                    decimal total = 0;
                    foreach (var l in list)
                    {
                        total += (decimal)l.TotalDemand;
                    }
                    decimal totalSeriesDemand = total;
                    decimal totalSeriesCosts = total * price;

                    string updateSql = $"update [dbo].[LTB_Project] set TotalSeriesDemand='{totalSeriesDemand}',TotalSeriesCosts='{totalSeriesCosts}' where ID='{ProjectID}' ";
                    isOk = repository.DapperContext.ExcuteNonQuery(updateSql, null);
                }
                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }


        public string GetchangedProjectNames(string ProjectName)
        {
            string msg = string.Empty;
            try
            {
                string changedProjectNames = "";
                ProjectName = String.Join(",", ProjectName.Split(';')); //分号换成逗号
                //1：先获取ProjectGroup
                string getProjectGroupSql = $" select distinct ProjectGroup from [dbo].[LTB_ProjectNameDropdown] where rowid in ({ProjectName}) ";
                List<string> ProjectGroupList = repository.DapperContext.QueryList<string>(getProjectGroupSql, null);
                //2：再将获取的ProjectName进行拼接
                foreach (var group in ProjectGroupList)
                {
                    string getProjectNameSql = $" select distinct ProjectName,ProjectGroup from [dbo].[LTB_ProjectNameDropdown] where rowid in ({ProjectName}) and ProjectGroup='{group}' ";
                    List<LTB_ProjectNameDropdown> DropdownList = repository.DapperContext.QueryList<LTB_ProjectNameDropdown>(getProjectNameSql, null);
                    string names = group;
                    foreach (var dropdown in DropdownList)
                    {
                        string name = dropdown.ProjectName;
                        names += name.Replace(group, "")+"/";
                    }
                    //去掉names最后一个多余的'/'
                    names = names.Substring(0, names.Length - 1);
                    changedProjectNames += names + ","; //拼起来 如： P68A/B, P81A/B,
                }
                //去掉changedProjectNames最后一个多余的','
                changedProjectNames = changedProjectNames.Substring(0, changedProjectNames.Length - 1);
                return changedProjectNames;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return msg;
            }
        }


        //Service
        public List<LTB_ServiceDemands> GetServiceCommon(string spn, string ProjectID, string searchMaterial)
        {
            string msg = string.Empty;
            try
            {
                string sql = $" select s.*,pl.Plant,sp.SendingPlant,c.CounterType from [dbo].[LTB_Project] p  left join [dbo].[LTB_ServiceDemands] s on p.ID=s.ProjectID left join [dbo].[LTB_Plant] pl on pl.ID=s.PlantID left join [dbo].[LTB_SendingPlant] sp on sp.ID=s.SendingPlantID   left join [dbo].[LTB_CounterType] c on c.ID=s.CounterTypeID where 1=1 and s.ID is not null ";
                if (!string.IsNullOrEmpty(spn))
                {
                    sql += $" and SPN='{spn}' ";
                }
                if (!string.IsNullOrEmpty(ProjectID))
                {
                    sql += $" and ProjectID='{ProjectID}' ";
                }
                if (!string.IsNullOrEmpty(searchMaterial))
                {
                    sql += $" and CounterMaterial like '%{searchMaterial}%' ";
                }
                List<LTB_ServiceDemands> list = repository.DapperContext.QueryList<LTB_ServiceDemands>(sql, null);
                if (list.Count != 0)
                {
                    foreach (var series in list)
                    {
                        string projectNameID = series.ProjectName;
                        if (!string.IsNullOrEmpty(projectNameID))
                        {
                            string[] nameIDs = projectNameID.Split(';');
                            int[] proNameID = Array.ConvertAll<string, int>(nameIDs, Convert.ToInt32); //将string[]转为int[]，用于回显project的多选拉下
                            series.ProjectNameID = proNameID;
                            #region 拼接ProjectName
                            //string names = "";
                            //if (nameIDs.Length != 0)
                            //{
                            //    foreach (var nameID in nameIDs)
                            //    {
                            //        //string getProjectNameSql = $"SELECT * FROM [dbo].[LTB_ProjectName] where ID='{nameID}'"; //级联多选下拉
                            //        //List<LTB_ProjectName> namelist = repository.DapperContext.QueryList<LTB_ProjectName>(getProjectNameSql, null);

                            //        string getProjectNameSql = $"SELECT * FROM [dbo].[LTB_ProjectNameDropdown] where rowID='{nameID}'";  //多选下拉
                            //        List<LTB_ProjectNameDropdown> namelist = repository.DapperContext.QueryList<LTB_ProjectNameDropdown>(getProjectNameSql, null);
                            //        if (namelist.Count != 0)
                            //        {
                            //            string pname = namelist[0].ProjectName.ToString();
                            //            names += pname + ";";
                            //        }
                            //    }

                            //}
                            //names = names.Substring(0, names.Length - 1); //去掉最后一个;
                            ////若同时选中 P81A和P81B 显示成 P81A/B
                            //if (names.Contains("P81A") && names.Contains("P81B"))
                            //{
                            //    names = names.Replace("P81A", "P81A/B").Replace("P81B", "P81A/B");
                            //    //先将names转成数组再去重
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    //再转成字符串
                            //    names = String.Join(";", arrNames);
                            //}
                            ////同上操作 Denali LP1&Denali LP2 ==> Denali LP1/LP2
                            //if (names.Contains("Denali LP1") && names.Contains("Denali LP2"))
                            //{
                            //    names = names.Replace("Denali LP1", "Denali LP1/LP2").Replace("Denali LP2", "Denali LP1/LP2");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            ////同上操作 P60A & P60B ==> P60A/B
                            //if (names.Contains("P60A") && names.Contains("P60B"))
                            //{
                            //    names = names.Replace("P60A", "P60A/B").Replace("P60B", "P60A/B");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// P82A/B & PB2C ==> P82A/B/C
                            //if (names.Contains("P82A/B") && names.Contains("P82C"))
                            //{
                            //    names = names.Replace("P82A/B", "P82A/B/C").Replace("P82C", "P82A/B/C");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// T2;T6;T16 --> T2/6/16
                            //if (names.Contains("T2") && names.Contains("T6") && !names.Contains("T16"))
                            //{
                            //    names = names.Replace("T2", "T2/6").Replace("T6", "T2/6");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("T2") && !names.Contains("T6") && names.Contains("T16"))
                            //{
                            //    names = names.Replace("T2", "T2/16").Replace("T16", "T2/16");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("T2") && names.Contains("T6") && names.Contains("T16"))
                            //{
                            //    names = names.Replace("T6", "T6/16").Replace("T16", "T6/16");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("T2") && names.Contains("T6") && names.Contains("T16"))
                            //{
                            //    names = names.Replace("T2", "T2/6/16").Replace("T6", "T2/6/16").Replace("T16", "T2/6/16");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// P68A; P68B; P68G --> P68A/B/G
                            //if (names.Contains("P68A") && names.Contains("P68B") && !names.Contains("P68G"))
                            //{
                            //    names = names.Replace("P68A", "P68A/B").Replace("P68B", "P68A/B");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P68A") && names.Contains("P68G") && !names.Contains("P68B"))
                            //{
                            //    names = names.Replace("P68A", "P68A/G").Replace("P68G", "P68A/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P68B") && names.Contains("P68G") && !names.Contains("P68A"))
                            //{
                            //    names = names.Replace("P68B", "P68B/G").Replace("P68G", "P68B/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P68A") && names.Contains("P68B") && names.Contains("P68G"))
                            //{
                            //    names = names.Replace("P68A", "P68A/B/G").Replace("P68B", "P68A/B/G").Replace("P68G", "P68A/B/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// P15A; P15P; P15F; P15G --> P15A/P/F/G
                            //if (names.Contains("P15A") && names.Contains("P15P") && names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/P/F/G").Replace("P15P", "P15A/P/F/G").Replace("P15F", "P15A/P/F/G").Replace("P15G", "P15A/P/F/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && names.Contains("P15P") && names.Contains("P15F") && !names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/P/F").Replace("P15P", "P15A/P/F").Replace("P15F", "P15A/P/F");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && names.Contains("P15P") && !names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/P/G").Replace("P15P", "P15A/P/G").Replace("P15G", "P15A/P/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && !names.Contains("P15P") && names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/F/G").Replace("P15F", "P15A/F/G").Replace("P15G", "P15A/F/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("P15A") && names.Contains("P15P") && names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15P", "P15P/F/G").Replace("P15F", "P15P/F/G").Replace("P15G", "P15P/F/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && names.Contains("P15P") && !names.Contains("P15F") && !names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/P").Replace("P15P", "P15A/P");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && !names.Contains("P15P") && names.Contains("P15F") && !names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/F").Replace("P15F", "P15A/F");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (names.Contains("P15A") && !names.Contains("P15P") && !names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15A", "P15A/G").Replace("P15G", "P15A/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("P15A") && names.Contains("P15P") && names.Contains("P15F") && !names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15P", "P15P/F").Replace("P15F", "P15P/F");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("P15A") && names.Contains("P15P") && !names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15P", "P15P/G").Replace("P15G", "P15P/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //else if (!names.Contains("P15A") && !names.Contains("P15P") && names.Contains("P15F") && names.Contains("P15G"))
                            //{
                            //    names = names.Replace("P15F", "P15F/G").Replace("P15G", "P15F/G");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}
                            //// P15S & P15T --> P15S/T
                            //if (names.Contains("P15S") && names.Contains("P15T"))
                            //{
                            //    names = names.Replace("P15S", "P15S/T").Replace("P15T", "P15S/T");
                            //    string[] arrNames = names.Split(';').Distinct().ToArray();
                            //    names = String.Join(";", arrNames);
                            //}

                            //series.ProjectNames = names;
                            #endregion

                        }
                        series.isEdit = true; //用于控制前台table行编辑的开启与关闭
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return null;
            }
        }
        public int UpdateServiceCommon([FromQuery] string serviceInfo)
        {
            string msg = string.Empty;
            try
            {
                LTB_ServiceDemands serviceCommonInfo = JsonConvert.DeserializeObject<LTB_ServiceDemands>(serviceInfo);
                int isOk = 0;
                if (serviceCommonInfo != null)
                {
                    //百分比计算
                    if (serviceCommonInfo.SumPercentage > 0)
                    {
                        serviceCommonInfo.TotalDemand = Math.Ceiling((decimal)(serviceCommonInfo.TotalDemand * serviceCommonInfo.SumPercentage / 100)); //向上取整
                    }
                    //根据多选下拉的ID，获取拼接后的值
                    string changedProjectNames = GetchangedProjectNames(serviceCommonInfo.ProjectName);
                    //第一版：级联多选下拉
                    string updateSeriesSql = $"update [dbo].LTB_ServiceDemands set CounterMaterial='{serviceCommonInfo.CounterMaterial}',CounterMaterialDescription='{serviceCommonInfo.CounterMaterialDescription}',PlantID='{serviceCommonInfo.PlantID}',SendingPlantID='{serviceCommonInfo.SendingPlantID}',Customer='{serviceCommonInfo.Customer}',CounterTypeID='{serviceCommonInfo.CounterTypeID}',CoverageType='{serviceCommonInfo.CoverageType}',OnStockFrom='{serviceCommonInfo.OnStockFrom}',OnStockUntil='{serviceCommonInfo.OnStockUntil}',ProjectName='{serviceCommonInfo.ProjectName}',ProjectNames='{changedProjectNames}',RepairPart='{serviceCommonInfo.RepairPart}',RepairsUntil_EOSOrESD='{serviceCommonInfo.RepairsUntil_EOSOrESD}',EndOfService_EOSOrESD='{serviceCommonInfo.EndOfService_EOSOrESD}',SumServiceDemand='{serviceCommonInfo.SumServiceDemand}',SumPercentage='{serviceCommonInfo.SumPercentage}',ComponentQuantity='{serviceCommonInfo.ComponentQuantity}',TotalDemand='{serviceCommonInfo.TotalDemand}' where ID='{serviceCommonInfo.ID}' and  ProjectID='{serviceCommonInfo.ProjectID}' ";
                    isOk += repository.DapperContext.ExcuteNonQuery(updateSeriesSql, null);

                    //第二版：多选下拉
                    //string projectName = String.Join(";", serviceCommonInfo.ProjectNameID);
                    //string updateServiceSql = $"update [dbo].LTB_ServiceDemands set CounterMaterial='{serviceCommonInfo.CounterMaterial}',CounterMaterialDescription='{serviceCommonInfo.CounterMaterialDescription}',PlantID='{serviceCommonInfo.PlantID}',SendingPlantID='{serviceCommonInfo.SendingPlantID}',Customer='{serviceCommonInfo.Customer}',CounterTypeID='{serviceCommonInfo.CounterTypeID}',CoverageType='{serviceCommonInfo.CoverageType}',OnStockFrom='{serviceCommonInfo.OnStockFrom}',OnStockUntil='{serviceCommonInfo.OnStockUntil}',ProjectName='{projectName}',RepairPart='{serviceCommonInfo.RepairPart}',RepairsUntil_EOSOrESD='{serviceCommonInfo.RepairsUntil_EOSOrESD}',EndOfService_EOSOrESD='{serviceCommonInfo.EndOfService_EOSOrESD}',SumServiceDemand='{serviceCommonInfo.SumServiceDemand}',SumPercentage='{serviceCommonInfo.SumPercentage}',ComponentQuantity='{serviceCommonInfo.ComponentQuantity}',TotalDemand='{serviceCommonInfo.TotalDemand}' where ID='{serviceCommonInfo.ID}' and  ProjectID='{serviceCommonInfo.ProjectID}' ";
                    //isOk += repository.DapperContext.ExcuteNonQuery(updateServiceSql, null);

                    if (isOk > 0)
                    {
                        SetProjectServiceCostsAndDemand((int)serviceCommonInfo.ProjectID);
                    }

                }

                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }

        }
        public int AddServiceCommon(int ProjectID)
        {
            string msg = string.Empty;
            try
            {
                string insertServiceSql = " insert into [dbo].[LTB_ServiceDemands] (ProjectID,CounterMaterial,CounterMaterialDescription,PlantID,SendingPlantID,Customer,CounterTypeID,CoverageType,OnStockFrom,OnStockUntil,ProjectName,ProjectNames,RepairPart,RepairsUntil_EOSOrESD,EndOfService_EOSOrESD,SumServiceDemand,SumPercentage,ComponentQuantity,TotalDemand) ";
                //insertSeriesSql += $" values ('{ProjectID}','','','1','1','','','','','','','0','0','0','0','0','0')";
                insertServiceSql += $" values ('{ProjectID}','','','','','','','','','','','','0','0','0','0','0','0','0')";
                int isOk = repository.DapperContext.ExcuteNonQuery(insertServiceSql, null);

                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }
        public int DialogAddServiceMaterial(string MaterialTextarea, string ProjectID)
        {
            string msg = string.Empty;
            int isOk = 0;
            try
            {
                List<string> value = new List<string>();

                //正则表达式获取table里的值 
                MatchCollection match2 = Regex.Matches(MaterialTextarea, @"<[^>]+>(?<content>[^<>]+)<[^>]+>", RegexOptions.Compiled);  //Success  
                foreach (Match m in match2)
                {
                    value.Add(m.Groups["content"].Value);
                }
                //添加Material
                if (value.Count != 0)
                {
                    for (int i = 0; i < value.Count; i++)
                    {
                        string Material = value[i].ToString();
                        string DescriptionMaterial = value[i + 1].ToString();
                        i++; //直接到下一个Material
                        string insertServiceSql = " insert into [dbo].[LTB_ServiceDemands] (ProjectID,CounterMaterial,CounterMaterialDescription,PlantID,SendingPlantID,Customer,CounterTypeID,CoverageType,OnStockFrom,OnStockUntil,ProjectName,RepairPart,RepairsUntil_EOSOrESD,EndOfService_EOSOrESD,SumServiceDemand,SumPercentage,ComponentQuantity,TotalDemand) ";
                        insertServiceSql += $" values ('{ProjectID}','{Material}','{DescriptionMaterial}','','','','','','','','','0','0','0','0','0','0','0')";
                        isOk += repository.DapperContext.ExcuteNonQuery(insertServiceSql, null);
                    }
                }

                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }

        public int DeleteServiceCommon(int ID, int ProjectID)
        {
            string msg = string.Empty;
            try
            {
                string delSql = $"delete [dbo].[LTB_ServiceDemands] where id='{ID}' and ProjectID='{ProjectID}'";
                int isOk = repository.DapperContext.ExcuteNonQuery(delSql, null);
                if (isOk > 0)
                {
                    SetProjectServiceCostsAndDemand(ProjectID);
                }
                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }
        //无论是添加、修改还是删除，project的Total Service Costs和Total Service Demand都会跟着变化
        public int SetProjectServiceCostsAndDemand(int ProjectID)
        {
            string msg = string.Empty;
            try
            {
                int isOk = 0;
                string getProject = $"SELECT * FROM [dbo].[LTB_Project] where ID='{ProjectID}'";
                List<LTB_Project> projectList = repository.DapperContext.QueryList<LTB_Project>(getProject, null);
                decimal price = (decimal)projectList[0].Price;
                string getServiceDemands = $"select * from [dbo].[LTB_ServiceDemands] where ProjectID='{ProjectID}'";
                List<LTB_ServiceDemands> list = repository.DapperContext.QueryList<LTB_ServiceDemands>(getServiceDemands, null);
                if (list.Count > 0)
                {
                    decimal total = 0;
                    foreach (var l in list)
                    {
                        total += (decimal)l.TotalDemand;
                    }
                    decimal totalServiceDemand = total;
                    decimal totalServiceCosts = total * price;

                    string updateSql = $"update [dbo].[LTB_Project] set TotalServiceDemand='{totalServiceDemand}',TotalServiceCosts='{totalServiceCosts}' where ID='{ProjectID}' ";
                    isOk = repository.DapperContext.ExcuteNonQuery(updateSql, null);
                }
                return isOk;
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return 0;
            }
        }



        public Summary GetSummary(string pdn)
        {
            string msg = string.Empty;
            try
            {
                Summary summary = new Summary();
                string projectSql = $"SELECT * FROM [dbo].[LTB_Project] where PDN = '{pdn}' ";
                List<LTB_Project> projectList = repository.DapperContext.QueryList<LTB_Project>(projectSql, null);

                return getSummaryByprojectID(projectList, summary);
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                Logger.Error(msg);
                return null;
            }
        }
        public Summary getSummaryByprojectID(List<LTB_Project> projectList, Summary summary)
        {
            var project = projectList.FirstOrDefault();

            if (project != null && project.State.ToUpper() == "NEW")
                return new Summary
                {
                    Project = new Project
                    {
                        SPN = project?.SPN,
                        StockUpStartdate = Convert.ToDateTime(project?.StockUpStartdate).ToString("yyyy-MM-dd"),
                        State = project?.State
                    }
                };

            if (projectList.Any())
            {
                string seriesDemandsSql = $"SELECT * FROM [dbo].[LTB_SeriesDemands] where ProjectID={project?.ID}";
                string serviceDemandssSql = $"SELECT * FROM [dbo].[LTB_ServiceDemands] where ProjectID={project?.ID}";

                var priceTypeID = SummayMethods.CurrencySymbol(project.PriceTypeID.ToString());

                List<LTB_SeriesDemands> seriesDemandsList = repository.DapperContext.QueryList<LTB_SeriesDemands>(seriesDemandsSql, null);
                List<LTB_ServiceDemands> serviceDemandsList = repository.DapperContext.QueryList<LTB_ServiceDemands>(serviceDemandssSql, null);

                var ssdsList = new List<SeriesDemands>();
                var sedsList = new List<ServiceDemands>();

                if (seriesDemandsList.Any())
                {
                    ssdsList = seriesDemandsList.Select(o => new SeriesDemands
                    {
                        CounterMaterial = o.CounterMaterial,
                        CounterMaterialDescription = o.CounterMaterialDescription,
                        PlantID = SummayMethods.PlantName(o.PlantID),
                        SendingPlantID = SummayMethods.SendingPlant(o.SendingPlantID),
                        Customer = o.Customer,
                        CounterTypeID = (int)o.CounterTypeID,
                        CoverageType = o.CoverageType,
                        OnStockFrom = Convert.ToDateTime(o.OnStockFrom).ToString("M/d/yyyy"),
                        OnStockUntil = Convert.ToDateTime(o.OnStockUntil).ToString("M/d/yyyy"),
                        //ProjectName = SummayMethods.ProjectName(o.ProjectName),
                        ProjectName = o.ProjectNames,
                        PlanRestOfCurrentFY = SummayMethods.Format(o.PlanRestOfCurrentFY.ToString(), false),
                        PlanFirstFollowingFY = SummayMethods.Format(o.PlanFirstFollowingFY.ToString(), false),
                        PlanSecondFollowingFY = SummayMethods.Format(o.PlanSecondFollowingFY.ToString(), false),
                        PlanThirdFollowingFY = SummayMethods.Format(o.PlanThirdFollowingFY?.ToString().ToString(), false),
                        SumPlan = SummayMethods.Format(o.SumPlan.ToString(), false),
                        ComponentQuantity = SummayMethods.Format(o.ComponentQuantity.ToString(), false),
                        TotalDemand = SummayMethods.Format(o.TotalDemand.ToString(), false)
                    }).ToList();
                }

                if (serviceDemandsList.Any())
                {
                    sedsList = serviceDemandsList.Select(o => new ServiceDemands
                    {
                        CounterMaterial = o.CounterMaterial,
                        CounterMaterialDescription = o.CounterMaterialDescription,
                        PlantID = SummayMethods.PlantName(o.PlantID),
                        SendingPlantID = SummayMethods.SendingPlant(o.SendingPlantID),
                        Customer = o.Customer,
                        CounterTypeID = (int)o.CounterTypeID,
                        CoverageType = o.CoverageType,
                        OnStockFrom = Convert.ToDateTime(o.OnStockFrom).ToString("M/d/yyyy"),
                        OnStockUntil = Convert.ToDateTime(o.OnStockUntil).ToString("M/d/yyyy"),
                        //ProjectName = SummayMethods.ProjectName(o.ProjectName),
                        ProjectName = o.ProjectNames,
                        RepairPart = o.RepairPart,
                        RepairsUntil_EOSOrESD = SummayMethods.Format(o.RepairsUntil_EOSOrESD.ToString(), false),
                        EndOfService_EOSOrESD = SummayMethods.Format(o.EndOfService_EOSOrESD.ToString(), false),
                        SumServiceDemand = SummayMethods.Format(o.SumServiceDemand?.ToString().ToString(), false),
                        ComponentQuantity = SummayMethods.Format(o.ComponentQuantity.ToString(), false),
                        TotalDemand = SummayMethods.Format(o.TotalDemand.ToString(), false)
                    }).ToList();
                }

                return new Summary
                {
                    Project = new Project
                    {
                        PDN = project.PDN,
                        SPN = project.SPN,
                        BL = project.BL,
                        ObsoleteMaterialDescription = project.ObsoleteMaterialDescription,
                        AffectedBLs = project.AffectedBLs,
                        Manufacturer = project.Manufacturer,
                        StockUpStartdate = Convert.ToDateTime(project.StockUpStartdate).ToString("yyyy-MM-dd"),
                        Price = project.Price.ToString() + priceTypeID,
                        TotalSeriesDemand = SummayMethods.Format(project.TotalSeriesDemand.ToString(), false) + " Pcs",
                        TotalSeriesCosts = SummayMethods.Format(project.TotalSeriesCosts.ToString(), true) + priceTypeID,
                        TotalServiceDemand = SummayMethods.Format(project.TotalServiceDemand.ToString(), false) + " Pcs",
                        TotalServiceCosts = SummayMethods.Format(project.TotalServiceCosts.ToString(), true) + priceTypeID,
                        CreatederDepartment = project.CreatederDepartment,
                        Createder = project.Createder,
                        CreateDate = Convert.ToDateTime(project.CreateDate).ToString("yyyy-MM-dd"),
                        Approvaler1 = project.Approvaler1,
                        ApprovalerDepartment1 = project.ApprovalerDepartment1,
                        Approvaler2 = project.Approvaler2.ToString(),
                        Approvaler3 = project.Approvaler3,
                        ApprovalerDepartment2 = project.ApprovalerDepartment2,
                        ApprovalerDepartment3 = project.ApprovalerDepartment3,
                        ApprovalDate1 = Convert.ToDateTime(project.ApprovalDate1).ToString("yyyy-MM-dd"),
                        ApprovalDate2 = Convert.ToDateTime(project.ApprovalDate2).ToString("yyyy-MM-dd"),
                        ApprovalDate3 = Convert.ToDateTime(project.ApprovalDate3).ToString("yyyy-MM-dd"),
                        totalDemand = SummayMethods.Format((
                        Convert.ToDouble(project.TotalSeriesDemand.ToString()) +
                        Convert.ToDouble(project.TotalServiceDemand.ToString())).ToString(), false) + " Pcs",
                        totalCosts = SummayMethods.Format((Convert.ToDouble(project.TotalServiceCosts.ToString()) +
                        Convert.ToDouble(project.TotalSeriesCosts.ToString())).ToString("f2"), true) + priceTypeID,
                        State = project.State

                    },
                    SeriesDemandsList = ssdsList,
                    ServiceDemandsList = sedsList
                };
            }
            return null;
        }

    }
}
