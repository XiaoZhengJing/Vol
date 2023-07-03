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
    [Entity(TableCnName = "employee",TableName = "Employee")]
    public partial class Employee:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="UID")]
       [MaxLength(50)]
       [Column(TypeName="uniqueidentifier")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
        public Guid UID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name ="FirstName")]
       [MaxLength(128)]
       [Column(TypeName="nvarchar(128)")]
       [Editable(true)]
       public string FirstName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="LastName")]
       [MaxLength(128)]
       [Column(TypeName="nvarchar(128)")]
       [Editable(true)]
       public string LastName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Department")]
       [MaxLength(128)]
       [Column(TypeName="nvarchar(128)")]
       [Editable(true)]
       public string Department { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Email")]
       [MaxLength(128)]
       [Column(TypeName="nvarchar(128)")]
       [Editable(true)]
       public string Email { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="DisplayName")]
       [MaxLength(128)]
       [Column(TypeName="nvarchar(128)")]
       [Editable(true)]
       public string DisplayName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="TelephoneNumber")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       [Editable(true)]
       public string TelephoneNumber { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Domain")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string Domain { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="MobilePhone")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string MobilePhone { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="GID")]
       [MaxLength(50)]
       [Column(TypeName="varchar(50)")]
       [Editable(true)]
       public string GID { get; set; }

       
    }

    //用于远程搜索
    public class SearchEmployee
    {
        public string value { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
    }
}