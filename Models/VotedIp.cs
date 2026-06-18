namespace VoteMaster.Models;

public class VotedIp
{
    public string Ip { get; set; }
    public DateTime CanVoteAt { get; set; }
    public int VotingId { get; set; }
}