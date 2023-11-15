namespace Tugas9.Data;
using Models.Dto;
using Microsoft.AspNetCore.Identity;

    public static class UserStore
    {
    public static List<UserDTO> userList = new List<UserDTO>
        {
            new UserDTO {Id =  1, Name = "Dimas", Password = "123"},
            new UserDTO {Id = 2, Name = "Kenang", Password = "321"}
        };
    }

