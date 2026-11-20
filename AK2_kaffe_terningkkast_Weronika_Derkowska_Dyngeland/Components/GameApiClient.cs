// GameApiClient.cs - the GET call

namespace BlazorApp1.Components;


public class GameApiClient (HttpClient http)
{git ststus
    // GET
    public async Task<List<BoardGame>?> GetBoardGamesAsync() 
        => await http.GetFromJsonAsync<List<BoardGame>>("api/games");

    // POST
    public async Task<BoardGame>? AddAsync(BoardGame b)
    {
        var response = await http.PostAsJsonAsync("api/games", b);
        return await response.Content.ReadFromJsonAsync<BoardGame>();
    }
        
    // PUT
    public async Task UpdateAsync(BoardGame b)
        => await http.PutAsJsonAsync($"api/games/{b.Id}", b);

    // DELETE
    public async Task DeleteAsync(BoardGame b)
        => await http.DeleteAsync($"api/games/{b.Id}");
}

