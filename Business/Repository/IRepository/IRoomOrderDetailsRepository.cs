using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IRoomOrderDetailsRepository
    {
        public Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details);

        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(int Id);

        public Task<RoomOrderDetailsDTO> GetRoomOrderDetail(int roomOrderId);

        public Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetails();

        public Task<bool> UpdateRoomOrderStatus(int roomOrderId, string status);

        public Task<bool> IsRoomBooked(int RoomId, DateTime checkInDate, DateTime checkOutDate);
    }
}
