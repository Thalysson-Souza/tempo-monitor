namespace TempoMonitor.Web.Contracts.Locations;

public class CreateLocationRequest
{
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}