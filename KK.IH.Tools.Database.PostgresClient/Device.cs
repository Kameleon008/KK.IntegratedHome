using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.IH.Tools.Database.PostgresClient
{
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public Guid Guid { get; set; }
    }
}
