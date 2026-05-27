using Microsoft.EntityFrameworkCore;

namespace VoteMaster.Data;

public class DTOVoteRequest
{
    public int id { get; set; }
    public byte option { get; set; }
}