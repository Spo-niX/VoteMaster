using Microsoft.EntityFrameworkCore;

namespace VoteMaster.Data;

public class MainVoting
{
    public int Id {get; set; }
    public string Title{ get; set; }
    public List<MainVotingVariant> Variants{ get; set; }
    public DateTime AvieableTo{ get; set; }
    public List<int> VoitersId {get; set; }
    public string CreaterName {get; set; }
}