using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("users")]
    public partial class users
    {
        public users()
        {
        }

        /// <summary>
        /// Desc:
        /// Default:nextval('users_id_seq'::regclass)
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Desc:密码
        /// Default:
        /// Nullable:False
        /// </summary>
        public string pwd { get; set; }

        /// <summary>
        /// Desc:姓名
        /// Default:
        /// Nullable:False
        /// </summary>
        public string peoplename { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public short sex { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public int card_type { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public string card_no { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public DateTime birthday { get; set; }

        /// <summary>
        /// Desc:手机号
        /// Default:
        /// Nullable:True
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// Desc:头像
        /// Default:
        /// Nullable:True
        /// </summary>
        public int headimg_id { get; set; }

        /// <summary>
        /// Desc:扩展字段
        /// Default:
        /// Nullable:True
        /// </summary>
        public string meta { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        public int create_organize_id { get; set; }

        /// <summary>
        /// Desc:创建机构名称
        /// Default:
        /// Nullable:False
        /// </summary>
        public string create_organize_title { get; set; }

        /// <summary>
        /// Desc:创建人id
        /// Default:
        /// Nullable:False
        /// </summary>
        public int create_user_id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        public string create_peoplename { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:False
        /// </summary>
        public DateTime create_on { get; set; }

        /// <summary>
        /// Desc:修改机构编号
        /// Default:
        /// Nullable:True
        /// </summary>
        public int modify_organize_id { get; set; }

        /// <summary>
        /// Desc:修改机构名称
        /// Default:
        /// Nullable:True
        /// </summary>
        public string modify_organize_title { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public int modify_user_id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public string modify_peoplename { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public DateTime modify_on { get; set; }

        /// <summary>
        /// Desc:状态 字典 enable_or_not
        /// Default:
        /// Nullable:True
        /// </summary>
        public short status { get; set; }
    }
}