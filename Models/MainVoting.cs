using Microsoft.EntityFrameworkCore;

namespace VoteMaster.Data;

public class MainVoting
{
    public string Title{ get; set; }
    public List<MainVotingVariant> Variants{ get; set; }
    public DateTime AvieableTo{ get; set; }
}