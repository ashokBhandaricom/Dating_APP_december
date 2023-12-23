using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
   
    public class AccountController : BaseApiController
    {
        
        private readonly DataContext _context ;
        public ITokenService _tokenService ; 

        public AccountController(DataContext context , ITokenService tokenService) {
           _tokenService = tokenService;
            _context = context; 
        }


   [HttpPost("register") ] // post: api/controller/register 
   public async Task<ActionResult<UserDTO>> Register(RegisterDTO  registerDTO){
    using var hmac = new HMACSHA512() ;
    if(await UserExists(registerDTO.UserName)) return BadRequest("username already exist "); 
    var user = new AppUser{
        UserName = registerDTO.UserName.ToLower()  ,
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)) ,
        PasswordSalt = hmac.Key   
    };
    _context.Users.Add(user); 

    await  _context.SaveChangesAsync(); 
    return new UserDTO{
        UserName = user.UserName,
        Token = _tokenService.CreateToken(user ) 
    }  ; 
   }
[HttpPost("login")]

public async  Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO){
    var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName == loginDTO.UserName) ; 

    if ( user == null) return Unauthorized() ; 

    using var hmac = new HMACSHA512(user.PasswordSalt) ;
    var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password)) ;

     for(int i = 0; i< computedhash.Length; i++){
        if( computedhash[i] != user.PasswordHash[i]  )  return Unauthorized() ; 
     }

 return  new UserDTO{
        UserName = user.UserName,
        Token = _tokenService.CreateToken(user ) 
    }  ; 
}

   private async Task<bool> UserExists( string username){

    return await  _context.Users.AnyAsync(x=> x.UserName == username.ToLower() );
   } 
    }
}