# CarInsurance Application

## Overview
The CarInsurance application is an ASP.NET Core MVC project built with Entity Framework Core for managing car insurance quotes. It fulfills the requirements of Assignment Part 4, including quote calculation logic, a user input form, an admin view, and a visually enhanced UI with a 3D geometry background and smooth scrolling.

### Features
- **Quote Calculation**: Calculates insurance quotes based on user inputs:
  - Base price: $50/month.
  - Age adjustments: +$100 (≤18), +$50 (19–25), +$25 (≥26).
  - Car year: +$25 if <2000 or >2015.
  - Car make/model: +$25 for Porsche, +$25 more for Porsche 911 Carrera.
  - Speeding tickets: +$10 per ticket.
  - DUI: +25% to total.
  - Full coverage: +50% to total.
- **Create View**: A form for users to input details (first name, last name, email, date of birth, car details, DUI status, speeding tickets, coverage type). The quote is calculated automatically and not editable by users.
- **Admin View**: Displays all issued quotes with first name, last name, email, and quote amount.
- **UI Enhancements**:
  - 3D geometry background using Three.js (rotating torus knot).
  - Smooth scrolling with CSS (`scroll-behavior: smooth`).
  - Form animations using Animate.css and custom CSS for a staggered slide-in effect.
  - Glassmorphism styling for a modern, semi-transparent look.
- **Database**: Uses SQL Server LocalDB (`InsuranceDb.mdf` and `InsuranceDb_log.ldf`) to store insuree data.

