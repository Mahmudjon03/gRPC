using System.Diagnostics;
using ClientHTML;
using ClientMvc.Models;
using ClientMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientMvc.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly TodoService _todo;
    private readonly ToDoServiceProto.ToDoServiceProtoClient _toDoServiceProto;
    
    public HomeController(ILogger<HomeController> logger,TodoService todo)
    {
        _logger = logger;
        _todo = todo;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var res =await _todo.GetAllToDo();
        
        return View(res);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return  View(new AddToDo());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddToDo todo)
    {
        if (ModelState.IsValid == true)
        {
            var res = await _todo.CreateToDo(todo);
            return RedirectToAction(nameof(Index));
        }
        return View(nameof(Index));
    }
   

   
    
   
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
