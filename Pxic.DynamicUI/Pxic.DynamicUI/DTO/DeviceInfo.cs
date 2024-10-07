using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pxic.DynamicUI.DTO
{
    public class DeviceInfo
    {
        public required string Name { get; set; }
        public required byte[] Image { get; set; }
        public required string Vendor { get; set; }
        public required Version FwVersion { get; set; }
    }
}
