using Grpc.Core;
using GRPSExample.Data;
using GRPSExample.Entities;
using Microsoft.EntityFrameworkCore;

namespace GRPSExample.Services;

public class ToDoService:ToDoServiceProto.ToDoServiceProtoBase
{
    private readonly DataContext _context;

    public ToDoService(DataContext context)
    {
        _context = context;
    }

    public override async Task<AllToDo> GetAllToDo(EmptyRequest noy, ServerCallContext callContext)
    {
        var todo = await _context.ToDoS.Select(x => new ToDoProto()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description
        }).ToListAsync();
        
        AllToDo allToDo = new AllToDo();
        allToDo.ToDoProto.AddRange(todo);
        return await Task.FromResult(allToDo);
    }

    public override async Task<ToDoProto> GetByIdToDo(IdRequest id,ServerCallContext call)
    {
        var todo = await _context.ToDoS.FindAsync(id);
        if (todo == null) throw new RpcException(new Status(StatusCode.NotFound, "ToDo not found!"));
       
        return await Task.FromResult(new ToDoProto()
        {
            Id=todo.Id,
            Title = todo.Title,
            Description = todo.Description
        }); 
    }

    public override async Task<ToDoProto> CreateToDo(AddToDo addToDo,ServerCallContext call)
    {
        var newToDo = new ToDo()
        {
            Title = addToDo.Title,
            Description = addToDo.Description
        };
        await _context.ToDoS.AddAsync(newToDo);
        await _context.SaveChangesAsync();
        return await Task.FromResult(new ToDoProto()
        {
            Id=newToDo.Id,
            Title = addToDo.Title
            , Description = newToDo.Description
        });
    }

    public override async Task<ToDoProto> UpdateToDo(ToDoProto model,ServerCallContext call)
    {
        var todo = await _context.ToDoS.FindAsync(model.Id);
       
        if (todo == null) throw new RpcException(new Status(StatusCode.NotFound, "ToDo not found"));
        
        todo.Title = model.Title;
        
        todo.Description = model.Description;
        
        await _context.SaveChangesAsync();
        
        return await Task.FromResult(model);
    }

    public override async Task<BooleanRequest> DeleteToDo(IdRequest id,ServerCallContext call)
    {

        var todo = await _context.ToDoS.FindAsync(id.Id);
        if (todo == null)
            return new BooleanRequest()
            {
                Boolean = false
            };
        _context.ToDoS.Remove(todo!);
        
        await _context.SaveChangesAsync();

        return await Task.FromResult(new BooleanRequest()
        {
            Boolean = true
        });
        
    }
    
}