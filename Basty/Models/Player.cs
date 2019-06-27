
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Basty.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public byte[] Image { get; set; }
        public DateTime BirthDate { get; set; }


        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        
    }
}
