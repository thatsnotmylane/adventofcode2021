using System.Linq;
using System.Text.RegularExpressions;

Console.WriteLine("Hello Advent of Code");

var debug = true;

var cki = new ConsoleKeyInfo();

do
{
    DayThree();
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
        case "2":
            DayTwo();
            break;
        case "3":
            DayThree();
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

void DayTwo()
{
    var filePath = @"C:\Users\thats\source\repos\adventofcode2021\inputs\day02input.txt";
    var input = File.ReadAllLines(filePath);

    var p1horizontal = 0;
    var p1depth = 0;

    var p2Horizontal = 0;
    var p2Depth = 0;
    var p2Aim = 0;

    foreach(var line in input)
    {
        int.TryParse(line.Last().ToString(), out var amount);

        if (line.Contains("forward"))
        {
            p1horizontal = p1horizontal + amount;

            p2Horizontal = p2Horizontal + amount;
            p2Depth = p2Depth + (p2Aim * amount);
        }
        else if (line.Contains("down"))
        {
            p1depth = p1depth + amount;

            p2Aim = p2Aim + amount;
        }
        else if (line.Contains("up"))
        {
            p1depth = p1depth - amount;

            p2Aim = p2Aim - amount;
        }
        
    }

    Console.WriteLine("Part One");
    Console.WriteLine($"Horizontal: {p1horizontal} Depth: {p1depth}. Part 1 Anser: {p1horizontal * p1depth}");

    Console.WriteLine("Part Two");
    Console.WriteLine($"Horizontal: {p2Horizontal} Depth: {p2Depth} Aim: {p2Aim}. Part 2 Answer: {p2Horizontal * p2Depth}");
}

void DayThree()
{
    var filePath = @"C:\Users\thats\source\repos\adventofcode2021\inputs\day03input.txt";
    var input = File.ReadAllLines(filePath);

    var first = input.First();
    var lineLen = first.Length;
    var totalLines = input.Length;

    var oneCounts = new int[lineLen];

    foreach(var line in input)
    {
        var lineArr = line.ToArray();
        for(var i = 0; i < lineLen; i++)
        {
            if(lineArr[i] == '1')
            {
                oneCounts[i]++;
            }
        }
    }

    var gamma = new int[lineLen];
    var epsolon = new int[lineLen];

    var gammaTotal = 0.0;
    var epsTotal = 0.0;

    for(var i = 0; i < lineLen; i++)
    {
        var exponent = lineLen - i - 1;
        var col = oneCounts[i];
        if(col > totalLines / 2)
        {
            gamma[i] = 1;
            gammaTotal = gammaTotal + Math.Pow(2, exponent);
            epsolon[i] = 0;
        }
        else
        {
            gamma[i] = 0;
            epsolon[i] = 1;
            epsTotal = epsTotal + Math.Pow(2, exponent);
        }
    }

    Console.WriteLine($"G: {gammaTotal} E:{epsTotal}. Part 1 Anser: {gammaTotal * epsTotal}");
}