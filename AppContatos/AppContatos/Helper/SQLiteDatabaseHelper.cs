using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppContatos.Model;
using SQLite;

namespace AppContatos.Helper
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;  

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Contato>().Wait();
        }

        public Task<int> Insert(Contato c)
        {
            return _conn.InsertAsync(c);
        }

        public Task<List<Contato>>Update(Contato c)
        {
            string sql = "UPDATE Contato SET Nome=?, Sobrenome=?, Numero=?, Email=? WHERE id=? ";

            return _conn.QueryAsync<Contato>(sql, c.Nome, c.Sobrenome, c.Numero, c.Email, c.Id);
        }
        

        public Task<List<Contato>> GetAll()
        {
            return _conn.Table<Contato>().ToListAsync();
        }

        public  Task<int> Delete(int id)
        {
            return _conn.Table<Contato>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Contato>> Search(string q)
        {
            string sql = "SELECT * FROM Contato WHERE nome LIKE '%" + q + "%'";

            return _conn.QueryAsync<Contato>(sql);
        }

       
    }
}
