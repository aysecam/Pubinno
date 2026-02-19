namespace Pubinno.Application.Features.Pours.Queries.GetTapSummary
{
    public class TapSummaryResponse
    {
        public string DeviceId { get; set; } = null!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int TotalVolumeMl { get; set; }
        public int TotalPours { get; set; }
        public ProductSummaryDto? TopProduct { get; set; }
        public LocationSummaryDto? TopLocation { get; set; }
        public List<ProductSummaryDto> ByProduct { get; set; } = new();
        public List<LocationSummaryDto> ByLocation { get; set; } = new();
    }

    public class ProductSummaryDto
    {
        public string ProductId { get; set; } = null!;
        public int VolumeMl { get; set; }
        public int Pours { get; set; }
    }

    public class LocationSummaryDto
    {
        public string LocationId { get; set; } = null!;
        public int VolumeMl { get; set; }
        public int Pours { get; set; }
    }
}
