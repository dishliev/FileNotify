string directoryPath = "C:\\MyDirectory";
if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

while (true)
{
    Console.WriteLine("Enter text:");
    string input = Console.ReadLine();

    string fileName = $"file_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
    string filePath = Path.Combine(directoryPath, fileName);
    File.WriteAllText(filePath, input);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("File created: ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(fileName);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($" in directory: {directoryPath}");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Do you want to create another file? (Y/N)");
    Console.ResetColor();

    string response = Console.ReadLine();

    if (response.ToLower() != "y")
        break;
}
