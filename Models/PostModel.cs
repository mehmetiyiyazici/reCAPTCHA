namespace DenemePostMan.Models;

public class Post
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public List<string>? Tags { get; set; }
    public int? Views { get; set; }
    public int? UserId { get; set; }
}


public class PostsList
{
    public List<Post> Posts { get; set; }
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}
public class CaptchaResponse
{
    public bool Success { get; set; }
    public double Score { get; set; }
}