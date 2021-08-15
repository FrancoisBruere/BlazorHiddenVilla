using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter room name")]
        public string Name { get; set; }
        [Range(1, 7, ErrorMessage = "Please enter occupancy between 1 and 7")]
        public int Occupancy { get; set; }
        [Range(1, 3000, ErrorMessage = "Regular rate must be between 1 and 3000")]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }
    }
}
