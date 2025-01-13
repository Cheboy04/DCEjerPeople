using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCEjerPeople.Models;
using SQLite;

namespace DCEjerPeople
{
    public class DCPersonRepository
    {
        string _dbPath;
        private SQLiteAsyncConnection conn;

        public string StatusMessage { get; set; }


        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);

            await conn.CreateTableAsync<DCPerson>();
        }

        public DCPersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                // TODO: Call Init()
                await Init();
                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                // TODO: Insert the new person into the database
                result = await conn.InsertAsync(new DCPerson { Name = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public async Task<List<DCPerson>> GetAllPeople()
        {
            // TODO: Init then retrieve a list of Person objects from the database into a list
            try
            {
                await Init();
                return await conn.Table<DCPerson>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<DCPerson>();
        }

    }
}
