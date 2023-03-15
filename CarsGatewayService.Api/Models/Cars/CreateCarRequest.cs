namespace Cars.Api.Models.Cars;

public class CreateCarRequest
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Price { get; set; }
}