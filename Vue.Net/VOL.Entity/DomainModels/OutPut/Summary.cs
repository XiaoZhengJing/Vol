using System.Collections.Generic;

namespace VOL.Entity.DomainModels.OutPut
{
    /// <summary>
    /// 数据汇总
    /// </summary>
    public class Summary
    {
        public Project Project { get; set; }

        public List<SeriesDemands> SeriesDemandsList { get; set; }

        public List<ServiceDemands> ServiceDemandsList { get; set; }

    }

    /// <summary>
    /// Project
    /// </summary>
    public class Project
    {
        public string PDN { get; set; }
        public string SPN { get; set; }
        public string Manufacturer { get; set; }
        public string ObsoleteMaterialDescription { get; set; }
        public string AffectedBLs { get; set; }
        public string BL { get; set; }
        public string StockUpStartdate { get; set; }
        public string Price { get; set; }
        public string PriceType { get; set; }
        public string TotalSeriesDemand { get; set; }
        public string TotalSeriesCosts { get; set; }
        public string TotalServiceDemand { get; set; }
        public string TotalServiceCosts { get; set; }
        public string CreatederDepartment { get; set; }
        public string Createder { get; set; }
        public string CreateDate { get; set; }
        public string Approvaler1 { get; set; }
        public string ApprovalerDepartment1 { get; set; }

        public string ApprovalDate1 { get; set; }
        public string Approvaler2 { get; set; }
        public string ApprovalerDepartment2 { get; set; }
        public string ApprovalDate2 { get; set; }

        public string ApprovalerDepartment3 { get; set; }
        public string ApprovalDate3 { get; set; }
        public string Approvaler3 { get; set; }

        public string totalDemand { get; set; }

        public string totalCosts { get; set; }

        public string State { get; set; }

    }

    /// <summary>
    /// SeriesDemands
    /// </summary>
    public class SeriesDemands
    {
        public string CounterMaterial { get; set; }
        public string CounterMaterialDescription { get; set; }
        public string PlantID { get; set; }
        public string SendingPlantID { get; set; }
        public string Customer { get; set; }
        public int CounterTypeID { get; set; }
        public string CoverageType { get; set; }
        public string OnStockFrom { get; set; }
        public string OnStockUntil { get; set; }
        public string ProjectName { get; set; }
        public string PlanRestOfCurrentFY { get; set; }
        public string PlanFirstFollowingFY { get; set; }
        public string PlanSecondFollowingFY { get; set; }
        public string PlanThirdFollowingFY { get; set; }
        public string SumPlan { get; set; }
        public string ComponentQuantity { get; set; }
        public string TotalDemand { get; set; }
    }

    /// <summary>
    /// ServiceDemands
    /// </summary>
    public class ServiceDemands
    {
        public string CounterMaterial { get; set; }
        public string CounterMaterialDescription { get; set; }
        public string PlantID { get; set; }
        public string SendingPlantID { get; set; }
        public string Customer { get; set; }
        public int CounterTypeID { get; set; }
        public string CoverageType { get; set; }
        public string OnStockFrom { get; set; }
        public string OnStockUntil { get; set; }

        public string ProjectName { get; set; }
        public string RepairPart { get; set; }
        public string RepairsUntil_EOSOrESD { get; set; }
        public string EndOfService_EOSOrESD { get; set; }
        public string SumServiceDemand { get; set; }
        public string ComponentQuantity { get; set; }
        public string TotalDemand { get; set; }
    }
}
