public class RandomUserReturn
{
    public required List<Result> Results { get; set; }
}

public class Result
{
    public required Name Name { get; set; }
    public required Location Location { get; set; }
    public required string Email { get; set; }
    public required Picture Picture { get; set; }
}

public class Name
{
    public required string Title { get; set; }
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
    public required string Medium { get; set; }
    public required string Thumbnail { get; set; }
}
