# SearchFiles

![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-512bd4)
![Language: C#](https://img.shields.io/badge/Language-C%23-orange)

High-performance **multi-threaded file and content search** for Windows, written in **C#**.  
Supports **AVX2 accelerated search** for content inside files, optimized for large datasets.

---

## Features

- Search for **files by name** with wildcards.
- Search **inside file contents** with AVX2 acceleration.
- Multi-threaded processing for maximum CPU utilization.
- Displays **line numbers and byte offsets** for matches.
- Supports **large directories**, including network drives and NVMe/HDD.
- Optional: single file content search.
- MIT licensed — free to use and modify.

---

## Requirements

- Windows 10 / 11
- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- CPU with AVX2 support for fastest content search (optional fallback for non-AVX2 CPUs).

Check your CPU for AVX2 support:

```powershell
wmic cpu get Name,Caption,Description,Architecture,DataWidth
````

---

## Usage

### File name search:

```powershell
dotnet run -c Release <directory_path> <filename_pattern>
```

Example:

```powershell
dotnet run -c Release E:\ONEDRIVE\ *.pdf
```

Output:

```
Found 79 file(s) matching pattern '*.pdf'
File: E:\ONEDRIVE\Example.pdf
File: E:\ONEDRIVE\Folder\OtherExample.pdf
...
```

### Content search inside files:

```powershell
dotnet run -c Release <directory_path> <filename_pattern> <search_pattern>
```

Example:

```powershell
dotnet run -c Release E:\ONEDRIVE\ 1.txt 0
```

Output:

```
Found 4 file(s) matching pattern '1.txt'
File: E:\ONEDRIVE\1.txt
 --> Pattern '0' found at line 1, byte offset 0
File: E:\ONEDRIVE\Folder\1.txt
 --> Pattern '0' found at line 3, byte offset 70
...
```

---

## How It Works

* **AVX2 acceleration:** Uses SIMD instructions to speed up substring search in file contents.
* **Multi-threaded scanning:** Utilizes all CPU cores to process multiple files concurrently.
* **Byte offset reporting:** Shows exact position in file where the pattern is found.
* Works efficiently with **large directories** and network storage.

---

## Performance Notes

* Single-threaded AVX2 search: ~12 GB/s throughput on a modern i9 CPU.
* Multi-threaded AVX2 search: ~58 GB/s throughput.
* NVMe SSD recommended for best disk I/O performance.
* Can scan tens of thousands of files in seconds.

---

## License

This project is licensed under the **MIT License** — see [LICENSE](LICENSE) for details.

---

## Author

**Srećko Jovancevic**

> High-performance search tool with AVX2 acceleration, multi-threading, and professional usage in large enterprise datasets.
> Inspired by personal need to search large OneDrive and network directories efficiently.

---
## Acknowledgements

This project README and some guidance were prepared with the assistance of **[ChatGPT](https://openai.com/chatgpt)**, an AI language model by OpenAI.

## Notes

* Supports both **filename and content search**.
* Works on large datasets and high-capacity drives.
* Optional **offset + line number reporting** helps locate content quickly.

