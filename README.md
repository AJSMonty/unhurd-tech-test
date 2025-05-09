## 🎵 Unhurd Tech Test

Welcome to the Unhurd Tech Test repo — a full-stack project built with an ASP.NET Core (C#) backend, a Vite/React frontend, and deployed to Azure.
The app allows users to sign-in/create an account, and then create and manage a list of tasks.

# https://wonderful-meadow-02d574f03.6.azurestaticapps.net/

## 🗂️ Project Structure

```
unhurd-tech-test/
├── client/                         # Frontend (React + TypeScript)
│   └── src/
│       ├── components/             # Reusable shared UI components, including authentication logic
│       ├── features/               # Feature-based folders with their own components and hooks
│       ├── models/                 # TypeScript type definitions and interfaces
│       ├── network/                # Network service classes for each feature (API calls)
│       ├── pages/                  # Top-level routed pages (e.g. Login, Home)
│       ├── routing/                # Centralized routing logic using React Router
│       ├── styles/                 # Global SCSS styling and theming
│       └── utilities/              # Utility functions, including Axios instance setup
│
├── server/                         # Backend (ASP.NET Core Web API)
│   ├── Common/                     # Cross-cutting concerns and shared logic
│   │   ├── Middleware/             # Custom middleware for request/response handling
│   │   ├── Results/                # Standardized response/result types
│   │   └── Web/                    # Helpers for translating internal results into HTTP responses (e.g. IActionResult factory)
│   |
│   ├── Configurations/             # Strongly-typed configuration models (e.g. Cosmos DB connection and container settings)
│   |
│   ├── Features/                   # Core application features (grouped by domain)
│   │   ├── Accounts/               # Handlers, requests, and models for account-related logic
│   │   └── PromoTasks/             # Handlers, requests, and models for promo task logic
│   |
│   ├── Repositories/               # Data access layer (interfaces and EF Core implementations)
│   |
│   └── Services/                   # Application-level services and startup tasks
│
└── .github/workflows/              # GitHub Actions CI/CD workflows
```

## 🚀 Getting Started

# ✅ Prerequisites

.NET 8 SDK

Node.js + npm

# 🧪 Running the API Locally

```
cd server
dotnet restore
dotnet dotnet run --launch-profile https
```

The API will start at https://localhost:7166

# 🌐 Running the Frontend

```
cd client
npm install
npm run dev
```

## 🛠️ CI/CD Pipeline

This repo includes 2 GitHub Actions workflows that:

- Builds the .NET Core API
- Deploys to Azure App Service (unhurd-tech-test-api) on every push to main
- File: .github/workflows/main_unhurd-tech-test-api.yml

- Builds the Vite/React App
- Deploys to Azure Static Web Apps (unhurd-tech-test-frontend) on every push to main
- File: .github/workflows/prod_build_deploy.yml

## 🤝 Maintainer

Made by AJSMonty
