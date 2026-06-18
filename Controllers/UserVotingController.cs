using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Cryptography;
using System.Net.Security;
using VoteMaster.Data;
using VoteMaster.Models;

namespace VoteMaster.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserVotingController : ControllerBase 
{


    private readonly AppDbContext _db;
    private readonly ILogger<UserController> _logger; 
    private readonly IConfiguration _config;

    public UserVotingController(AppDbContext db, ILogger<UserController> logger, IConfiguration config)  
    {
        _db = db;
        _logger = logger;
        _config = config;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVoting(int id)
    {
        var endVoting = _db.Votings.Single(x => x.Id == id);

        if(endVoting == null)
        {
            return BadRequest("No voting with that id");
        }

        return Ok(endVoting);
    }

    [Authorize]
    [HttpPost("vote")]
    public async Task<IActionResult> Vote([FromBody] DTOVoteRequest request)
    {
        var Voting = _db.Votings.Single(x => x.Id == request.id);

        if(Voting.AvieableTo < DateTime.UtcNow)
        {
            return BadRequest("Voting has ended");
        }
        var voiter = _db.Users.Single(x => x.Name == User.Identity.Name);
        if(Voting.VoitersId.Contains(voiter.Id))
        {
            return BadRequest("You already voted");
        }

        Voting.Variants[request.option].VoitersNumber++;
        Voting.VoitersId.Add(voiter.Id);
        await _db.SaveChangesAsync();

        return Ok();
    }

    [Authorize]
    [HttpPost("deleteVote")]
    public async Task<IActionResult> deleteVote([FromBody] DTOVoteRequest request)
    {
        var Voting = _db.Votings.Single(x => x.Id == request.id);

        if(Voting.AvieableTo < DateTime.UtcNow)
        {
            return BadRequest("Voting has ended");
        }
        var voiter = _db.Users.Single(x => x.Name == User.Identity.Name);

        Voting.Variants[request.option].VoitersNumber--;
        Voting.VoitersId.Remove(voiter.Id);
        await _db.SaveChangesAsync();

        return Ok();
    }
}