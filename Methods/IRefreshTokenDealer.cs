using Microsoft.AspNetCore.Http.HttpResults;
using VoteMaster.Models;

public interface IRefreshTokenDealer
{
    public string GenerateRefreshToken(User user, DTORefreshToken tk); 
    public string GenerateRefreshToken(User user); 
}