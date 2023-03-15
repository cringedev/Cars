namespace Cars.Api.Entities;

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Price { get; set; }
    public int UserId { get; set; }
}