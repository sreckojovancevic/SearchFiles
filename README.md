# SearchFiles

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)  
[![.NET](https://img.shields.io/badge/.NET-9.0-512bd4)](https://dotnet.microsoft.com/)  
[![Language: C#](https://img.shields.io/badge/Lang-C%23-orange)](https://learn.microsoft.com/dotnet/csharp/)

A **high‑performance file search utility** written in C# (.NET 9.0) that uses **AVX2 SIMD instructions** and multi‑threading to rapidly search through file contents and directory structures. Great for searching text within large collections of files.

---

## 📌 Features

- 🔍 Recursive directory scanning  
- 📄 Filename pattern matching (`*.txt`, `UPUTSTVO*.pdf`, etc.)  
- ⚡ Multi‑threaded content search using AVX2 SIMD acceleration  
- 📊 Reports **line number** and **byte offset** for matches  
- 🗂 Handles large datasets efficiently  
- 💾 Works on HDD, SSD, and NVMe storage devices

---

## 🚀 Requirements

- Windows or Linux  
- .NET 9.0 SDK  
- AVX2‑capable CPU (e.g., Intel Core i9, AMD Ryzen)  
- Recommended RAM: **≥16 GB** for large datasets

---

## 🛠 Installation

Clone the repository:

```bash
git clone https://github.com/sreckojovancevic/SearchFiles.git
cd SearchFiles
