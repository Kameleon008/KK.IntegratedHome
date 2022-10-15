namespace KK.IH.Api.DatabaseApi.Controllers
{
    using KK.IH.Api.DatabaseApi.CQRS.DeviceCQRS.GetAll;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private IMediator mediator;

        public DeviceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<GetAllDeviceResponse> GetAll([FromQuery] GetAllDeviceRequest request)
        {
            var response = await this.mediator.Send(request);
            return response;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
