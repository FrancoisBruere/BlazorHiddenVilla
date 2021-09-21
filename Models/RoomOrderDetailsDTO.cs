using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RoomOrderDetailsDTO
    {

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // will be string as useid is a guid

        [Required]
        public string StripeSessionId { get; set; } // for stripe payment

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public DateTime ActualCheckInDate { get; set; }

        public DateTime ActualCheckOutDate { get; set; }

        [Required]
        public double TotalCost { get; set; }

        [Required]
        public int RoomId { get; set; }
        public bool IsPaymentSuccessful { get; set; } = false;

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public HotelRoomDTO HotelRoomDTO { get; set; }
        public string Status { get; set; }

    }
}
