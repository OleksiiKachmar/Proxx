//TODO Cover negative cases, like nulls etc

using Proxx;




//

GameSettings gameSettings = new GameSettings()
{
    BoardSize = 9,
    BlackHoleCount = 8
};
IBlackHoleGenerator blackHoleGenerator = new RandomBlackHoleGenerator();
ICounterCellGenerator counterCellGenerator = new CounterCellGenerator();
GameManager gameManager = new GameManager(gameSettings, blackHoleGenerator, counterCellGenerator);
gameManager.ClickCell(5,6);
gameManager.ClickCell(3, 4);

Console.ReadLine();