using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("role")]
    public partial class role
    {
           public role(){


           }
           /// <summary>
           /// Desc:
           /// Default:nextval('role_id_seq'::regclass)
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:关联机构表(organization)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? organization_id {get;set;}

           /// <summary>
           /// Desc:角色名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string title {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? status {get;set;}

           /// <summary>
           /// Desc:是否拥有继承功能
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? parent_id {get;set;}

           /// <summary>
           /// Desc:扩展字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string meta {get;set;}

           /// <summary>
           /// Desc:创建机构编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int create_organize_id {get;set;}

           /// <summary>
           /// Desc:创建机构名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string create_organize_title {get;set;}

           /// <summary>
           /// Desc:创建人id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int create_user_id {get;set;}

           /// <summary>
           /// Desc:创建人姓名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string create_peoplename {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime create_on {get;set;}

           /// <summary>
           /// Desc:修改机构编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? modify_organize_id {get;set;}

           /// <summary>
           /// Desc:修改机构名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string modify_organize_title {get;set;}

           /// <summary>
           /// Desc:修改人id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? modify_user_id {get;set;}

           /// <summary>
           /// Desc:修改人姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string modify_peoplename {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? modify_on {get;set;}

    }
}
