//using ChurchMemberApp.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace ChurchMemberApp.DB
//{
//    public class DataService
//    {
//        private readonly AppDbContext context;
//        public DataService()
//        {
//            context = new AppDbContext();
//        }

//        public async Task<bool> AddMediaAsync(DownloadedMedia model)
//        {
//            try
//            {
//                var result = await context.Downloads.AddAsync(model);
//                await context.SaveChangesAsync();

//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        public async Task<bool> DeleteMediaAsync(DownloadedMedia model)
//        {
//            try
//            {
//                var result = context.Downloads.Remove(model);
//                await context.SaveChangesAsync();

//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }


//        public async Task<IEnumerable<DownloadedMedia>> GetAllMediaAsync()
//        {
//            try
//            {
//                var result = await context.Downloads.ToListAsync();
//                return result;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public async Task<DownloadedMedia> GetMediaAsync(int Id)
//        {
//            try
//            {
//                var result = await context.Downloads.FindAsync(Id);
//                return result;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }
//    }
//}
