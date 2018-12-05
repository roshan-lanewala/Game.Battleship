# Game.Battleship
C# implementation of Battleship game design

* Project is build using .net core 2.0

To build the project:
```
git clone https://github.com/roshan-lanewala/Game.Battleship.git
cd ./Game.Battleship
dotnet build
```

reference the DLL ./Game.BattleShip/bin/Debug/netcoreapp2.0/Game.BattleShip.dll in a Console app.

```
class Program
{
    static void Main(string[] args)
    {
        Game game = new Game(10, 10);
        string result = null;
        do
        {
          result = game.Play();
        } while (result == null);
        Console.WriteLine(result);
        Console.ReadLine();
    }
}
```

The above program will instantiate a Game board of 10x10 between two players. This will create the Boards for player to place Battleships and Fire shots on opponent player. Once a Player all ships sunk the opponent will be announced winner. Calling the ```Play()``` in the while loop will play rounds between the two player until one player has lost.
