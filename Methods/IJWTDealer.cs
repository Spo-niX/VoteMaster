using VoteMaster.Models;

public interface IJWTDealer
{
    public string GenerateJWT(User user); 
}