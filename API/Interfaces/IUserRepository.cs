using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update( AppUser user) ;
        Task<bool> SaveAllAsync() ;
        Task<IEnumerable<AppUser>> getUsersAsync() ;

        Task<AppUser> GetUsersByIdAsync(int id ) ;

        Task<AppUser> GetUserByUserNameAsync(string username) ; 
        Task<IEnumerable<MembersDto>> GetMembersAsync() ;

        Task<MembersDto> getMembersAsync( string username) ; 
    }
} 