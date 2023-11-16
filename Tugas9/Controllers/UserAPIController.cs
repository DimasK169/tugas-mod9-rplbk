namespace Tugas9.Controllers;
using Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Tugas9.Data;
using Microsoft.AspNetCore.JsonPatch;

[Route("api/UserAPI")]
[ApiController]
public class UserAPIController : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public ActionResult<IEnumerable<UserDTO>> GetUser()
    {
        return UserStore.userList;
    }
    [HttpGet("{id:int}", Name = "GetUser")]
    public ActionResult<UserDTO> GetUser(int id)
    {
        if (id == 0) return BadRequest();
        var user = UserStore.userList.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    [HttpPost]
    public ActionResult<UserDTO> CreateUser([FromBody] UserDTO userDTO)
    {
        if (UserStore.userList.FirstOrDefault(u => u.Name.ToLower() == userDTO.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "User already exist");
            return BadRequest(ModelState);
        }
        if (userDTO == null)
        {
            return BadRequest(userDTO);
        }
        if (userDTO.Id == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        userDTO.Id = UserStore.userList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        UserStore.userList.Add(userDTO);
        string response = "Sukses menambahkan data user" + "\nId : " + userDTO.Id.ToString() + "\nNama : " + userDTO.Name + "\nPassword : " + userDTO.Password;
        return CreatedAtRoute("GetUser", new { id = userDTO.Id }, response);
    }
    [HttpDelete("{id:int}", Name = "DeleteUser")]
    public IActionResult DeleteUser(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var user = UserStore.userList.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        UserStore.userList.Remove(user);
        return NoContent();
    }
    [HttpPut("{id:int}", Name = "Update User")]
    public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
    {
        if (userDTO == null || id != userDTO.Id)
        {
            return BadRequest();
        }
        var user = UserStore.userList.FirstOrDefault(u => u.Id == id);
        user.Name = userDTO.Name;
        user.Password = userDTO.Password;

        return NoContent();
    }
    [HttpPatch("{id:int}", Name = "UpdatePartialUser")]
    public IActionResult UpdatePartialUser(int id, JsonPatchDocument<UserDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }
        var user = UserStore.userList.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return BadRequest();
        }
        patchDTO.ApplyTo(user, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }
    [HttpPost("/login")]
    public ActionResult<UserDTO> LoginAcc([FromBody] UserDTO userDTO)
    {
        if (userDTO == null)
        {
            return BadRequest("Username/Password Invalid");
        }

        var user = UserStore.userList.FirstOrDefault(u => u.Name == userDTO.Name);
        if (user == null && user.Password != userDTO.Password)
        {
            return NotFound("Username/Password Salah");
        }
        return Ok("Berhasil Login");
    }

}
    

