using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMasterDTO
{
    public class TarefasDTO
    {
        public int IdTarefa { get; set; }
        public string TituloTarefa { get; set; }
        public string DescricaoTarefa { get; set; }
        public string StatusTarefa { get; set; }

        //Perguntar sobre as datas
        public int UsuarioTp { get; set; }
    }
}
