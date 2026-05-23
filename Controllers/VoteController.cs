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
    }
}