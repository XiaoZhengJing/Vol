/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOL.Entity.SystemModels;

namespace VOL.Entity.DomainModels
{
    [Entity(TableCnName = "SSCommon",TableName = "LTB_SSCommon")]
    public partial class LTB_SSCommon:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="ID")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int ID { get; set; }

       /// <summary>
       ///LTB_Project.ID
       /// </summary>
       [Display(Name ="LTB_Project.ID")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? ProjectID { get; set; }

       /// <summary>
       ///计数器材料
       /// </summary>
       [Display(Name ="计数器材料")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string CounterMaterial { get; set; }

       /// <summary>
       ///描述计数器材料
       /// </summary>
       [Display(Name ="描述计数器材料")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string CounterMaterialDescription { get; set; }

       /// <summary>
       ///设备
       /// </summary>
       [Display(Name ="设备")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string Plant { get; set; }

       /// <summary>
       ///发送设备
       /// </summary>
       [Display(Name ="发送设备")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string SendingPlant { get; set; }

       /// <summary>
       ///计数器类型
       /// </summary>
       [Display(Name ="计数器类型")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string CounterType { get; set; }

       /// <summary>
       ///范围类型
       /// </summary>
       [Display(Name ="范围类型")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string CoverageType { get; set; }

       /// <summary>
       ///部件数量
       /// </summary>
       [Display(Name ="部件数量")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? ComponentQuantity { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProjectName")]
       [MaxLength(200)]
       [Column(TypeName="varchar(200)")]
       public string ProjectName { get; set; }

       
    }

    public class SeriesCommon
    {
        //Series
        public int ID { get; set; }

        /// <summary>
        ///LTB_SSCommon.ID
        /// </summary>
        [Display(Name = "LTB_SSCommon.ID")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? SSCommonID { get; set; }

        /// <summary>
        ///顾客
        /// </summary>
        [Display(Name = "顾客")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string Customer { get; set; }

        /// <summary>
        ///库存开始时间
        /// </summary>
        [Display(Name = "库存开始时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? OnStockFrom { get; set; }

        /// <summary>
        ///库存结束时间
        /// </summary>
        [Display(Name = "库存结束时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? OnStockUntil { get; set; }

        /// <summary>
        ///本财年剩余计划
        /// </summary>
        [Display(Name = "本财年剩余计划")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? PlanRestOfCurrentFY { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "PlanFirstFollowingFY")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? PlanFirstFollowingFY { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "PlanSecondFollowingFY")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? PlanSecondFollowingFY { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "PlanThirdFollowingFY")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? PlanThirdFollowingFY { get; set; }

        /// <summary>
        ///计划总和
        /// </summary>
        [Display(Name = "计划总和")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? SumPlan { get; set; }

        /// <summary>
        ///总需求
        /// </summary>
        [Display(Name = "总需求")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? TotalDemand { get; set; }

        //Common
        /// <summary>
        ///LTB_Project.ID
        /// </summary>
        [Display(Name = "LTB_Project.ID")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? ProjectID { get; set; }

        /// <summary>
        ///计数器材料
        /// </summary>
        [Display(Name = "计数器材料")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string CounterMaterial { get; set; }

        /// <summary>
        ///描述计数器材料
        /// </summary>
        [Display(Name = "描述计数器材料")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        [Editable(true)]
        public string CounterMaterialDescription { get; set; }

        /// <summary>
        ///设备
        /// </summary>
        [Display(Name = "设备")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        [Editable(true)]
        public string Plant { get; set; }

        /// <summary>
        ///发送设备
        /// </summary>
        [Display(Name = "发送设备")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string SendingPlant { get; set; }

        /// <summary>
        ///计数器类型
        /// </summary>
        [Display(Name = "计数器类型")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? CounterType { get; set; }

        /// <summary>
        ///范围类型
        /// </summary>
        [Display(Name = "范围类型")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? CoverageType { get; set; }

        /// <summary>
        ///部件数量
        /// </summary>
        [Display(Name = "部件数量")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? ComponentQuantity { get; set; }

        [Display(Name = "ProjectName")]
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string ProjectName { get; set; }


    }

    public class ServiceCommon
    {
        //Service
        public int ID { get; set; }

        /// <summary>
        ///LTB_SSCommon.ID
        /// </summary>
        [Display(Name = "LTB_SSCommon.ID")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? SSCommonID { get; set; }

        /// <summary>
        ///顾客
        /// </summary>
        [Display(Name = "顾客")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string Customer { get; set; }

        /// <summary>
        ///库存开始时间
        /// </summary>
        [Display(Name = "库存开始时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? OnStockFrom { get; set; }

        /// <summary>
        ///库存结束时间
        /// </summary>
        [Display(Name = "库存结束时间")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? OnStockUntil { get; set; }

        /// <summary>
        ///修理部分
        /// </summary>
        [Display(Name = "修理部分")]
        [Column(TypeName = "tinyint")]
        [Editable(true)]
        public byte? RepairPart { get; set; }

        /// <summary>
        ///之前的维修数量
        /// </summary>
        [Display(Name = "之前的维修数量")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? RepairsUntil_EOSOrESD { get; set; }

        /// <summary>
        ///延长服务结束
        /// </summary>
        [Display(Name = "延长服务结束")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? EndOfService_EOSOrESD { get; set; }

        /// <summary>
        ///合计服务需求
        /// </summary>
        [Display(Name = "合计服务需求")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? SumServiceDemand { get; set; }

        /// <summary>
        ///总需求
        /// </summary>
        [Display(Name = "总需求")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? TotalDemand { get; set; }

        /// <summary>
        ///LTB_Project.ID
        /// </summary>
        [Display(Name = "LTB_Project.ID")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? ProjectID { get; set; }

        /// <summary>
        ///计数器材料
        /// </summary>
        [Display(Name = "计数器材料")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string CounterMaterial { get; set; }

        /// <summary>
        ///描述计数器材料
        /// </summary>
        [Display(Name = "描述计数器材料")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        [Editable(true)]
        public string CounterMaterialDescription { get; set; }

        /// <summary>
        ///设备
        /// </summary>
        [Display(Name = "设备")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        [Editable(true)]
        public string Plant { get; set; }

        /// <summary>
        ///发送设备
        /// </summary>
        [Display(Name = "发送设备")]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string SendingPlant { get; set; }

        /// <summary>
        ///计数器类型
        /// </summary>
        [Display(Name = "计数器类型")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? CounterType { get; set; }

        /// <summary>
        ///范围类型
        /// </summary>
        [Display(Name = "范围类型")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? CoverageType { get; set; }

        /// <summary>
        ///部件数量
        /// </summary>
        [Display(Name = "部件数量")]
        [DisplayFormat(DataFormatString = "10,2")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? ComponentQuantity { get; set; }

        [Display(Name = "ProjectName")]
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string ProjectName { get; set; }


    }


}