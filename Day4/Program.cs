using FileInput;
using Logger;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main(string[] args)
    {
        var data = FileInputHandler.GetFileData("Day4.txt");


        //Load the data into a 2d grid so that we can use index positions
        char[,] grid = new char[data.Count, data[0].Length];
        for (int i = 0; i < data.Count; i++)
        {
            for(int j = 0; j < data[i].Length; j++)
            {
                grid[i,j] = data[i][j];    
            }
        }

        //Part1(data, grid);
        Part2(data, grid);
    }

    private static void Part1(List<string> data, char[,] grid)
    {
        //For each word being searched
        //Iterate over the grid
        //Check if the current position is a starting letter of the word
        //Check if there exists any cells wordLength - 1 away that contain the ending letter of the word.
        //If so, check the letters inbetween by determining the direction between the two cells.

        var wordOccurrences = new Dictionary<string, int>()
        {
            { "XMAS", 0 }
        };
        foreach (var entry in wordOccurrences)
        {
            var word = entry.Key;
            var characters = entry.Key.ToCharArray();
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    //If the grid position contains the starting letter
                    if (grid[i, j] == characters[0])
                    {
                        //Check surrounding cells for the ending letter.
                        var positions = new List<(int, int)>()
                        {
                            //Top Left
                            (i - (word.Length - 1), j - (word.Length - 1)),
                            //Left
                            (i, j- (word.Length - 1)),
                            //Bottom Left
                            (i + (word.Length - 1), j - (word.Length - 1)),
                            //Bottom
                            (i+ (word.Length - 1), j),
                            //Bottom Right
                            (i + (word.Length - 1), j + (word.Length - 1)),
                            //Right
                            (i, j + (word.Length - 1)),
                            //Top Right
                            (i - (word.Length - 1), j + (word.Length - 1)),
                            //Top
                            (i - (word.Length - 1), j)
                        };

                        var positionsInBounds = positions.Where(p => IsPositionInBounds(p, data.Count - 1, data[i].Length - 1));
                        foreach (var position in positionsInBounds)
                        {
                            //If the possible word end position is the word's ending letter, check the remaning letters in-between
                            if (grid[position.Item1, position.Item2] == characters[word.Length - 1])
                            {
                                //Get the direction between the start and end 
                                var direction = (Math.Sign(position.Item1 - i), Math.Sign(position.Item2 - j));

                                //For each letter remaining in the word
                                if (characters.Length > 3)
                                {
                                    var remainingLetters = characters[1..(word.Length - 1)];
                                    var wordFound = true;
                                    for (int letterIndex = 0; letterIndex < remainingLetters.Length; letterIndex++)
                                    {
                                        //Get the letter's position
                                        var letter = grid[i + (direction.Item1 * (letterIndex + 1)), j + (direction.Item2 * (letterIndex + 1))];
                                        if (letter != remainingLetters[letterIndex])
                                        {
                                            wordFound = false;
                                            break;
                                        }
                                    }

                                    //If we've checked all the letters then we can add this count to the list.
                                    if (wordFound)
                                    {
                                        wordOccurrences[word]++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        foreach (var entry in wordOccurrences)
        {
            Console.WriteLine($"Word: {entry.Key} Times seen: {entry.Value}");
        }
    }

    private static void Part2(List<string> data, char[,] grid) {
        var XmasOccurences = 0;
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                //If the grid position is the central letter
                if (grid[i, j] == 'A')
                {
                    //Check surrounding cells for the other letters.
                    var positions = new List<(int, int)>()
                        {
                            //Top Left
                            (i - 1, j - 1),
                            //Bottom Left
                            (i + 1, j - 1),
                            //Bottom Right
                            (i + 1, j + 1),
                            //Top Right
                            (i - 1, j + 1),
                        };

                    if(!positions.All(p => IsPositionInBounds(p, data.Count - 1, data[i].Length - 1)))
                    {
                        //Not all positions are in bounds so an X can't exist.
                        continue;
                    }

                    var letters = new string (positions.Select(p => grid[p.Item1, p.Item2]).ToArray());
                    var validSequences = new List<string>
                    {
                        "MMSS",
                        "MSSM",
                        "SSMM",
                        "SMMS"
                    };

                    if(validSequences.Contains(letters))
                    {
                        XmasOccurences++;
                    }
                }
            }
        }
        Console.WriteLine(XmasOccurences);

    }
    private static bool IsPositionInBounds((int, int) position, int gridXMax, int gridYMax)
    {
        if (position.Item1 < 0 || position.Item1 > gridXMax)
        {
            return false;
        }
        else if(position.Item2 < 0 || position.Item2 > gridYMax)
        {
            return false;
        }

        return true;
    }
}