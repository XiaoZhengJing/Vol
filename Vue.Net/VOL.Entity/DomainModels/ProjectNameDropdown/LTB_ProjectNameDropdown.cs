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
    [Entity(TableCnName = "ProjectNameDropdown",TableName = "LTB_ProjectNameDropdown")]
    public partial class LTB_ProjectNameDropdown:BaseEntity
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
       ///
       /// </summary>
       [Display(Name ="ProjectName")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string ProjectName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="SCMProjectName")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       public string SCMProjectName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="rowID")]
       [Column(TypeName="int")]
       public int? rowID { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProjectGroup")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       public string ProjectGroup { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProductGroup")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       public string ProductGroup { get; set; }

       
    }

    public class ProjectNameDropdown
    {
        public int rowID { get; set; }
        public string ProjectName { get; set; }
    }
}