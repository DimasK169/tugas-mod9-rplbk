namespace Tugas9.Controllers;
using Models.Dto;
using Microsoft.AspNetCore.Mvc;
[Route("api/UserAPI")]
[ApiController]
public class UserAPIController : ControllerBase
{
    [HttpGet]
    public IEnumerable<UserDTO> GetUsers()
    {
        return new List<UserDTO> {
            new UserDTO{Id=1, Name="Dimas", Password="123"},
            new UserDTO{Id=2,Name="Kenang", Password="123"}
            };
    }
}
    

