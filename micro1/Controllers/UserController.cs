using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IDataService _dataService;

    public UserController(AppDbContext db, IDataService dataService)
    {
        _db = db;
        _dataService = dataService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _dataService.ProcessUserData(input);
        
        return Ok(new { 
            message = "Data processed successfully", 
            data = result 
        });
    }
}