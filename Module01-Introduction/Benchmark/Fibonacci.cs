using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Dotnetos.AsyncExpert.Homework.Module01.Benchmark
{
    [DisassemblyDiagnoser(exportCombinedDisassemblyReport: true)]
    [MemoryDiagnoser]
    public class FibonacciCalc
    {
        // HOMEWORK:
        // 1. Write implementations for RecursiveWithMemoization and Iterative solutions
        // 2. Add MemoryDiagnoser to the benchmark
        // 3. Run with release configuration and compare results
        // 4. Open disassembler report and compare machine code
        // 
        // You can use the discussion panel to compare your results with other students

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public ulong Recursive(ulong n)
        {
            if (n == 1 || n == 2) return 1;
            return Recursive(n - 2) + Recursive(n - 1);
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong RecursiveWithMemoization(ulong n)
        {
            if (n == 1 || n == 2) return 1;

            var memory = new ulong[n];
            memory[0] = 1;
            memory[1] = 1;

            return RecWMemo(n, memory);

            static ulong RecWMemo(ulong nInner, ulong[] memo)
            {
                var memorized = memo[nInner - 1];
                if (memorized == 0)
                {
                    memorized = RecWMemo(nInner - 2, memo) + RecWMemo(nInner - 1, memo);
                    memo[nInner - 1] = memorized;
                }

                return memorized;
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public ulong Iterative(ulong n)
        {
            if (n == 1 || n == 2) return 1;

            ulong prev = 1;
            ulong prev2 = 1;
            ulong current = 2;
            for (ulong i = 2; i < n; i++)
            {
                current = prev + prev2;
                prev2 = prev;
                prev = current;
            }

            return current;
        }

        public IEnumerable<ulong> Data()
        {
            yield return 15;
            yield return 35;
        }
    }
}