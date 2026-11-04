using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TransactionProcessingDemo;

public class TransactionProcessor
{
    private decimal _balance;
    private readonly TraceSwitch _traceSwitch;

    public decimal Balance => _balance;

    public TransactionProcessor(decimal initialBalance)
    {
        _balance = initialBalance;

        _traceSwitch = new TraceSwitch(
            "TransactionSwitch",
            "Transaction logging level");

        _traceSwitch.Level = TraceLevel.Verbose;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Enter a valid deposit amount.");
            return;
        }

        _balance += amount;

        string message =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
            $"INFO: Deposit successful. Amount: ${amount:F2}, " +
            $"Balance: ${_balance:F2}";

        Debug.WriteLine(message);
        Console.WriteLine(message);
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Enter a valid withdrawal amount.");
            return;
        }

        if (amount > _balance)
        {
            string warning =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                $"WARNING: Insufficient balance for withdrawal of ${amount:F2}.";

            if (_traceSwitch.TraceWarning)
            {
                Trace.WriteLine(warning);
            }

            // Switch to production error-only logging.
            _traceSwitch.Level = TraceLevel.Error;

            string error =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                $"ERROR: Withdrawal failed. Requested: ${amount:F2}, " +
                $"Available: ${_balance:F2}.";

            if (_traceSwitch.TraceError)
            {
                Debug.WriteLine(error);
                Console.WriteLine(error);
            }

            return;
        }

        _balance -= amount;

        string message =
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
            $"INFO: Withdrawal successful. Amount: ${amount:F2}, " +
            $"Balance: ${_balance:F2}";

        Debug.WriteLine(message);
        Console.WriteLine(message);
    }
}
