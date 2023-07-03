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
    [Entity(TableCnName = "ProjectName",TableName = "LTB_ProjectName")]
    public partial class LTB_ProjectName:BaseEntity
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
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       [Editable(true)]
       public string ProjectName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProductGroup")]
       [MaxLength(100)]
       [Column(TypeName="varchar(100)")]
       public string ProductGroup { get; set; }

       
    }

    //下面两个class  别被代码生成器覆盖了
    public class ProjectNameOption
    {
        public int value { get; set; }
        public string label { get; set; }
        public List<ProjectNameOptionChildren> children { get; set; }
    }

    public class ProjectNameOptionChildren
    {
        public int value { get; set; }
        public string label { get; set; }
    }
}