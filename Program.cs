using System.Linq;

Console.WriteLine("Hello Advent of Code");

var debug = true;

var cki = new ConsoleKeyInfo();

do
{
    while (Console.KeyAvailable == false)
    {
        Thread.Sleep(250);
    }
    cki = Console.ReadKey(true);
    var day_select = cki.KeyChar.ToString();
    switch (day_select)
    {
        case "d":
            debug = debug == true ? false : true;
            Console.WriteLine(debug == true ? "Debug On" : "Debug Off");
            break;
        case "1":
            DayOne();
            break;
        
        default:
        case "?":
            Console.WriteLine($"Usage: ");

            Console.WriteLine("d - Toggle Debug Mode");
            Console.WriteLine("q - Quit");
            Console.WriteLine("1, 2, 3... etc - Run the Solution for that day");
            break;
    }
}
while (cki.Key != ConsoleKey.Q);


void DayOne()
{
    var path_1 = @"C:\Users\thats\source\repos\adventofcode2021\inputs\day01input.txt";
    var input = File.ReadAllLines(path_1);

    var increased = 0;
    var decresed = 0;
    var noChange = 0;

    var prev = (int?)null;
    var next = (int?)null;

    var windowSize = 3;
    var prevWindow = new int[0];
    var nextWindow = new int[0];

    var windowIncreased = 0;
    var windowDecreased = 0;
    var windowNoChange = 0;

    foreach (var line in input)
    {
        int.TryParse(line, out var depth);
        if(prev != null)
        {
            next = depth;

            var loopVal = prev - next;
            if(loopVal < 0)
            {
                increased++;
            }
            else if(loopVal > 0)
            {
                decresed++;
            }
            else if(loopVal == 0)
            {
                noChange++;
            }


            if (prevWindow.Length < windowSize)
            {
                prevWindow = prevWindow.Concat(new int[1] { depth }).ToArray();
            }
            else
            {
                prevWindow = prevWindow.Skip(1).Take(2).ToArray().Concat(new int[1] {  prev.Value }).ToArray();
            }
            if(nextWindow.Length < windowSize)
            {
                nextWindow = nextWindow.Concat(new int[1] { depth }).ToArray();
            }
            else
            {
                nextWindow = nextWindow.Skip(1).Take(2).ToArray().Concat(new int[1] { depth }).ToArray();
            }

            var prevSum = prevWindow.Sum();
            var nextSum = nextWindow.Sum();

            var windowVal = prevSum - nextSum;

            if (prevWindow.Length == windowSize && nextWindow.Length == windowSize)
            {
                if (windowVal < 0)
                {
                    windowIncreased++;
                }
                else if (windowVal > 0)
                {
                    windowDecreased++;
                }
                else
                {
                    windowNoChange++;
                }
            }

            // Console.WriteLine($"previous: {String.Join(", ", prevWindow)}, next: {String.Join(", ", nextWindow)}");
            prev = next;
        }
        else
        {
            prev = depth;
            prevWindow = prevWindow.Concat(new int[1] { depth }).ToArray();
        }

        
    }

    Console.WriteLine($"Increased: {increased}, Decreased: {decresed}, No Changes: {noChange}");
    Console.WriteLine($"Window Size: {windowSize} - Increased: {windowIncreased}, Decreased: {windowDecreased}, No Changes: {windowNoChange}");
}