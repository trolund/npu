namespace NPU.Data.Models;

public class Score: BaseItem
{
    public required string NpuId { get; set; }
    public required int Creativity { get; set; }
    public required int Uniqueness { get; set; }
}