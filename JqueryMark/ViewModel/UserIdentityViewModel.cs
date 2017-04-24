using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JqueryMark.ViewModel
{
    public class UserIdentityViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool IsLocked { get; set; }
    }
}