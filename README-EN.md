# WellTool

🍬A set of tools that keep C# sweet.

👉 <https://github.com/tozwei/WellTool> 👈

## 📚Introduction

WellTool is a feature-rich and easy-to-use C# utility library designed to help developers quickly and conveniently complete various development tasks through a variety of practical utility classes.
These encapsulated tools cover a range of operations including strings, numbers, collections, encoding, dates, files, IO, encryption, databases, JSON, HTTP clients, and more,
which can meet various development needs.

## 🎁Origin of WellTool's Name

WellTool = Well + tool, implying the completeness and ease of use of the tool library, while also expressing good wishes for developers.

## 🍺WellTool Philosophy

WellTool is both a toolset and a knowledge base. We never claim original code, as most utility classes are adapted from existing sources. Therefore:

- You can import and use them, or copy and modify them without marking any information, just hope you can timely feedback any bugs found.
- We strive to improve Chinese comments to provide a good learning environment for source code learners, aiming to make everyone able to understand.

## 🛠️Components

A C# basic utility class that encapsulates methods such as files, streams, encryption and decryption, transcoding, regular expressions, threads, XML, etc., forming various Util utility classes, while providing the following components:

| Module | Introduction |
| --- | --- |
| WellTool.Core | Core, including Bean operations, dates, various Utils, etc. |
| WellTool.AI | AI large model encapsulation implementation |
| WellTool.Aop | Dynamic proxy encapsulation, providing aspect support |
| WellTool.BloomFilter | Bloom filter, providing Bloom filters for some Hash algorithms |
| WellTool.Cache | Simple cache implementation |
| WellTool.Captcha | Image captcha implementation |
| WellTool.Cron | Scheduled task module, providing Crontab-like expression scheduled tasks |
| WellTool.Crypto | Encryption and decryption module, providing symmetric, asymmetric and digest algorithm encapsulation |
| WellTool.DB | Data operation, based on ActiveRecord idea |
| WellTool.Dfa | Multi-keyword search based on DFA model |
| WellTool.Extra | Extension module, encapsulation for third parties |
| WellTool.Http | Http client encapsulation based on HttpClient |
| WellTool.Json | JSON implementation |
| WellTool.Jwt | JSON Web Token (JWT) encapsulation implementation |
| WellTool.Log | Log facade that automatically recognizes log implementation |
| WellTool.Poi | Encapsulation for Excel and Word |
| WellTool.Script | Script execution encapsulation, such as JavaScript |
| WellTool.Setting | More powerful Setting configuration file encapsulation |
| WellTool.Socket | Network communication encapsulation based on C# |
| WellTool.System | System parameter call encapsulation (system information, etc.) |

You can import each module individually according to your needs, or import all modules by introducing WellTool.All.

## 📝Documentation

- 📘Chinese Documentation
- 📙API Reference

## 📦Installation

### 📦NuGet

Execute the following command in the package manager console of your project:

```powershell
Install-Package WellTool.All
```

Or add dependency in the project file:

```xml
<PackageReference Include="WellTool.All" Version="8.0.1" />
```

## 🔔️Notes

WellTool supports .NET Framework 4.6+ and .NET Core 3.1+, and has not been tested on other platforms. It cannot guarantee that all utility classes or methods are available.

## 🚽Compile and Install

Visit WellTool's GitHub homepage: <https://github.com/tozwei/WellTool> to download the entire project source code, then enter the WellTool project directory and execute:

```powershell
dotnet build
```

Then you can use NuGet to import it.

## 🏗️Contribution

### 🎋Branch Description

WellTool's source code is divided into two branches, with the following functions:

| Branch | Role |
| --- | --- |
| master | Main branch, used for release versions, consistent with packages submitted to NuGet, does not accept any PR or modifications |
| dev | Development branch, default for the next version, accepts modifications or PR |

### 🐞Provide Bug Feedback or Suggestions

When submitting issue feedback, please indicate the .NET version, WellTool version and related dependency library versions you are using.

- GitHub issue

### 🧬Steps to Contribute Code

1. Fork the project to your own repo on GitHub
2. Clone the forked project (your project) to your local machine
3. Modify the code (remember to modify the dev branch)
4. Commit and push to your own repository (dev branch)
5. Log in to GitHub, you can see a pull request button on your homepage, click it, fill in some instructions, and then submit.
6. Wait for the maintainer to merge

### 📐PR Principles

WellTool welcomes anyone to contribute to WellTool and contribute code. However, the maintainer is an obsessive-compulsive patient. To take care of the patient, the submitted PR (pull request) needs to meet some specifications as follows:

- Complete comments, especially each newly added method should indicate method description, parameter description, return value description, etc. according to C# documentation specifications. If necessary, please add unit tests, and if you wish, you can also add your name.
- WellTool's indentation follows Visual Studio default settings, and the code style is consistent.
- Please try to keep the PR single, one PR only solves one problem or adds one feature, do not submit multiple unrelated modifications in one PR.
- Please ensure all unit tests pass before submitting code.

## 📄License

WellTool adopts the MIT license, please check the [LICENSE](LICENSE) file for details.

## 🙏Acknowledgements

WellTool draws on many excellent open source projects, hereby expressing thanks:

- [Hutool](https://gitee.com/chinabugotech/hutool) - Java utility library
- [NPOI](https://github.com/nissl-lab/npoi) - .NET POI implementation
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) - JSON processing library

## 📞Contact Us

- GitHub: <https://github.com/tozwei/WellTool>
- Gitee: <https://gitee.com/zwell/WellTool>

---

**WellTool, making C# programming sweeter!** 🍬
