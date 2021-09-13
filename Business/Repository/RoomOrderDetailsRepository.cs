using AutoMapper;
using Business.Repository.IRepository;
using Common;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoomOrderDetailsRepository : IRoomOrderDetailsRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RoomOrderDetailsRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details)
        {
            try
            {
                details.CheckInDate = details.CheckInDate.Date;
                details.CheckOutDate = details.CheckOutDate.Date;
                var roomOrder = _mapper.Map<RoomOrderDetailsDTO, RoomOrderDetails>(details);
                roomOrder.Status = SD.Status_Pending;
                var result = await _db.RoomOrderDetails.AddAsync(roomOrder);
                await _db.SaveChangesAsync();

                return _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(result.Entity);


            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetails()
        {
            try
            {

                IEnumerable<RoomOrderDetailsDTO> roomOrders = _mapper.Map<IEnumerable<RoomOrderDetails>, IEnumerable<RoomOrderDetailsDTO>>
                    (_db.RoomOrderDetails.Include(u => u.HotelRoom));

                return roomOrders;

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<RoomOrderDetailsDTO> GetRoomOrderDetail(int roomOrderId)
        {
            try
            {

                RoomOrderDetails roomOrder = await _db.RoomOrderDetails
                    .Include(u => u.HotelRoom).ThenInclude(x => x.HotelRoomImages).FirstOrDefaultAsync(u => u.Id == roomOrderId);

                RoomOrderDetailsDTO roomOrderDetailsDTO = _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(roomOrder);

                roomOrderDetailsDTO.HotelRoomDTO.TotalDays = roomOrderDetailsDTO.CheckOutDate.Subtract(roomOrderDetailsDTO.CheckInDate).Days;
               
                
                return roomOrderDetailsDTO;

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int RoomId, DateTime checkInDate, DateTime checkOutDate)
        {
            var status = false;
            var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == RoomId && x.IsPaymentSuccessful &&
            // check if checkin date that user wants does not fall between any dates that is already booked
            (checkInDate<x.CheckOutDate && checkInDate.Date > x.CheckInDate
            // check if checkout date that user wants does not fall between dates that is already booked
            || checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date < x.CheckInDate.Date)).FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                status = true;
            }

            return status;
        }

        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRoomOrderStatus(int roomOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
