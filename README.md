# SearchFiles - Multi-threaded AVX2 File Content Search

A **high-performance file search utility** written in C# (.NET 9.0) using **AVX2 SIMD instructions** and multi-threading. Designed for extremely fast content searches across directories and large disks (HDD/SSD/NVMe). Ideal for both **text content search** and **filename pattern search**.

---

## Features

- **Recursive file search** in directories
- **Filename pattern matching** (`*.txt`, `UPUTSTVO*.pdf`, etc.)
- **Multi-threaded content search** using AVX2 SIMD instructions
- Reports **line number and byte offset** of matches
- Supports **large directories and disks** efficiently
- Works on **HDD, SSD, and NVMe**
- Extremely fast: up to tens of GB/s throughput on supported CPUs
- Optionally restrict search to certain directories or file types

---

## Requirements

- Windows or Linux
- .NET 9.0 SDK
- AVX2-capable CPU (Intel or AMD)
- Sufficient RAM for large searches (recommended 32 GB for massive data)

---

## Installation / Build

1. Clone the repository:

```bash
git clone <your-repo-url>
cd SearchFiles
````

2. Build the project in Release mode:

```bash
dotnet build -c Release
```

---

## Usage

```bash
dotnet run -c Release <directory> <file-pattern> <search-pattern>
```

### Examples

**1. Search all PDFs starting with "UPUTSTVO" in OneDrive:**

```bash
dotnet run -c Release E:\ONEDRIVE\ UPUTSTVO*.pdf
```

**Output example:**

```
Found 79 file(s) matching pattern 'UPUTSTVO*.pdf'
File: E:\ONEDRIVE\...UPUTSTVO ZA PRISTUPANJE.pdf
File: E:\ONEDRIVE\...uputstvo_za_october_RAin.pdf
...
```

**2. Search for pattern "0" inside `1.txt` files:**

```bash
dotnet run -c Release E:\ONEDRIVE\ 1.txt 0
```

**Output example:**

```
Scanning files...
Found 4 file(s) matching pattern '1.txt'
File: E:\ONEDRIVE\...\1.txt
 --> Pattern '0' found at line 1, byte offset 0
File: E:\ONEDRIVE\...\1.txt
 --> Pattern '0' found at line 3, byte offset 70
...
```

---

## How It Works

* Uses **AVX2 SIMD instructions** to accelerate content scanning.
* Supports **multi-threading** to utilize all CPU cores.
* Combines **filename pattern matching** and **in-file content search**.
* Reports **matches with line numbers and byte offsets** for easy referencing.

---

## Notes

* Unsafe C# code is used (`/unsafe` compilation flag required) for SIMD optimization.
* AVX2 instructions require a CPU that supports them.
* Performance depends on CPU speed, storage speed (HDD/SSD/NVMe), and available RAM.
* Searching huge directories may temporarily increase disk usage or system load.

---

## Contributing

Contributions are welcome! You can help by:

* Adding **logging to CSV/JSON**
* Filtering files by **size or date**
* Adding a **GUI interface**
* Enhancing **pattern matching features**
* Optimizing **multi-threaded throughput**

To contribute:

1. Fork the repository
2. Create a branch: `git checkout -b feature-name`
3. Commit your changes: `git commit -m "Add feature"`
4. Push to branch: `git push origin feature-name`
5. Submit a Pull Request

---

## License

This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.

---

## About

This project was developed with guidance from **ChatGPT (GPT-5 mini)**, which provided code examples, AVX2 integration advice, multi-threading strategies, and documentation guidance.

---

## Optional Tips

* Use **NVMe SSDs** for maximum throughput when scanning large directories.
* Adjust **threading and buffer sizes** in the source code for specific workloads.
* Avoid running other heavy disk-intensive tasks during large scans to maintain speed.

