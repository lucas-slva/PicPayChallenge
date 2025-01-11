using Microsoft.AspNetCore.Mvc;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;

namespace PicPay.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(UserDto userDto)
    {
        var user = await _userService.CreateUserAsync(userDto);
        
        return Ok(user);
    }
}