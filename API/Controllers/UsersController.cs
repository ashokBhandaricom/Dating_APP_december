using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 [Authorize]
    public class UsersController : BaseApiController
    {
        public IUserRepository _userRepository { get; }
        public IMapper _mapper{get; }
        
        public UsersController(IUserRepository userRepository , IMapper mapper )
        {
            _userRepository = userRepository;
            _mapper = mapper ; 
        

        }
[AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers()
        {

     var users =    await _userRepository.GetMembersAsync() ; 
   
     return Ok(users); 
        }
       
        [HttpGet("{username}")]
[AllowAnonymous]
        public async Task<ActionResult<MembersDto>> GetUser(string username)
        {

         return    await _userRepository.getMembersAsync(  username ) ; 
         

        }

     //   [HttpGet("{Id}")]

        // public async Task<ActionResult<AppUser>> GetUsers(int id)
        // {

        //     return await _context.Users.FindAsync(id);

        // }
    
}
}