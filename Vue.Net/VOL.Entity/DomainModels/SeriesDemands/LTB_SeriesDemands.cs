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
    [Entity(TableCnName = "Series Demands",TableName = "LTB_SeriesDemands")]
    public partial class LTB_SeriesDemands:BaseEntity
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
       ///本财年剩余计划
       /// </summary>
       [Display(Name ="本财年剩余计划")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? PlanRestOfCurrentFY { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="PlanFirstFollowingFY")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? PlanFirstFollowingFY { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="PlanSecondFollowingFY")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? PlanSecondFollowingFY { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="PlanThirdFollowingFY")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? PlanThirdFollowingFY { get; set; }

       /// <summary>
       ///计划总和
       /// </summary>
       [Display(Name ="计划总和")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? SumPlan { get; set; }

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