using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Watch.Models.Users
{
    public class User : Model<Guid>, IUser
    {
        [Required]
        public Credentials                Credentials { get; set; }
        public List<Models.Watches.Watch> Watches     { get; set; }        
    }
}
