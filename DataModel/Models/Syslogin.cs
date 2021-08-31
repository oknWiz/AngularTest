using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Syslogin
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public bool IsLogin { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
