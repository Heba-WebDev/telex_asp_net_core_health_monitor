namespace API.Models;

public class Setting
{
    public string Label { get; set; } = "";
    public string Type { get; set; } = "";
    public bool Required { get; set; }
    public string Default { get; set; } = "";

}
