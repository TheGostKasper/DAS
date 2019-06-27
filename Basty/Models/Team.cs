using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basty.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Coach { get; set; }
        public byte[] Image { get; set; }
        public DateTime Foundation { get; set; }


        public IList<Player> Players { get; set; }
    }
}