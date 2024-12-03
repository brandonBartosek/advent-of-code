using FileInput;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        //Each line of data is a "report", numbers delmited by spaces

        //The dimensions are:
        //Reports: The number of lines in the data
        //Levels: The chunks of numbers in the line

        //Determine which set of numbers results in a "Safe" sequence.

        //Conditions:
        //--Safe:
        //--All increasing or all decreasing
        //--Two adjacents levels(chunks) differ by atleast one and at most three

        var data = FileInputHandler.GetFileData("Day2.txt");

        int safeCount = 0;
        List<int[]> invalidSequences = new List<int[]>();
        for (int i = 0; i < data.Count; i++)
        {
            var numbers = data[i].Split(" ").Select(number => int.Parse(number.Trim())).ToArray();
            if (IsSequenceSafe(numbers))
            {
                safeCount++;
            }
            else
            {
                invalidSequences.Add(numbers);
            }
        }


        Console.WriteLine($"Safe count: {safeCount}");


        Console.WriteLine($"-------------- PART 2 --------------");

        //If there are any invalid sequences, re-evaluate them to determine if they can be "tolerated"
        if (invalidSequences.Count > 0)
        {

            //For each invalid sequence, check the subsequences that could be generated when removing an item.
            foreach (var sequence in invalidSequences)
            {
                var subSequences = new List<List<int>>();
                for (int i = 0; i < sequence.Length; i++)
                {
                    //Copy the sequence, remove the item, add the possible new sequence.
                    var copySequence = sequence.ToList();
                    copySequence.RemoveAt(i);

                    //Validate the subsequence. If any subsequence is valid, then this result can be tolerated.
                    if(IsSequenceSafe([..copySequence]))
                    {
                        safeCount++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"Tolerated Safe count: {safeCount}");
    }

    

    public static bool IsSequenceSafe(int[] sequence)
    {
        //Direction 0 asc, 1 desc
        int? direction = null;
        int? previous = null;

        foreach (var number in sequence)
        {
            //If we have seen a number in this level
            if (previous != null)
            {
                var difference = number - previous;

                //Handle ascending and descending checks
                if (difference > 0)
                {
                    if (direction != null && direction == 1)
                    {
                        //Direction is 1 (Descending) but would result in an ascending
                        Console.WriteLine("Direction is 1 (Descending) but would result in an ascending");
                        return false;
                    }
                    //Ascending
                    direction = 0;
                }
                else if (difference < 0)
                {
                    if (direction != null && direction == 0)
                    {
                        //Direction is 0 (ascending) but would result in a descending
                        Console.WriteLine("Direction is 1 (Descending) but would result in an ascending");
                        return false;
                    }

                    //Descending
                    direction = 1;
                }

                //Determine if the previous number is within 1 to 3 numbers in range.
                var absDifference = Math.Abs((int)difference);
                if (absDifference < 1 || absDifference > 3)
                {
                    Console.WriteLine($"Difference of '{absDifference}' is out of range."); 
                    return false;
                }


            }

            previous = number;
        }

        return true;

    }



}