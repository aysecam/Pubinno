namespace Pubinno.Domain.Entities
{
    public class PourEvent : BaseEntity
    {
        public Guid EventId { get; set; }
        public string? DeviceId { get; set; } 
        public string? LocationId { get; set; }
        public string? ProductId { get; set; } 
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int VolumeMl { get; set; }
    }
}
