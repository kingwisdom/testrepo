using ChurchMemberApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChurchMemberApp.DB
{
    
    public class DB
    {
        SQLiteAsyncConnection database;
        public DB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<DownloadedMedia>().Wait();
            //database.CreateTableAsync<DBFeed>().Wait();
        }

        public Task<List<DownloadedMedia>> GetDownloadedAudio()
        {
            var Media = database.Table<DownloadedMedia>().ToListAsync();
            return Media;
        }

        public Task<int> AddupdatedDownloadedMedia(DownloadedMedia UpdatedMedia)
        {
            var audio = database.InsertAsync(UpdatedMedia);
            return audio;
        }

        public Task<int> DeleteDownoadedMedia(DownloadedMedia Audio)
        {
            var audio = database.DeleteAsync(Audio);
            return audio;
        }

      

        //public bool Refresh()
        //{
        //    database.DropTableAsync<MediaModel>();

        //    database.CreateTableAsync<MediaModel>();

        //    return true;
        //}
    }
}
