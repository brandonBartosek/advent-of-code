
using FileInput;

public class Program
{
    private static void Main(string[] args)
    {
        var data = FileInputHandler.GetFileData("Day1.txt");

        //Parse the file into List<int> that can be sorted.
        var leftList = new List<int>();
        var rightList = new List<int>();
        foreach (var item in data)
        {
            var numbers = item.Split(" ").Where(i => !string.IsNullOrEmpty(i)).ToList();
            leftList.Add(int.Parse(numbers[0].Trim()));
            rightList.Add(int.Parse(numbers[1].Trim()));
        }

        //Sort the left and right list
        leftList.Sort();
        rightList.Sort();

        var part1Total = Part1(leftList, rightList).ToString();
        Console.WriteLine(part1Total);

        var part2Total = Part2(leftList, rightList).ToString();
        Console.WriteLine(part2Total);
    }
    public static int Part1(List<int> leftList, List<int> rightList)
    {
        //Iterate over the lists, summing their differences as a total.
        var total = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            total += Math.Abs(leftList[i] - rightList[i]);
        }
        return total;
    }
    public static int Part2(List<int> leftList, List<int> rightList)
    {

        //Iterate over the lists, multiplying the left item by its count occurrence in the right 
        var total = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            total += leftList[i] * rightList.Where(r => r == leftList[i]).Count();
        }
        return total;
    }

}