using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JqueryMark.ActionModel
{
    /// <summary>
    /// 登入者資訊Action Model
    /// </summary>
    public class LoginActionModel
    {
        /// <summary>
        /// 登入者名稱
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 登入者密碼
        /// </summary>
        [Required]
        public string UserPassword { get; set; }
    }
}