using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetGameEndpointName = "GetGame";
List<GameDto> games = [
    new GameDto(1, "Game 1", "Action", 59.99m, new DateOnly(2023, 10, 1)),
    new GameDto(2, "Game 2", "Adventure", 49.99m, new DateOnly(2023, 9, 15)),
    new GameDto(3, "Game 3", "RPG", 39.99m, new DateOnly(2023, 8, 20))
    ];

// GET: /games
app.MapGet("games", () => games);

// GET: /games/{id}
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
    .WithName(GetGameEndpointName);

// POST: /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

app.MapGet("/", () => "Hello Subash!");

app.Run();
