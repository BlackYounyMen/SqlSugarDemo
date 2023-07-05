using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("login_log")]
    public partial class login_log
    {
           public login_log(){


           }
           /// <summary>
           /// Desc:
           /// Default:nextval('login_log_id_seq'::regclass)
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:返回状态值
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string resstatus {get;set;}

           /// <summary>
           /// Desc:机构id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? organization_id {get;set;}

           /// <summary>
           /// Desc:机构标题
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string organization_title {get;set;}

           /// <summary>
           /// Desc:用户id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int user_id {get;set;}

           /// <summary>
           /// Desc:用户账号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string username {get;set;}

           /// <summary>
           /// Desc:用户姓名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string peoplename {get;set;}

           /// <summary>
           /// Desc:登录方式
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int login_type {get;set;}

           /// <summary>
           /// Desc:登录ip
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string login_ip {get;set;}

           /// <summary>
           /// Desc:登录时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime login_time {get;set;}

           /// <summary>
           /// Desc:登出时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? logout_time {get;set;}

           /// <summary>
           /// Desc:token 令牌
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string token {get;set;}

    }
}
