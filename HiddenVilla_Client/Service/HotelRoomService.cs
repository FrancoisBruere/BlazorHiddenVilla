using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class HotelRoomService : IHotelRoomService
    {

        private readonly HttpClient _client;

        public HotelRoomService(HttpClient client)
        {
            _client = client;
        }

        public async Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate)
        {
            var responce = await _client.GetAsync($"api/HotelRoom/{roomId}?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();
                var room = JsonConvert.DeserializeObject<HotelRoomDTO>(content);
                return room;
            }
            else
            {
                var content = await responce.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
            
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate)
        {
            var responce = await _client.GetAsync($"api/HotelRoom?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            var content = await responce.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDTO>>(content);
            

            return rooms;
        }
    }
}
