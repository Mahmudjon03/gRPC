using ClientHTML;
using Grpc.Net.Client;

namespace ClientMvc.Services;

public class TodoService
{
   
    private ToDoServiceProto.ToDoServiceProtoClient _toDoServiceProto;
    public TodoService(GrpcChannel channel)
    {
       
        _toDoServiceProto = new ToDoServiceProto.ToDoServiceProtoClient(channel);
    }
    
    public async Task<AllToDo> GetAllToDo() 
    {
        return await _toDoServiceProto.GetAllToDoAsync(new EmptyRequest());
    }

    public async Task<ToDoProto> CreateToDo(AddToDo toDo)
    {
       var res= await _toDoServiceProto.CreateToDoAsync(toDo);

       return res;
    }
    public async Task<ToDoProto> UpdateToDo(ToDoProto toDo)
    {
        var res= await _toDoServiceProto.UpdateToDoAsync(toDo);

        return res;
    }
    public async Task<BooleanRequest> Delete(IdRequest id)
    {
        var res= await _toDoServiceProto.DeleteToDoAsync(id);

        return res;
    }
    public async Task<ToDoProto> GetById(IdRequest  id)
    {
        
        var res= await _toDoServiceProto.GetByIdToDoAsync(id);
        
        return res;
    }
    
}