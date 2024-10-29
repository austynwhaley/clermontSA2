public class RandomUserReturn
{
    public required Results[] Results { get; set; }
}

public class Results
{
    public required Name Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required Location Location { get; set; }
    public required Picture Picture { get; set; }
}

public class Name
{
    public required string First { get; set; }
    public required string Last { get; set; }
}

public class Location
{
    public required string City { get; set; }
    public required string Country { get; set; }
}

public class Picture
{
    public required string Large { get; set; }
}
