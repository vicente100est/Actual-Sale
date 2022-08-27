using System;
using System.Collections.Generic;

#nullable disable

namespace WSSale.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string EmailUser { get; set; }
        public string PasswordUser { get; set; }
        public string NameUser { get; set; }
    }
}
