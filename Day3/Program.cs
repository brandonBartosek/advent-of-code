using FileInput;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        var data = FileInputHandler.GetFileData("Day3.txt");

        //Merge the multiple data lines into a single stream of character data 
        var singleLineData = string.Join("", data.ToArray()).ToCharArray();


        //Iterate over each character of the data keep track of the possible matching sequence
        Stack<char> commandStack = new Stack<char>();
        Regex mulRegex = new Regex("mul\\([[0-9]*,[0-9]*\\)");
        Regex doRegex = new Regex("do\\(\\)");
        Regex dontRegex = new Regex("don't\\(\\)");
        int total = 0;
        bool isDoing = true;
        foreach (var character in singleLineData)
        {
            //We only care to even look at sequences if they begin with m
            if((commandStack.Count == 0 && (character == 'm' || character == 'd')) || commandStack.Count > 0)
            {
                commandStack.Push(character);
            }

            //If the end of the command is found, pop the stack and see if it matches the regex for a valid multiplication
            if (commandStack.Count > 0 && commandStack.Peek() == ')')
            {   
                var command = string.Empty;
                foreach(var value in commandStack.Reverse())
                {
                    command += value;
                }


                //Handle processing the mul command
                var match = mulRegex.Match(command);
                if (match.Success)
                {
                    //Parse out the numbers and multiply, sum to total.
                    var matchString = match.ToString();
                    Console.WriteLine(matchString);
                    var split = matchString.Split(',');

                    //Trim left
                    var leftNumber = int.Parse(split[0].Split('(')[1]);
                    var rightNumber = int.Parse(split[1].Split(')')[0]);

                    //Only apply the total if we are "do"
                    if(isDoing)
                    {
                        total += leftNumber * rightNumber;
                    }
                }

                //Handle processing the do command
                var doMatch = doRegex.Match(command);
                if (doMatch.Success)
                {
                    isDoing = true;
                }

                var dontMatch = dontRegex.Match(command);
                if (dontMatch.Success)
                {
                    isDoing = false;
                }


                command = string.Empty;
                commandStack.Clear();
            }

        }

        Console.WriteLine(total);

    }
}