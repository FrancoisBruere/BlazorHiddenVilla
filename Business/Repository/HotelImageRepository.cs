﻿using AutoMapper;
using Business.Repository.IRepository;
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
    public class HotelImageRepository : IHotelImageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelImageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO)
        {
            var image = _mapper.Map<HotelRoomImageDTO, HotelRoomImage>(imageDTO);
            await _db.HotelRoomImages.AddAsync(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
        {
            var image = await _db.HotelRoomImages.FindAsync(imageId);

            if (image != null)
            {
                _db.HotelRoomImages.Remove(image);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl)
        {
            var image = await _db.HotelRoomImages.FirstOrDefaultAsync(x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());

            if (image != null)
            {
                _db.HotelRoomImages.Remove(image);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            var imageList = await _db.HotelRoomImages.Where(x => x.RoomId==roomId).ToListAsync();

            if (imageList.Count != 0)
            {
                _db.HotelRoomImages.RemoveRange(imageList);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId)
        {
            return _mapper.Map<IEnumerable<HotelRoomImage>,IEnumerable<HotelRoomImageDTO>>(
            await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync());

        }
    }
}
