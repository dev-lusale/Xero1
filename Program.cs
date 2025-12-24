using System;
using System.Windows.Forms;

namespace Xero1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Check if running tests
            if (args.Length > 0 && args[0].ToLower() == "test")
            {
                // Simple test execution
                Console.WriteLine("Running basic conversion tests...");
                try
                {
                    // Test basic conversions
                    string result1 = NumberConverter.ConvertBetweenBases("255", 10, 2);
                    string result2 = NumberConverter.ConvertBetweenBases("FF", 16, 10);
                    string result3 = NumberConverter.ConvertBetweenBases("1010", 2, 16);
                    
                    Console.WriteLine($"255 (decimal) to binary: {result1}");
                    Console.WriteLine($"FF (hex) to decimal: {result2}");
                    Console.WriteLine($"1010 (binary) to hex: {result3}");
                    Console.WriteLine("Basic tests completed successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Test failed: {ex.Message}");
                }
                return;
            }

            // Run the GUI application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}