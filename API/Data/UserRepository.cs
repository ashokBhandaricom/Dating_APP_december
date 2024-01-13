using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        public DataContext _context { get; set ; }
        private readonly IMapper _mapper;

        public UserRepository(DataContext context ,IMapper mapper )
        {
            _mapper = mapper;
            _context = context;
            
        }
        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _context.Users
             .Include(p => p.Photos).
             SingleOrDefaultAsync( x => x.UserName == username ) ; 
        }

        public async Task<IEnumerable<AppUser>> getUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos).
            ToListAsync() ; 
        }

        public async Task<AppUser> GetUsersByIdAsync(int id)
        {
           return await _context.Users.FindAsync(id); 
        }

        public async Task<bool> SaveAllAsync()
        {
            int count =  await _context.SaveChangesAsync()  ; 
            if ( count > 0 ) return true; 
            return false ; 
        }

        public void Update(AppUser user)
        {
           _context.Entry(user).State= EntityState.Modified    ;     }

        public async Task<IEnumerable<MembersDto>> GetMembersAsync()
        {
           return await _context.Users
           .ProjectTo<MembersDto>(_mapper.ConfigurationProvider).ToListAsync(); 
        }

        public async Task<MembersDto> getMembersAsync(string username)
        {
            return await _context.Users
            .Where(x=> x.UserName == username)
            .ProjectTo<MembersDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync() ; 
        }
    }
}