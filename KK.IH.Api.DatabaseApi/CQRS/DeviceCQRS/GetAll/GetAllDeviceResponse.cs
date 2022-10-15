using KK.IH.Api.DatabaseApi.Models;
using System.Collections.Generic;

namespace KK.IH.Api.DatabaseApi.CQRS.DeviceCQRS.GetAll
{
    public class GetAllDeviceResponse
    {
        public List<Device> Devices { get; set; }
    }
}
