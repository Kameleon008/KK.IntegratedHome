namespace KK.IH.Api.DatabaseApi.CQRS.DeviceCQRS.GetAll
{
    using KK.IH.Database.PostgresMigration;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllDeviceHandler : IRequestHandler<GetAllDeviceRequest, GetAllDeviceResponse>
    {
        private readonly DatabaseContext database;
        public GetAllDeviceHandler(DatabaseContext database)
        {
            this.database = database;
        }

        public async Task<GetAllDeviceResponse> Handle(GetAllDeviceRequest request, CancellationToken cancellationToken)
        {
            var devices = await database.Devices.ToListAsync();
            return new GetAllDeviceResponse { Devices = devices };
        }
    }
}
