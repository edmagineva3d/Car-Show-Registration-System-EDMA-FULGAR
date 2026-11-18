Car Show Registration System

A full-stack web application for managing participant registrations, payment tracking, and event statistics for a car show. The system replaces paper forms and shared spreadsheets with a centralized dashboard backed by a REST API.

Live frontend: https://car-show-registration-system-edma-f.vercel.app/

Table of Contents
Overview
Team and Responsibilities
Features
Technology Stack
Project Structure
Installation and Local Setup
API Reference
Screenshots and Project Evidence
Academic Information
1. Overview

The Car Show Registration System allows event organizers to register participants and their vehicles, monitor registration and payment status, and review event totals in real time. Participants may register as Exhibitors, Competitors, or VIPs, and the server determines the corresponding registration fee.

The project is divided into two layers developed in parallel:

Frontend: a responsive dashboard that provides KPI cards, search and filtering, pagination, and registration forms.
Backend: a RESTful API built with Vertical Slice Architecture, command and query handlers, input validation, fee calculation, and SQLite persistence through Entity Framework Core.
2. Team and Responsibilities
Role	Developer	Responsibilities
Frontend Developer	Gineva D. Edma	User interface design, React and Vite component architecture, Tailwind CSS styling, KPI dashboard, debounced search and filtering, pagination, and deployment on Vercel.
Backend Developer	Harold C. Fulgar	ASP.NET Core Web API, Vertical Slice Architecture, command and query handlers, FluentValidation rules, SQLite and EF Core migrations, data seeding, and API documentation.
3. Features
3.1 Event KPI Dashboard

Presents key event metrics at the top of the dashboard.

Shows total registrations, confirmed entries, pending registrations, and payment totals.
Refreshes the metric cards when new registrations are added or data is reloaded.

Frontend components: KpiCards.jsx, KpiCard.jsx, HeroSection.jsx API endpoint: GET /api/registrations/summary

3.2 New Registration Form

Allows event staff to register a participant and vehicle through a validated modal form.

Collects participant details, vehicle details, and the registration type.
Applies the registration fee on the server according to the selected type.
Submits the entry to the backend and updates the dashboard list.

Frontend component: NewRegistrationModal.jsx Backend slice: CreateRegistrationCommand.cs, CreateRegistrationValidator.cs

3.3 Search and Multi-Criteria Filtering

Allows organizers to locate entries quickly as the participant list grows.

Searches by participant name using a custom debounce hook to limit API requests.
Filters by registration type and registration status.
Provides a reset option and an empty-state message when no records match.

Frontend components: RegistrationFilters.jsx, useDebounce.js, EmptyState.jsx Backend slice: GetRegistrationsQuery.cs, GetRegistrationsQueryHandler.cs

3.4 Registration Table and Pagination

Displays registration records in a structured table.

Shows participant details, registration type, payment status, and registration status.
Uses server-side pagination for browsing multiple pages of records.
Uses color-coded badges to distinguish statuses at a glance.

Frontend components: RegistrationTable.jsx, Pagination.jsx, StatusBadge.jsx, PaymentBadge.jsx, formatters.js

3.5 Registration Details View

Allows organizers to inspect a single registration without leaving the dashboard.

Retrieves the complete participant, vehicle, fee, and status details for a registration ID.
Supports quick verification during event check-in.

Frontend component: RegistrationDetailsModal.jsx Backend slice: GetRegistrationByIdQuery.cs, GetRegistrationByIdQueryHandler.cs API endpoint: GET /api/registrations/{id}

3.6 Loading, Empty, and Error States

Provides clear feedback under varying network and data conditions.

Displays loading indicators while data is retrieved from the API.
Displays an empty-state message when a search returns no results.
Displays an error message with a retry option when the backend is unreachable.

Frontend components: LoadingState.jsx, EmptyState.jsx, ErrorState.jsx

3.7 Vertical Slice Backend and Data Seeding

Organizes backend logic by feature rather than by technical layer.

Separates the command (CreateRegistration) and the queries (GetRegistrations, GetRegistrationById, GetRegistrationSummary) into self-contained slices.
Enforces domain rules through typed enumerations (RegistrationType, RegistrationStatus, PaymentStatus) and the Result pattern.
Applies EF Core migrations and seeds sample registrations into SQLite on startup.

Backend modules: RegistrationEndpoints.cs, AppDbContext.cs, RegistrationSeeder.cs

4. Technology Stack
Frontend (Gineva D. Edma)
Technology	Purpose
React	Component-based user interface
Vite	Build tool and development server
Tailwind CSS	Utility-first styling and responsive layout
JavaScript (ES6+)	Application logic, custom hooks, and API client
Vercel	Hosting for the deployed frontend
Backend (Harold C. Fulgar)
Technology	Purpose
C# and ASP.NET Core	Minimal API web service
Vertical Slice Architecture	Feature-based organization with command and query handlers
Entity Framework Core	Object-relational mapping, migrations, and queries
SQLite	File-based relational database for registrations
FluentValidation	Validation rules for incoming requests
5. Project Structure
text
Car-Show-Registration-System-EDMA-FULGAR-backend-dev/
│
├── frontend/                         # React, Vite, and Tailwind CSS application (Edma)
│   ├── public/                       # Static assets
│   ├── src/
│   │   ├── api/                      # API service and fallback sample data
│   │   ├── components/               # KPI cards, modals, table, and badges
│   │   ├── hooks/                    # Custom hooks (useDebounce.js)
│   │   ├── pages/                    # Main dashboard page (RegistrationsPage.jsx)
│   │   ├── utils/                    # Currency and date formatters
│   │   ├── App.jsx
│   │   └── main.jsx
│   ├── index.html
│   ├── package.json
│   ├── tailwind.config.js
│   └── vite.config.js
│
├── backend/                          # ASP.NET Core Vertical Slice API (Fulgar)
│   ├── Api/                          # Endpoints, filters, extensions, Program.cs
│   ├── Application/                  # Commands, queries, validators, and responses
│   ├── Domain/                       # Entities, enumerations, and fee rules
│   ├── Infrastructure/               # AppDbContext, migrations, and seeder
│   └── VerticalSlice.slnx
│
├── docs/
│   ├── Screenshots_backend/          # Backend API test evidence
│   └── screenshots_frontend/         # Frontend and integration evidence
│
└── README.md
6. Installation and Local Setup
Prerequisites
Node.js 18 or later, with npm
.NET SDK 10
Git
Backend
bash
cd backend
dotnet restore ./Api/Api.csproj
dotnet build ./Api/Api.csproj
dotnet run --project ./Api/Api.csproj

On startup, the API applies the EF Core migrations and seeds sample registrations into the SQLite database. The API is available at http://localhost:5205, and the interactive documentation is available at http://localhost:5205/scalar/v1.

Frontend
bash
cd frontend
npm install
npm run dev

Open the address shown in the terminal, typically http://localhost:5173. The deployed version is available at https://car-show-registration-system-edma-f.vercel.app/.

7. API Reference
Endpoints
Method	Endpoint	Description	Success	Errors
POST	/api/registrations	Validates and creates a registration.	201 Created	400, 409
GET	/api/registrations	Returns a paged list of registrations with optional filters.	200 OK	400
GET	/api/registrations/{id}	Returns a single registration.	200 OK	404
GET	/api/registrations/summary	Returns aggregated counts and fee totals for the dashboard.	200 OK	None
Query parameters for GET /api/registrations
Parameter	Default	Rule
participantName	None	Case-insensitive contains match, maximum 150 characters
type	None	Exhibitor, Competitor, or VIP
status	None	Pending, Confirmed, CheckedIn, or Cancelled
page	1	Must be 1 or greater
pageSize	10	Must be between 1 and 50

Results are sorted by registration date, newest first. Filters may be combined.

Business rules
Registration fees are set by the server: Exhibitor PHP 1,500, Competitor PHP 2,500, and VIP PHP 5,000.
The registration number, date, fee, and initial status (Pending) are assigned by the server and are ignored if sent by the client.
A plate number may be registered only once; a duplicate returns 409 Conflict.
The amount paid must be between 0 and the registration fee.
Payment status (Unpaid, Partial, or Paid) is computed from the amount paid and is not stored.
Cancelled registrations are excluded from the monetary totals in the summary.
8. Screenshots and Project Evidence

Frontend evidence is stored in docs/screenshots_frontend/. Backend test evidence is stored in docs/Screenshots_backend/.

Evidence	Description	File
Folder structure	Frontend and backend workspace layout	docs/screenshots_frontend/01-folder-structure.png
Dashboard and KPI cards	Event metrics and page header	docs/screenshots_frontend/02a-dashboard-kpiCards.png
Table and pagination	Participant table with status badges and page controls	docs/screenshots_frontend/02b-table-pagination.png
Loading state	Feedback displayed while awaiting API responses	docs/screenshots_frontend/03-loading-state.png
Search and filters	Search by name, registration type, and status	docs/screenshots_frontend/04-search-by-name.png
Empty and error states	Fallback screens for empty results and API errors	docs/screenshots_frontend/07-empty-state.png
Network requests	HTTP requests between the React frontend and the API	docs/screenshots_frontend/09-browser-devtools-network.png
Running application	Complete application running locally	docs/screenshots_frontend/11-running-full-app.png
9. Academic Information
Item	Details
Project title	Car Show Registration System
Course	Integrative Programming 2
Program and section	BS Information Technology, BSIT 3D
Institution	Computer Communication Development Institute, Sorsogon Campus
Frontend developer	Gineva D. Edma
Backend developer	Harold C. Fulgar
Frontend stack	React, Vite, Tailwind CSS
Backend stack	C#, ASP.NET Core (Vertical Slice Architecture), EF Core, SQLite
Live frontend	https://car-show-registration-system-edma-f.vercel.app/
