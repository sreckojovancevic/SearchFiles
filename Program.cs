using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: dotnet run -c Release <directory> <filename-pattern> <search-pattern>");
            return;
        }

        string rootDir = args[0];
        string filePattern = args[1];
        string searchPattern = args[2];

        if (!Directory.Exists(rootDir))
        {
            Console.WriteLine($"Directory '{rootDir}' does not exist.");
            return;
        }

        if (!Avx2.IsSupported)
        {
            Console.WriteLine("AVX2 not supported on this CPU.");
            return;
        }

        Console.WriteLine("Scanning files...");
        var files = Directory.GetFiles(rootDir, filePattern, SearchOption.AllDirectories);

        Console.WriteLine($"Found {files.Length} file(s) matching pattern '{filePattern}'");

        var results = new ConcurrentBag<string>();

        Parallel.ForEach(files, file =>
        {
            try
            {
                using var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read, 65536, FileOptions.SequentialScan);
                using var sr = new StreamReader(fs);

                long byteOffset = 0;
                int lineNumber = 0;
                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    lineNumber++;
                    byte[] lineBytes = System.Text.Encoding.UTF8.GetBytes(line);

                    if (AvxContains(lineBytes, searchPattern))
                    {
                        results.Add($"File: {file}\n --> Pattern '{searchPattern}' found at line {lineNumber}, byte offset {byteOffset}");
                    }

                    // +1 for '\n'
                    byteOffset += lineBytes.Length + 1;
                }
            }
            catch (Exception ex)
            {
                results.Add($"Error reading file {file}: {ex.Message}");
            }
        });

        foreach (var r in results.OrderBy(r => r))
        {
            Console.WriteLine(r);
        }
    }

    static unsafe bool AvxContains(byte[] data, string pattern)
    {
        byte[] patternBytes = System.Text.Encoding.UTF8.GetBytes(pattern);
        int pLen = patternBytes.Length;
        int i = 0;

        fixed (byte* pData = data)
        fixed (byte* pPattern = patternBytes)
        {
            while (i <= data.Length - pLen)
            {
                int j = 0;

                if (Avx2.IsSupported && pLen >= 32)
                {
                    // AVX2 vectorized comparison (block-wise)
                    for (; j <= pLen - 32; j += 32)
                    {
                        var vecData = Avx.LoadVector256(pData + i + j);
                        var vecPattern = Avx.LoadVector256(pPattern + j);
                        var cmp = Avx2.CompareEqual(vecData, vecPattern);
                        if (!Avx2.TestZ(cmp, cmp))
                            continue;
                        else
                            break;
                    }
                }

                // Remaining bytes
                for (; j < pLen; j++)
                {
                    if (pData[i + j] != pPattern[j])
                        break;
                }

                if (j == pLen)
                    return true;

                i++;
            }
        }

        return false;
    }
}
