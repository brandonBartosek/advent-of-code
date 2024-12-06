using FileInput;

internal class Program
{
    private static void Main(string[] args)
    {
        var data = FileInputHandler.GetFileData("Day5.txt");
        var rules = new Dictionary<int, List<int>>();
        var updates = new List<string>();

        //Split the rules from the updates, build the dictionary of rules, store the update instructions
        var splitCharacter = "";
        var readingRules = true;
        foreach (var item in data)
        {
            if (item == splitCharacter)
            {
                readingRules = false;
                continue;
            }

            if (readingRules)
            {
                //Parse the rule line
                var numbers = item.Split("|");
                var requiredPreviousNumber = int.Parse(numbers[0].Trim());
                var keyNumber = int.Parse(numbers[1].Trim());

                if(!rules.ContainsKey(keyNumber))
                {
                    rules[keyNumber] = new List<int>(); 
                }

                rules[keyNumber].Add(requiredPreviousNumber);
            }
            else
            {
                updates.Add(item);
            }
        }


        //Part 1
        var correctMiddleSum = 0;
        var incorrectUpdates = new List<string>();
        foreach (var update in updates)
        {
            var numbers = update.Split(",").Select(n => int.Parse(n)).ToList();
            var valid = true;
            for(int i =0; i<numbers.Count; i++) {
                //Don't process numbers without rules
                if (!rules.ContainsKey(numbers[i]))
                {
                    continue;
                }

                //Look-up the number and get the rules for this number.
                var requiredPreviousPages = rules[numbers[i]];

                //If any of this number's required pages are after this page then it fails.
                if (requiredPreviousPages.Any(r => numbers[i..].Contains(r)))
                {
                    incorrectUpdates.Add(update);
                    valid = false;
                    break;
                }
            }

            if(valid)
            {
                //Assumes that the update is an odd number of numbers.
                var middleValue = numbers[(numbers.Count - 1) / 2];
                correctMiddleSum += middleValue;
            }
        }


        Console.WriteLine("Part 1: Correct Middle Sum:");
        Console.WriteLine(correctMiddleSum);

        //Part 2
        //We need to sort the incorrect updates based on the rules provided to make them correct.
        var sum = 0;
        foreach(var update in incorrectUpdates)
        {
            var numbers = update.Split(",").Select(n => int.Parse(n)).ToList();
            var sortedSequence = SortSequence(numbers, rules);
            sum += sortedSequence[(numbers.Count() - 1) / 2];
        }

        Console.WriteLine(sum);
    }   

    private static List<int> SortSequence(List<int> numbers, Dictionary<int, List<int>> rules)
    {
        numbers.Sort((x, y) =>
        {
            bool xMatches = rules[x].Contains(y);
            bool yMatches = rules[y].Contains(x);

            if (xMatches) return -1; 
            if (yMatches) return 1;  

            return 0; 
        });

        return numbers;
    }
}