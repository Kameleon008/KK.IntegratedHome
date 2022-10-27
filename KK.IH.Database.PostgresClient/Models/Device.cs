namespace KK.IH.Database.PostgresClient.Models
{
    using System;

    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public Guid Guid { get; set; }
    }
}
