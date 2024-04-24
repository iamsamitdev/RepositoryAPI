using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        var createdUser = await _userRepository.CreateAsync(user);
        return CreatedAtAction(
            nameof(Get), new { id = createdUser.Id }, createdUser
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] User user)
    {
        if (id != user.Id)
            return BadRequest();

        await _userRepository.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userRepository.DeleteAsync(id);
        return NoContent();
    }
}