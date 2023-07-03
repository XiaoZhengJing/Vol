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
    [Entity(TableCnName = "Project",TableName = "LTB_Project")]
    public partial class LTB_Project:BaseEntity
    {
        /// <summary>
       ///主键
       /// </summary>
       [Key]
       [Display(Name ="主键")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int ID { get; set; }

       /// <summary>
       ///PDN
       /// </summary>
       [Display(Name ="PDN")]
       [MaxLength(20)]
       [Column(TypeName="varchar(20)")]
       [Editable(true)]
       public string PDN { get; set; }

       /// <summary>
       ///SPN
       /// </summary>
       [Display(Name ="SPN")]
       [MaxLength(20)]
       [Column(TypeName="varchar(20)")]
       [Editable(true)]
       public string SPN { get; set; }

       /// <summary>
       ///BL
       /// </summary>
       [Display(Name ="BL")]
       [MaxLength(20)]
       [Column(TypeName="varchar(20)")]
       [Editable(true)]
       public string BL { get; set; }

       /// <summary>
       ///Description
       /// </summary>
       [Display(Name ="Description")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string ObsoleteMaterialDescription { get; set; }

       /// <summary>
       ///Affected BLs
       /// </summary>
       [Display(Name ="Affected BLs")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string AffectedBLs { get; set; }

       /// <summary>
       ///Manufacturer
       /// </summary>
       [Display(Name ="Manufacturer")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string Manufacturer { get; set; }

       /// <summary>
       ///Stock up start date
       /// </summary>
       [Display(Name ="Stock up start date")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? StockUpStartdate { get; set; }

       /// <summary>
       ///Price
       /// </summary>
       [Display(Name ="Price")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? Price { get; set; }

       /// <summary>
       ///Createder Department
       /// </summary>
       [Display(Name ="Createder Department")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string CreatederDepartment { get; set; }

       /// <summary>
       ///Createder
       /// </summary>
       [Display(Name ="Createder")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string Createder { get; set; }

       /// <summary>
       ///CreateDate
       /// </summary>
       [Display(Name ="CreateDate")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? CreateDate { get; set; }

       /// <summary>
       ///Approvaler
       /// </summary>
       [Display(Name ="Approvaler")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string Approvaler1 { get; set; }

       /// <summary>
       ///ApprovalerDepartment
       /// </summary>
       [Display(Name ="ApprovalerDepartment")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string ApprovalerDepartment1 { get; set; }

       /// <summary>
       ///ApprovalDate
       /// </summary>
       [Display(Name ="ApprovalDate")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? ApprovalDate1 { get; set; }

       /// <summary>
       ///Approvaler
       /// </summary>
       [Display(Name ="Approvaler")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string Approvaler2 { get; set; }

       /// <summary>
       ///ApprovalerDepartment
       /// </summary>
       [Display(Name ="ApprovalerDepartment")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string ApprovalerDepartment2 { get; set; }

       /// <summary>
       ///ApprovalDate
       /// </summary>
       [Display(Name ="ApprovalDate")]
       [Column(TypeName="datetime")]
       [Editable(true)]
       public DateTime? ApprovalDate2 { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="PriceTypeID")]
       [Column(TypeName="int")]
       public int? PriceTypeID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="TotalSeriesDemand")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       public decimal? TotalSeriesDemand { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="TotalSeriesCosts")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       public decimal? TotalSeriesCosts { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="TotalServiceDemand")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       public decimal? TotalServiceDemand { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="TotalServiceCosts")]
       [DisplayFormat(DataFormatString="10,2")]
       [Column(TypeName="decimal")]
       public decimal? TotalServiceCosts { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="State")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string State { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Approvaler3")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string Approvaler3 { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ApprovalerDepartment3")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string ApprovalerDepartment3 { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ApprovalDate3")]
       [Column(TypeName="datetime")]
       public DateTime? ApprovalDate3 { get; set; }

       
    }


    public class FYYearTotal
    {
        public decimal Total { get; set; }
        public string FYYear { get; set; }
    }
}