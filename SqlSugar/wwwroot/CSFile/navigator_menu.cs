using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("navigator_menu")]
    public partial class navigator_menu
    {
           public navigator_menu(){


           }
           /// <summary>
           /// Desc:
           /// Default:nextval('navigator_menu_id_seq'::regclass)
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:菜单类型：1 目录 2 菜单
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int type_id {get;set;}

           /// <summary>
           /// Desc:是否打开;目录用 1打开 其他不打开
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? if_open {get;set;}

           /// <summary>
           /// Desc:父编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int parent_id {get;set;}

           /// <summary>
           /// Desc:排序字符串
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string sort_str {get;set;}

           /// <summary>
           /// Desc:默认图标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string icon_default {get;set;}

           /// <summary>
           /// Desc:新图标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string icon_new {get;set;}

           /// <summary>
           /// Desc:打开图标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string icon_open {get;set;}

           /// <summary>
           /// Desc:名称(标题)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string title {get;set;}

           /// <summary>
           /// Desc:链接地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string link_page {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string description {get;set;}

           /// <summary>
           /// Desc:目标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string target {get;set;}

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
