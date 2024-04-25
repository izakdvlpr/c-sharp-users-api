namespace UserApi.Bases;

public class Error
{
    public required string Message { get; set; }
    public required string Code { get; set; }
    public required int Status { get; set; }
}