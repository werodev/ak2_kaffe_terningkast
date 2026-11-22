using BlazorApp1.Components;

namespace BlazorApp1.State;

public class GameNightState
{
    private List<BoardGame> selectedGames = new();
    public IReadOnlyList<BoardGame> SelectedGames => selectedGames.AsReadOnly();
    public int Count => selectedGames.Count;
    public int TotalPlayTime => selectedGames.Sum(g => g.PlayTimeMinutes);

    public void RemoveGame(int gameId)
    {
        bool removed = selectedGames.Remove(selectedGames.FirstOrDefault(g => g.Id == gameId));
        if (removed)
        {
            OnChange?.Invoke();
        }
    }
    public event Action? OnChange;
    

    public bool AddGame(BoardGame game)
    {
        if (selectedGames.Any(g => g.Id == game.Id))
        {
            return false;
        }
        selectedGames.Add(game);
        OnChange?.Invoke();
        return true;
    }
}