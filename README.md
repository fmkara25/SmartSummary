# 🧠 SmartSummary — AI Text Summarizer (ASP.NET Core MVC)

A simple web application that summarizes user-provided text using the **OpenAI API**.  
Built with **ASP.NET Core MVC** (C#) and a clean service-based architecture.

## 🚀 Live Demo
- Demo: https://smartsummary-0rca.onrender.com

## ✨ Features
- Paste any text and get a concise summary
- Choose summary length (sentence count)
- Optional output language (Turkish / English)
- Server-side OpenAI integration via a dedicated service layer

## 🧱 Tech Stack
- C# / ASP.NET Core MVC
- OpenAI API
- Razor Views

## 🗂️ Project Structure
- `Controllers/` — MVC controllers (request handling)
- `Views/` — UI pages (Razor)
- `Services/` — `OpenAiService.cs` (OpenAI calls)
- `Models/` — ViewModels and models

## ▶️ Run Locally
### Requirements
- .NET SDK (recommended: latest LTS)
- An OpenAI API key

### Setup
1. Clone the repository
2. Set your OpenAI API key (see below)
3. Run the project

```bash
dotnet restore
dotnet run
