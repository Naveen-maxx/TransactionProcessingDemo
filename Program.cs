
using System.Diagnostics;
using TransactionProcessingDemo;

// Send Trace messages to the console.
Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
Trace.AutoFlush = true;

TransactionProcessor processor = new TransactionProcessor(500m);

bool running = true;

while (running)
{
    Console.WriteLine("\n===== TRANSACTION MENU =====");
    Console.WriteLine("1. Deposit");
    Console.WriteLine("2. Withdraw");
    Console.WriteLine("3. Check Balance");
    Console.WriteLine("4. Exit");

    Console.Write("Enter your choice: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter deposit amount: $");

            if (decimal.TryParse(Console.ReadLine(), out decimal deposit))
            {
                processor.Deposit(deposit);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
            break;

        case "2":
            Console.Write("Enter withdrawal amount: $");

            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawal))
            {
                processor.Withdraw(withdrawal);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
            break;

        case "3":
            Console.WriteLine($"Current balance: ${processor.Balance:F2}");
            break;

        case "4":
            running = false;
            Console.WriteLine("Transaction system closed.");
            break;

        default:
            Console.WriteLine("Invalid choice. Try again.");
            break;
    }
}
