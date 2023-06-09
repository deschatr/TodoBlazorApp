
namespace TodoItemManagement;

public class TodoItemService : ITodoItemService
{
    private readonly HttpClient httpClient;

    // defines the end point for the API
    // base address is defined with the AddHttpClient call
    // calls will go to baseaddress + endpoint and baseaddress + endpoint + "/{id}"
    private const string endPoint = "api/TodoItems";

    // constructor used by AddHttpClient
    public TodoItemService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    // gets the list of items, returns an empty array if the call has failed
    public async Task<IEnumerable<TodoItem>> GetTodoItems()
    {
        TodoItem[]? results = null;
        try
        {
            results = await httpClient.GetFromJsonAsync<TodoItem[]>(endPoint);
        }
        catch (HttpRequestException e)
        {
            System.Console.WriteLine("!!! GetTodoItems EXCEPTION !!! " + e.GetType().ToString() + ": " + e.Message);
        }
        if (results == null) results = Array.Empty<TodoItem>();
        return results;
    }

    // adds an item and returns the list of items
    // the list of items is fetched from the API, so that the synchronisation is garanteed
    public async Task<bool> AddTodoItem( TodoItem todoItem )
    {
        bool isSuccessStatusCode = false;
        try
        {
            var response = await httpClient.PostAsJsonAsync<TodoItem>(endPoint, todoItem);
            isSuccessStatusCode = response.IsSuccessStatusCode;
        }
        catch (HttpRequestException e)
        {
            System.Console.WriteLine("!!! AddTodoItems EXCEPTION !!! " + e.GetType().ToString() + ": " + e.Message);
        }
        return isSuccessStatusCode;
    }

    // deletes an item, and returns the list of items
    // the list of items is fetched from the API, so that the synchronisation is garanteed
    public async Task<bool> DeleteTodoItem( TodoItem todoItem )
    {
        bool isSuccessStatusCode = false;
        try
        {
            var response = await httpClient.DeleteAsync(endPoint + "/" + todoItem.Id);
            isSuccessStatusCode = response.IsSuccessStatusCode;

        }
        catch (HttpRequestException e)
        {
            System.Console.WriteLine("!!! DeleteTodoItems EXCEPTION !!! " + e.GetType().ToString() + ": " + e.Message);
        }
        return isSuccessStatusCode;
    }

    // replaces an item, and returns the list of items
    // the list of items is fetched from the API, so that the synchronisation is garanteed
    public async Task<bool> SetTodoItem( TodoItem todoItem )
    {
        bool isSuccessStatusCode = false;
        try
        {
            var response = await httpClient.PutAsJsonAsync(endPoint + "/" + todoItem.Id, todoItem);
            isSuccessStatusCode = response.IsSuccessStatusCode;
        }
        catch (HttpRequestException e)
        {
            System.Console.WriteLine("!!! PutTodoItems EXCEPTION !!! " + e.GetType().ToString() + ": " + e.Message);
        }
        return isSuccessStatusCode;
    }
}