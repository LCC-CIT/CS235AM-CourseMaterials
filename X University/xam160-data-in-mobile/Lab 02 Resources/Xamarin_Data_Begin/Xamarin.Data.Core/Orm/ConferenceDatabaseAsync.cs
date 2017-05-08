
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Data.Core.Model;

namespace Xamarin.Data.Core.Orm
{
	
	public class ConferenceDatabaseAsync : SQLiteAsyncConnection
    {	
        //TODO: Step 1 - Setup the Database Path
        //public static string DatabaseFilePath {
        //    get { 
        //        var sqliteFilename = "SessionDB.db3";
		//				
        //        #if __ANDROID__
        //        // Just use whatever directory SpecialFolder.Personal returns
        //        string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
        //        #else
        //        // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
        //        // (they don't want non-user-generated data in Documents)
        //        string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
        //        string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
        //        #endif
		//	
        //        var path = Path.Combine (libraryPath, sqliteFilename);
		//	
        //        return path;	
        //    }
        //}

        public ConferenceDatabaseAsync(String path) : base(path)
		{
			//TODO: Step 4 - Create tables
            //// create the SQLite database tables based on the Model classes
            //var createTableSession = CreateTableAsync<Session> ();
            //var createTableSpeaker = CreateTableAsync<Speaker>();
			//
            //Task.WaitAll(createTableSession, createTableSpeaker);
		}

        #region Sessions
        public async Task<List<Session>> GetSessionsAsync()
        {
			//TODO: Step 5 - Select All Records from Table 
            //return await Table<Session>().ToListAsync();
        }

        public async Task<Session> GetSessionAsync(int id)
        {
			//TODO: Step 6 - Select records from table using Linq
            //return await Table<Session>().Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveSessionAsync(Session item)
        {
			//TODO: Step 7 - Perform an insert or update to the database 
            //if (item.Id <= 0) 
            //    return await InsertAsync(item);
			//
            //await UpdateAsync(item);
            //return item.Id;
        }

	    public async Task SaveSessionsAsync(IEnumerable<Session> sessions)
        {
            await InsertAllAsync(sessions);
        }

        public async Task<int> DeleteSessionAsync(Session session)
        {
			//TODO: Step 8 - Delete a record from the table
            //return await DeleteAsync(session);
        }

		public async Task DeleteSessionsAsync()
		{
			//TODO: Step 9 - Delete all data using SQL commands
            //await this.ExecuteAsync ("DELETE FROM Session;");
		}
        #endregion Sessions

        #region Speakers
        public async Task<List<Speaker>> GetSpeakersAsync()
        {
            return await Table<Speaker>().ToListAsync();
        }

        public async Task<Speaker> GetSpeakerAsync(string name)
        {
            return await Table<Speaker>().Where(s => s.Name == name).FirstOrDefaultAsync();
        }
        public async Task<Speaker> GetSpeakerAsync(int id)
        {
            return await Table<Speaker>().Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveSpeakerAsync(Speaker item)
        {
            if (item.Id == 0) 
                return await InsertAsync(item);

            await UpdateAsync(item);
            return item.Id;
        }

	    public async Task SaveSpeakersAsync(IEnumerable<Speaker> speakers)
        {
            await InsertAllAsync(speakers);
        }

        public async Task DeleteSpeakersAsync()
        {
			await ExecuteAsync ("DELETE FROM Speaker;");
        }

        public async Task<int> DeleteSpeakerAsync(Speaker speaker)
        {
            return await DeleteAsync(speaker);
        }
        #endregion Speakers
	}
}

