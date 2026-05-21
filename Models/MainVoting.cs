using Microsoft.EntityFrameworkCore;

namespace VoteMaster.Data;

public class MainVoting
{
    public string Title;
    public List<MainVotingVariant> Variants;
    public DateTime AvieableTo;
}