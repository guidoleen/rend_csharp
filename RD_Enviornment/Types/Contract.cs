using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_Enviornment.Types
{
	internal class Contract
	{
        public string _Id { get; set; } = String.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string ContractNummer { get; set; } = String.Empty;
        public SoortContract SoortContract { get; set; } = new();
    }

    enum SoortContract
    {
        Zzp = 1,
        Tao = 2,
        Inhuur = 3,
    }
}
