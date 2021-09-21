using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class RoomOrderDetailService : IRoomOrderDetailService
    {
        private readonly HttpClient _client;

        public RoomOrderDetailService(HttpClient client)
        {
            _client = client;
        }

        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details)
        {
            throw new NotImplementedException();
        }

        public async Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details)
        {

            details.UserId = "dummy user";
            var content = JsonConvert.SerializeObject(details);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync("api/roomorder/create", bodyContent);

           // string res = responce.Content.ReadAsStringAsync().Result;

            if (responce.IsSuccessStatusCode)
            {

                var contentTemp = await responce.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RoomOrderDetailsDTO>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await responce.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
