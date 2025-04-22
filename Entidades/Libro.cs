using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Libro
    {
        public int idLibro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public string genero{get; set; }
        public int copias { get; set; }
    }
}
