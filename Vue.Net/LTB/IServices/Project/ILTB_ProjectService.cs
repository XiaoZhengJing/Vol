/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 */
using Microsoft.AspNetCore.Mvc;
using VOL.Core.BaseProvider;
using VOL.Core.Utilities;
using VOL.Entity.DomainModels;
using VOL.Entity.DomainModels.OutPut;

namespace LTB.IServices
{
    public partial interface ILTB_ProjectService : IService<LTB_Project>
    {
        //Project
        List<LTB_Project> GetProject(string pdn);
        int AddOrUpdateProject([FromQuery] string projectDataInfo, string type);
        int DeleteProject(string pIds);
        string ChangeProjectState(int ProjectID, string State);
        List<LTB_Project> SSGetProject(string spn, int ProjectID);
        int CopyDemands([FromQuery] string projectDataInfo);



        //Series
        List<LTB_SeriesDemands> GetSeriesCommon(string spn, string ProjectID, string searchMaterial);
        int UpdateSeriesCommon([FromQuery] string seriesInfo); //(LTB_SeriesDemands seriesCommonInfo);
        int AddSeriesCommon(int ProjectID);
        int DeleteSeriesCommon(int ID, int ProjectID);
        int SetProjectSeriesCostsAndDemand(int ProjectID);
        int DialogAddMaterial(string MaterialTextarea,string ProjectID);



        //Service
        List<LTB_ServiceDemands> GetServiceCommon(string spn, string ProjectID, string searchMaterial);
        int UpdateServiceCommon([FromQuery] string serviceInfo);
        int AddServiceCommon(int ProjectID);
        int DeleteServiceCommon(int ID, int ProjectID);
        int DialogAddServiceMaterial(string MaterialTextarea, string ProjectID);

        //下拉绑定
        List<LTB_Plant> GetPlant();
        List<LTB_SendingPlant> GetSendingPlant();
        List<LTB_CounterType> GetCounterType();
        List<ProjectNameOption> GetProjectName();
        List<SearchEmployee> GetEmployee();
        List<ProjectNameDropdown> GetProjectNameDropdown();

        //OutPut 
        Summary getSummary(string Id);
        Summary getSummaryByprojectID(List<LTB_Project> projectList, Summary summary);

    }
}
