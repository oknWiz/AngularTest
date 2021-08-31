using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Sysuser
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
