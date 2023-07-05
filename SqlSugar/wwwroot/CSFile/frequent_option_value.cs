using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("frequent_option_value")]
    public partial class frequent_option_value
    {
           public frequent_option_value(){


           }
           /// <summary>
           /// Desc:
           /// Default:nextval('frequent_option_value_id_seq'::regclass)
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:父表id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int option_id {get;set;}

           /// <summary>
           /// Desc:标题
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string title {get;set;}

           /// <summary>
           /// Desc:字段值
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int store_value {get;set;}

           /// <summary>
           /// Desc:排序字段
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string sort_str {get;set;}

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
