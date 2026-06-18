using Microsoft.EntityFrameworkCore;

namespace VoteMaster.Data;

public class DTOVoting
{
    public string Title { get; set; }
    public List<string> Variants { get; set; }
    public DateTime AvieableTo { get; set; }
    public bool isAnonAllowed {get; set; }
}