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

public class VoteController : ControllerBase 
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserController> _logger; 
    private readonly IConfiguration _config;

    public VoteController(AppDbContext db, ILogger<UserController> logger, IConfiguration config)  
    {
        _db = db;
        _logger = logger;
        _config = config;
    }

    [Authorize]
    [HttpPost("createVoting")]
    public async Task<IActionResult> CreateVoting([FromBody] DTOVoting vt)
    {
        if (string.IsNullOrEmpty(vt.Title))
        {
            return BadRequest("No Title");
        }
        if (2 < vt.Variants.Count && vt.Variants.Count > 10)
        {
            return BadRequest("Wrong amount of variants");
        }
        if(vt.AvieableTo < DateTime.UtcNow || vt.AvieableTo > DateTime.UtcNow.AddMonths(3))
        {
            return BadRequest("Wrong life time");
        }

        List<MainVotingVariant> endVarints = new List<MainVotingVariant>();
        foreach(var i in vt.Variants)
        {
            var endVariant = new MainVotingVariant
            {
                Title = i,
                VoitersNumber = 0,
            };
            endVarints.Add(endVariant);
        }
        var endVoting = new MainVoting
        {
            Title = vt.Title,
            Variants = endVarints,
            AvieableTo = vt.AvieableTo,
            CreaterName = User.Identity.Name,
            VoitersId = new List<int>() 
        };

        _db.Votings.Add(endVoting);
        _db.SaveChanges();

        _logger.LogInformation("Voting {id} was created by user {name}", endVoting.Id, User.Identity.Name);

        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> CreateVoting(int id)
    {
        if (_db.Votings.Where(x => x.Id == id)
        .Where(x => x.CreaterName == User.Identity.Name) == null)
        {
            return Unauthorized("You'r not creater of this voting");
        }
        
        var VotingToRemove = _db.Votings.Single(x => x.Id == id);
        _db.Votings.Remove(VotingToRemove);
        _db.SaveChanges();

        _logger.LogInformation("Voting {id} was removed by user {name}", VotingToRemove.Id, User.Identity.Name);

        return Ok();
    }
}