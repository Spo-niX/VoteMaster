using Microsoft.EntityFrameworkCore;

namespace VoteMaster.Data;

public class MainVotingVariant
{
    public string Title{ get; set; }
    public float VoitersNumber{ get; set; }
}