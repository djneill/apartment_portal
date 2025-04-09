# aPad - Apartment Portal

## Overview

aPad is a modern platform designed to improve tenant experience and streamline operations for apartment management teams. It enables administrators to manage tenants, track reported issues, and handle lease signings, while providing tenants with powerful features to enhance their living experience.

### Business Problems Solved

This project solves several challenges in the property management industry:

- **Tenant Experience & Retention**: By digitizing and simplifying essential tenant interactions.
- **Digital Lease Signing & Renewals**: Handle new and renewed leases within the app.
- **Smart Lock Integration**: Tenants can control smart locks and generate temporary guest access keys.
- **Issue Reporting**: One-tap reporting for noise complaints or neighbor disturbances.
- **AI Complaint Analysis**: AI-powered detection of repeated complaints with escalation to management.
- **Package Management**: Secure package pickup through smart locker integrations.
- **Guest Parking Management**: Issue temporary guest parking permits directly from the app.

### About the Project

This project was developed during a 6-week cohort led by two professional software developers. Out of 600+ applicants, a selected team collaborated to build a project that solves real-world business problems. Participants gained practical experience working in a team environment while receiving mentorship from industry professionals.

---

## Getting Started

Follow these steps to clone and run the project locally:

### 1. Prerequisites

- Node v22.7.0
- NPM 10.8.2
- .NET 8.0 SDK
- .NET EF Core tool 

### 2. Clone the Repository

```git clone https://github.com/dgarcia-appdev/apartment_portal.git```

### 3. Environment Variables

#### 1. Database Connection:
- Place the DB connection string in the backend, under ./appsettings.development.json or user secrets
- Ensure the connection string is not being tracked by git
- Your connection string will be different

```
"ConnectionStrings": {
  "DefaultConnection": "Server=myServerAddress;Database=myDatabase;Trusted_Connection=True;"
}
```

#### 2. Gemini API Key (Optional)
- The API key must be for Gemini 2.0 Flash Lite
- Place the API Key in the backend, under ./appsettings.development.json or user secrets
- Ensure the API key is not being tracked by git

```
"AIKey": "YOUR_API_KEY"
```

### 4. HTTPS Certificate

1. Install mkcert
   ### Windows:
   ```choco install mkcert```
  
   ### MacOS:
   ```brew install mkcert```

2. Install Certificate

   ```mkcert -install```
3. Navigate Directories (From Project Root Directory)

   ```cd .\frontend\```
4. Create Certificate

   ```mkcert localhost```
5. Trust Certificate (MacOS only)

   ```dotnet dev-certs https --trust```

### 5. Create Database
1. Navigate Directories (From Project Root Directory)

   ```cd .\apartment_portal_api\apartment_portal_api\```

2. Apply DB Migrations

   ```dotnet ef database update```

### 6. Running the Application
Two Terminals required

  #### Backend
  1. Navigate Directories (From Project Root Directory)

     ```cd .\apartment_portal_api\```

  2. Run

     ```dotnet run --project=apartment_portal_api --launch-profile=https```

  #### Frontend
  1. Navigate Directories (From Project Root Directory)

     ```cd .\frontend\```

  2. Run

     ```npm run dev```

---

## Technologies Used

- React / Vite
- TypeScript
- ASP.NET Core
- Entity Framework Core
- ASP.NET Core Identity
- PostgreSQL

---

## Contributors

- **Dennis Garcia** – Team Lead  
- **Aaryan Das** – Team Lead  
- **DJ Neill** – Frontend  
- **Zeeshawn Marshall** – Backend  
- **Ayomide Omotosho** – Frontend  
- **Johan Gilces Reyes** – Frontend  
- **Felipe Teixeira Andrade** – Full Stack  
- **Casey Dietz** – Full Stack  
- **David Ogden** – Backend  

---

