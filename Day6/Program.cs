using FileInput;
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        var data = FileInputHandler.GetFileData("Day6.txt");

        //Build a grid that can be tracked and navigated
        var guardPosition = (0, 0);
        var guardDirection = 0; // 0 is up, 90 is right, 180 is down, 270 is left
        char[,] grid = new char[data.Count, data[0].Length];
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                grid[i, j] = data[i][j];
                //Player starting position
                if (data[i][j] == '^')
                {
                    guardPosition = (i, j);
                }
            }
        }


        //If the guard leaves the area we are done tracking his position.
        //If the guard collides with an obstruction, rotate 90 degrees CW
        var guardIsInBounds = true;
        //Positions visited starts at 1 to account for the place hes at initially.
        List<(int,int)> visited = new List<(int,int)> ();
        visited.Add(guardPosition);
        while (guardIsInBounds)
        {
            //Attempt to move in the pointing direction.
            //If moving would result in a collision with an obstacle, rotate 90 degrees.
            //If moving would result in being out of bounds, we're done!
            //If moving would result in moving into empty space, move and add to positions visited.
            var futureGuardPosition = guardPosition;
            if (guardDirection == 0)
            {
                futureGuardPosition = (guardPosition.Item1 - 1, guardPosition.Item2);
            }
            else if (guardDirection == 90)
            {
                futureGuardPosition = (guardPosition.Item1, guardPosition.Item2 + 1);
            }
            else if (guardDirection == 180)
            {
                futureGuardPosition = (guardPosition.Item1 + 1, guardPosition.Item2);
            }
            else if (guardDirection == 270)
            {
                futureGuardPosition = (guardPosition.Item1, guardPosition.Item2 - 1);
            }


            //Bounds Checking
            if (futureGuardPosition.Item1 < 0 || futureGuardPosition.Item1 >= data.Count)
            {
                guardIsInBounds = false;
                break;
            }
            else if (futureGuardPosition.Item2 < 0 || futureGuardPosition.Item2 >= data[0].Length)
            {
                guardIsInBounds = false;
                break;
            }


            //If we collide with an obstacle, rotate.
            if (grid[futureGuardPosition.Item1, futureGuardPosition.Item2] == '#')
            {
                var newDirection = guardDirection + 90;
                guardDirection = newDirection == 360 ? 0 : newDirection;
            }
            //If the space is open, move to it and mark the new location as visited.
            else if(grid[futureGuardPosition.Item1, futureGuardPosition.Item2] == '.')
            {
                guardPosition = futureGuardPosition;
                if (!visited.Contains(guardPosition))
                {
                    visited.Add(guardPosition);
                }
            }
        }

        Console.WriteLine(visited.Count());
    }
}