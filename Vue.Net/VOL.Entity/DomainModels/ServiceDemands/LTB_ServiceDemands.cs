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
    [Entity(TableCnName = "Service Demands",TableName = "LTB_ServiceDemands")]
    public partial class LTB_ServiceDemands:BaseEntity
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
       ///顾客
       /// </summary>
       [Display(Name ="顾客")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string Customer { get; set; }

       /// <summary>
       ///库存开始时间
       /// </summary>
       [Display(Name ="库存开始时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? OnStockFrom { get; set; }

       /// <summary>
       ///库存结束时间
       /// </summary>
       [Display(Name ="库存结束时间")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? OnStockUntil { get; set; }

       /// <summary>
       ///修理部分
       /// </summary>
       [Display(Name ="修理部分")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string RepairPart { get; set; }

       /// <summary>
       ///之前的维修数量
       /// </summary>
       [Display(Name ="之前的维修数量")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? RepairsUntil_EOSOrESD { get; set; }

       /// <summary>
       ///延长服务结束
       /// </summary>
       [Display(Name ="延长服务结束")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? EndOfService_EOSOrESD { get; set; }

       /// <summary>
       ///合计服务需求
       /// </summary>
       [Display(Name ="合计服务需求")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? SumServiceDemand { get; set; }

       /// <summary>
       ///总需求
       /// </summary>
       [Display(Name ="总需求")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? TotalDemand { get; set; }

       /// <summary>
       ///LTB_SSCommon.ID
       /// </summary>
       [Display(Name ="LTB_SSCommon.ID")]
       [Column(TypeName="int")]
       public int? ProjectID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CounterMaterial")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       public string CounterMaterial { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CounterMaterialDescription")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string CounterMaterialDescription { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="PlantID")]
       [Column(TypeName="int")]
       public int? PlantID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="SendingPlantID")]
       [Column(TypeName="int")]
       public int? SendingPlantID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CoverageType")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       public string CoverageType { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProjectName")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string ProjectName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ComponentQuantity")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       public decimal? ComponentQuantity { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="SumPercentage")]
       [Column(TypeName="int")]
       public int? SumPercentage { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CounterTypeID")]
       [Column(TypeName="int")]
       public int? CounterTypeID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProjectNames")]
       [MaxLength(200)]
       [Column(TypeName="varchar(200)")]
       public string ProjectNames { get; set; }

        public string Plant { get; set; }
        public string SendingPlant { get; set; }
        public string CounterType { get; set; }

        [NotMapped]
        public int[] ProjectNameID { get; set; }
        public bool isEdit { get; set; }
    }
}