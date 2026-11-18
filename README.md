# 🏎️ Car Show Registration System

<div align="center">

<img src="https://readme-typing-svg.demolab.com?font=Poppins&weight=600&size=22&duration=3000&pause=1000&color=6366F1¢er=true&vCenter=true&width=780&lines=Welcome+to+Car+Show+Registration+System;Full-Stack+Event+%26+Participant+Management;Track+Registrations%2C+KPIs%2C+Payments+%26+Statuses;React+%2B+Vite+%2B+Tailwind+CSS+%7C+ASP.NET+Core+Vertical+Slice" alt="Typing SVG" />

**Register. Track. Filter. Manage.**

A full-stack web application designed to streamline participant registrations, track payment statuses, monitor event KPIs, and manage vehicle entries for a car show event.

🌐 **Live Frontend Demo:** [https://car-show-registration-system-edma-f.vercel.app/](https://car-show-registration-system-edma-f.vercel.app/)

</div>

---

## 📖 About the Project

The **Car Show Registration System** is a full-stack event management platform built to simplify how organizers and participants handle car show registrations. Instead of manual tracking, the system provides a centralized dashboard where users can submit new entries, monitor real-time statistics, filter participants by registration type or status, and inspect detailed registration and payment records.

This project demonstrates end-to-end full-stack development and team collaboration across two core layers:

- **🎨 Frontend Development:** Responsible for the responsive user interface, interactive dashboard, KPI cards, live search and filtering, pagination, modal workflows, and API state handling.
- **⚙️ Backend Development:** Responsible for RESTful API endpoints, Vertical Slice Architecture, CQRS command/query handlers, input validation, fee calculations, and SQLite database persistence via Entity Framework Core.

---

## 👨‍💻 Developers & Roles

| Role | Developer | Core Responsibilities |
|---|---|---|
| **🎨 Frontend Developer** | **Gineva D. Edma** | UI/UX design, React + Vite component architecture, Tailwind CSS styling, KPI dashboard, search/debounce filtering, pagination, and frontend deployment on Vercel. |
| **⚙️ Backend Developer** | **Harold C. Fulgar** | ASP.NET Core Web API, Vertical Slice Architecture, CQRS handlers, FluentValidation rules, SQLite + EF Core migrations, data seeding, and API documentation. |

---

## ✨ Features and Functionalities

### 📊 1. Real-Time Event KPI Dashboard

Provides organizers with an immediate overview of key event metrics right at the top of the dashboard.

**Functionalities:**
- Displays summary statistics for total registrations, confirmed entries, pending approvals, and payment breakdowns.
- Dynamically updates metric cards whenever new registrations are added or data is refreshed.

**Technology used:** ⚛️ React, ⚡ Vite, 🎨 Tailwind CSS, and 🔗 REST API (`GET /api/registrations/summary`).

**Components:** `KpiCards.jsx`, `KpiCard.jsx`, `HeroSection.jsx`

---

### 📝 2. New Participant Registration Modal

Allows users or event staff to register new vehicles and participants through a structured, validated modal form.

**Functionalities:**
- Collects participant information, vehicle details, and registration category (`RegistrationType`).
- Automatically computes corresponding registration fees based on entry rules.
- Submits validated entries directly to the backend database and updates the dashboard list.

**Technology used:** ⚛️ React, 🎨 Tailwind CSS, 🟣 C# / ASP.NET Core, and ✅ FluentValidation.

**Components & Slices:** `NewRegistrationModal.jsx` | `CreateRegistrationCommand.cs`, `CreateRegistrationValidator.cs`

---

### 🔍 3. Debounced Search & Multi-Criteria Filtering

Makes locating specific entries fast and responsive even as the participant list grows.

**Functionalities:**
- Supports real-time search by participant or entry name using a custom debounce hook to minimize unnecessary API calls.
- Filters entries by **Registration Type** and **Registration/Payment Status**.
- Includes quick reset options and graceful empty-state feedback when no records match.

**Technology used:** ⚛️ React Custom Hooks (`useDebounce`), 🎨 Tailwind CSS, and 🟣 ASP.NET Core Query Handlers.

**Components:** `RegistrationFilters.jsx`, `useDebounce.js`, `EmptyState.jsx`

---

### 📋 4. Interactive Registration Table & Pagination

Organizes participant records in a clean, readable table with structured status indicators and page navigation.

**Functionalities:**
- Displays participant details, registration types, payment badges, and approval statuses.
- Implements client/server-friendly pagination for smooth browsing across multiple pages of records.
- Uses color-coded status and payment badges for quick visual scanning.

**Technology used:** ⚛️ React, 🎨 Tailwind CSS, and 🟨 JavaScript Formatters.

**Components:** `RegistrationTable.jsx`, `Pagination.jsx`, `StatusBadge.jsx`, `PaymentBadge.jsx`, `formatters.js`

---

### 🔎 5. Registration Details Inspection Modal

Lets organizers view complete information for any selected car show entry without leaving the main dashboard.

**Functionalities:**
- Fetches and displays full participant, vehicle, fee, and status details for a specific registration ID.
- Provides a clean modal view for quick verification during event check-ins.

**Technology used:** ⚛️ React, 🎨 Tailwind CSS, and 🔗 REST API (`GET /api/registrations/{id}`).

**Components & Slices:** `RegistrationDetailsModal.jsx` | `GetRegistrationByIdQuery.cs`, `GetRegistrationByIdQueryHandler.cs`

---

### 🛡️ 6. Resilient UI States (Loading, Empty & Error Handling)

Ensures a smooth and informative user experience across different network and data conditions.

**Functionalities:**
- Displays skeleton/loading indicators while fetching data from the backend API.
- Renders dedicated empty-state prompts when search filters return zero matches.
- Displays clear error-state alerts with recovery options if the backend service is unreachable.

**Technology used:** ⚛️ React and 🎨 Tailwind CSS.

**Components:** `LoadingState.jsx`, `EmptyState.jsx`, `ErrorState.jsx`

---

### 🏗️ 7. Vertical Slice Backend & Automated Seeding

Structures backend operations around isolated feature slices for clean maintainability and scalability.

**Functionalities:**
- Separates commands (`CreateRegistration`) and queries (`GetRegistrations`, `GetRegistrationById`, `GetRegistrationSummary`) into self-contained Vertical Slices.
- Enforces domain rules using typed Enums (`RegistrationType`, `RegistrationStatus`, `PaymentStatus`) and `Result` pattern extensions.
- Automatically initializes and seeds sample car show data into SQLite on startup via Entity Framework Core migrations.

**Technology used:** 🟣 C#, 🌐 ASP.NET Core, 🗄️ Entity Framework Core, 💾 SQLite, and ✅ FluentValidation.

**Backend Modules:** `RegistrationEndpoints.cs`, `AppDbContext.cs`, `RegistrationSeeder.cs`

---

## 📸 Screenshots and Project Evidence

The screenshots inside `docs/screenshots_frontend/` document the user interface, state handling, and full-stack integration:

| Evidence | Description | Preview |
|---|---|---|
| **Folder Structure** | Organized frontend and backend workspace layout | `docs/screenshots_frontend/01-folder-structure.png` |
| **Dashboard & KPI Cards** | Overview of event metrics and main header | `docs/screenshots_frontend/02a-dashboard-kpiCards.png` |
| **Table & Pagination** | Participant table with status badges and page controls | `docs/screenshots_frontend/02b-table-pagination.png` |
| **Loading State** | Visual feedback while awaiting API responses | `docs/screenshots_frontend/03-loading-state.png` |
| **Search & Filters** | Live search by name, registration type, and status | `docs/screenshots_frontend/04-search-by-name.png` |
| **Empty & Error States** | Graceful fallback screens for empty queries and API errors | `docs/screenshots_frontend/07-empty-state.png` |
| **DevTools Network** | Verified HTTP requests between React frontend and ASP.NET Core API | `docs/screenshots_frontend/09-browser-devtools-network.png` |
| **Running Full App** | Complete end-to-end application running locally | `docs/screenshots_frontend/11-running-full-app.png` |

---

## 🛠️ Technologies Used

### 🎨 Frontend Stack (Developed by Gineva D. Edma)

| Technology | Purpose |
|---|---|
| ⚛️ **React** | Component-based user interface architecture |
| ⚡ **Vite** | Fast frontend build tool and local development server |
| 🎨 **Tailwind CSS** | Utility-first styling for responsive layouts and badges |
| 🟨 **JavaScript (ES6+)** | Application logic, custom hooks (`useDebounce`), and API client |
| ▲ **Vercel** | Cloud hosting for the live frontend deployment |

### ⚙️ Backend Stack (Developed by Harold C. Fulgar)

| Technology | Purpose |
|---|---|
| 🟣 **C# / ASP.NET Core** | Minimal APIs and backend web service framework |
| 🧩 **Vertical Slice Architecture** | Feature-based organization with CQRS commands and queries |
| 🗄️ **Entity Framework Core** | ORM for database modeling, migrations, and queries |
| 💾 **SQLite** | Lightweight relational database for storing registrations |
| ✅ **FluentValidation** | Declarative validation rules for incoming registration requests |

---

## 📂 Project Structure

```text
Car-Show-Registration-System-EDMA-FULGAR-backend-dev/
│
├── frontend/                         # React + Vite + Tailwind CSS Application (Fulgar)
│   ├── public/                       # Static assets, icons, and fonts
│   ├── src/
│   │   ├── api/                      # Backend API service & fallback sample data
│   │   ├── components/               # Reusable UI components (KPIs, Modals, Table, Badges)
│   │   ├── hooks/                    # Custom React hooks (useDebounce.js)
│   │   ├── pages/                    # Main dashboard view (RegistrationsPage.jsx)
│   │   ├── utils/                    # Currency and date formatters
│   │   ├── App.jsx
│   │   └── main.jsx
│   ├── index.html
│   ├── package.json
│   ├── tailwind.config.js
│   └── vite.config.js
│
├── backend/                          # ASP.NET Core Vertical Slice API (Gineva D. Edma)
│   ├── Api/                          # Endpoints, Filters, Extensions, Program.cs
│   ├── Application/                  # CQRS Commands, Queries, Validators & DTOs
│   ├── Domain/                       # Entities (Registration), Enums & Fee Rules
│   ├── Infrastructure/               # EF Core AppDbContext, Migrations & Seeder
│   └── VerticalSlice.slnx
│
├── docs/
│   ├── Screenshots_backend/          # Backend API execution evidence
│   └── screenshots_frontend/         # Frontend UI & integration screenshots
│
└── README.md
```

---

## 🚀 Installation and Local Setup

### Prerequisites

Make sure the following tools are installed on your machine:
- **Node.js** (v18 or higher) and **npm**
- **.NET SDK** (for running the ASP.NET Core backend)
- **Git**

### 1. Run the Backend API

Open a terminal and navigate to the `backend` directory:

```bash
cd backend
dotnet restore ./Api/Api.csproj
dotnet build ./Api/Api.csproj
dotnet run --project ./Api/Api.csproj
```

The backend API will start locally and automatically apply Entity Framework Core migrations and seed sample registration records into SQLite.

### 2. Run the Frontend Application

Open a separate terminal and navigate to the `frontend` directory:

```bash
cd frontend
npm install
npm run dev
```

Open the local development URL shown in your terminal (typically `http://localhost:5173`), or visit the deployed frontend at **[https://car-show-registration-system-edma-f.vercel.app/](https://car-show-registration-system-edma-f.vercel.app/)**.

---

## 🔌 Backend API Endpoints

| HTTP Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/registrations` | Retrieves the list of car show registrations with optional filtering. |
| `GET` | `/api/registrations/{id}` | Retrieves detailed information for a specific registration entry. |
| `GET` | `/api/registrations/summary` | Returns aggregated KPI counts and fee totals for the dashboard. |
| `POST` | `/api/registrations` | Validates and creates a new car show registration record. |

---

## 🎓 Academic Project Information

| Information | Details |
|---|---|
| **Project Title** | Car Show Registration System |
| **Live Frontend URL** | [https://car-show-registration-system-edma-f.vercel.app/](https://car-show-registration-system-edma-f.vercel.app/) |
| **Frontend Developer** | Gineva D. Edma |
| **Backend Developer** | Harold C. Fulgar |
| **Frontend Stack** | React, Vite, Tailwind CSS |
| **Backend Stack** | C#, ASP.NET Core (Vertical Slice Architecture), EF Core, SQLite |

---

<div align="center">

**🏎️ Car Show Registration System**

*Built through collaborative frontend and backend engineering.*

**Developed by:** Gineva D. Edma (Frontend) & Harold C. Fulgar (Backend)  
**Course:** BS Information Technology — BSIT 3D

</div>
