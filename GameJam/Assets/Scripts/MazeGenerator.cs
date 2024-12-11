using System;
using System.Collections.Generic;
using System.Linq;


public static class MazeGenerator {

        public static Cell[,] GenerateMaze(int width, int height, int checkpointsCount, int minCheckpointRange, int maxCheckpointRange)
    {
        var random = new Random();
        var checkpoints = new List<(int y, int x)>();

        var maze = new Cell[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j] = Cell.Wall;
            }
        }

        var startX = random.Next(1, width - 2);
        var endX = random.Next(1, width - 2);

        checkpoints.Add(new ValueTuple<int, int>(0, startX));

        for (int i = 0; i < checkpointsCount; i++)
        {

            //var checkpointY = random.Next(1, height - 2);
            //var checkpointX = random.Next(1, width - 2);

            var vectorValueX = random.Next(
                minCheckpointRange,
                maxCheckpointRange + 1
            );

            var vectorValueY = random.Next(
                minCheckpointRange,
                maxCheckpointRange + 1
            );

            var directionX = random.Next(0, 2) == 0 ? -1 : 1;
            var directionY = random.Next(0, 2) == 0 ? -1 : 1;

            var vectorY = Clamp(checkpoints[i].y + (vectorValueY * directionY), 1, height - 2);
            var vectorX = Clamp(checkpoints[i].y + (vectorValueX * directionX), 1, width - 2);

            checkpoints.Add(new ValueTuple<int, int>(vectorY, vectorX));
            maze[vectorY, vectorX] = Cell.Checkpoint;
        }

        checkpoints.Add(new ValueTuple<int, int>(height - 1, endX));

        maze[0, startX] = Cell.Entrance;
        maze[height - 1, endX] = Cell.Exit;

        for (var i = 0; i < checkpointsCount + 1; i++)
        {
            var start = checkpoints[i];
            var end = checkpoints[i + 1];

            var currentPosition = start;

            var diffY = start.y - end.y;
            var diffX = start.x - end.x;

            var movesLeftY = Math.Abs(diffY);
            var movesLeftX = Math.Abs(diffX);

            var directionY = diffY > 0 ? -1 : 1;
            var directionX = diffX > 0 ? -1 : 1;
            while (movesLeftY > 0 || movesLeftX > 0)
            {
                if (movesLeftY > 0)
                {
                    var moveValueY = Math.Min(random.Next(1, movesLeftY + 1), movesLeftY);
                    for (var j = 0; j < moveValueY; j++)
                    {
                        currentPosition.y += directionY;
                        maze[currentPosition.y, currentPosition.x] =
                            maze[currentPosition.y, currentPosition.x] != Cell.Checkpoint ? Cell.Path : Cell.Checkpoint;
                    }

                    movesLeftY -= moveValueY;
                }

                if (movesLeftX > 0)
                {
                    var moveValueX =
                        Math.Min(random.Next(1, movesLeftX + 1), movesLeftX); // Sprawdź, czy mieści się w movesLeftX
                    for (var j = 0; j < moveValueX; j++)
                    {
                        currentPosition.x += directionX;
                        maze[currentPosition.y, currentPosition.x] =
                            maze[currentPosition.y, currentPosition.x] != Cell.Checkpoint ? Cell.Path : Cell.Checkpoint;
                    }

                    movesLeftX -= moveValueX;
                }
            }
        }

        return maze;
    }


    static int Clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

}