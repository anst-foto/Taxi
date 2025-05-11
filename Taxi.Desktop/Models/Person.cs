using System.Collections.Generic;

namespace Taxi.Desktop.Models;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Car> Cars { get; set; }
}