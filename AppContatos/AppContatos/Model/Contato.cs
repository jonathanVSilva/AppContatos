using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppContatos.Model
{
    public class Contato
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nome { get; set; }

        public double Numero { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }
    }
}
