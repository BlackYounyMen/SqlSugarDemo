using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("frequent_option_loader")]
    public partial class frequent_option_loader
    {
           public frequent_option_loader(){


           }
           /// <summary>
           /// Desc:
           /// Default:nextval('frequent_option_loader_id_seq'::regclass)
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:关联机构表
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? organization_id {get;set;}

           /// <summary>
           /// Desc:获取数据的sql语句
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string vw_sql {get;set;}

           /// <summary>
           /// Desc:键字段名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string key_field {get;set;}

           /// <summary>
           /// Desc:显示字段名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string val_field {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string description {get;set;}

           /// <summary>
           /// Desc:标题
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string title {get;set;}

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
