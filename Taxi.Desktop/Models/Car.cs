using System;

namespace Taxi.Desktop.Models;

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public CarStatus Status { get; set; }

    public int DriverId { get; set; }
    public Person Driver { get; set; }
}