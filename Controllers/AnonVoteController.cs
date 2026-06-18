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
using OfficeOpenXml;

namespace VoteMaster.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AnonVoteController : ControllerBase 
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserController> _logger; 
    private readonly IConfiguration _config;

    public AnonVoteController(AppDbContext db, ILogger<UserController> logger, IConfiguration config)  
    {
        _db = db;
        _logger = logger;
        _config = config;
    }

    [HttpPost("vote")]
    public async Task<IActionResult> Vote([FromBody] DTOVoteRequest request)
    {
        var Voting = _db.Votings.Single(x => x.Id == request.id);

        if (!Voting.isAnonAllowed)
        {
            return Unauthorized("Anon voiters aren't allowed");
        }

        if(Voting.AvieableTo < DateTime.UtcNow)
        {
            return BadRequest("Voting has ended");
        }

        var voiterip = HttpContext.Connection.RemoteIpAddress.ToString();

        if(_db.votedIps.Where(x => x.VotingId == request.id).Where(x => x.Ip == voiterip) != null)
        {
            return BadRequest("you already voted");
        }


        Voting.Variants[request.option].VoitersNumber++;

        _db.votedIps.Add(new VotedIp{Ip = voiterip,
        CanVoteAt = DateTime.UtcNow.AddMinutes(10),
        VotingId = request.id});

        await _db.SaveChangesAsync();

        return Ok();
    }
}