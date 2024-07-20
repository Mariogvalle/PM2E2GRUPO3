using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2E2GRUPO3.Models
{
    public class Ubicaciones
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string video { get; set; }
        public string audio { get; set; }
    }
}
