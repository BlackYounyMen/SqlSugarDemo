using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("system_log")]
    public partial class system_log
    {
           public system_log(){


           }
           /// <summary>
           /// Desc:
           /// Default:nextval('system_log_id_seq'::regclass)
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:模块
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string module {get;set;}

           /// <summary>
           /// Desc:操作
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string operation {get;set;}

           /// <summary>
           /// Desc:日志内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string log_info {get;set;}

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

    }
}
