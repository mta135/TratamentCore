using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tratament.Model.Models.EcerereTicketService
{
    public class TicketServiceModel
    {
        public short? Vpres_rf { get; set; }

        public string Vidnp { get; set; }

        public string Vnume { get; set; }

        public string Vprenume { get; set; }

        public string Vcuatm { get; set; }

        public string Vadresa { get; set; }

        public string Vtelefon { get; set; }

        public string Vemail { get; set; }

        public DateTime? VnascutD { get; set; }

        public string Vsex { get; set; }
    }
}
