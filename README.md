# Hotel Reservation Management System

Welcome to the Hotel Reservation Management System! This project aims to provide a seamless and efficient way to manage hotel room bookings, update prices, manage events, and send email confirmations. The system is designed to improve operational efficiency and enhance user experience.

## Table of Contents

- [About the Project](#about-the-project)
  - [Features](#features)
  - [Built With](#built-with)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
- [Usage](#usage)
  - [User Interface](#user-interface)
  - [Generating Reports](#generating-reports)
- [Acknowledgements](#acknowledgements)

## About the Project

The Hotel Reservation Management System is designed to simplify the process of managing hotel operations. The system allows staff to handle room bookings, update room prices, manage login details, organize events, and send email confirmations efficiently.

### Features

- **Room Booking Management:** Manage customer bookings, including adding, updating, and deleting reservations.
- **Price Update:** Update room prices and generate price update reports.
- **Login Management:** Manage login details for front office and admin staff.
- **Event Management:** Organize and manage hotel events, including sending event-related information to customers.
- **Email Confirmation:** Send email confirmations to customers upon booking and event registration.
- **Report Generation:** Generate detailed PDF reports on bookings, room prices, and events.

### Built With

- **.NET Framework**
- **C#**
- **WPF (Windows Presentation Foundation)**
- **PdfSharp** (for PDF generation)
- **SQL Server** (for database management)
- **SMTP/Email Service** (for sending email confirmations)

## Getting Started

To get a local copy up and running, follow these steps.

### Prerequisites

- Visual Studio with .NET development tools installed.
- SQL Server or SQL Server Express.
- SMTP server or email service for sending confirmations.
# Hotel Reservation Management

## Getting Started

1. **Open the solution file in Visual Studio.**
2. **Update the connection string** in the `app.config` file to point to your SQL Server instance.
3. **Configure the SMTP settings** in the `app.config` file for email confirmations.
4. **Build the solution** to restore NuGet packages.

## Usage

### User Interface

#### Login Screen
Provides secure access for staff to log into the system.

<p align="center">
  <img src="https://raw.githubusercontent.com/Kushanz7/Hotel-Reservation-System/refs/heads/master/HotelReservationSystem/Login.png" alt="Login Screen">
</p>

#### Dashboard
Overview of system functionalities, providing quick navigation links.

<p align="center">
  <img src="https://github.com/Kushanz7/Hotel-Reservation-System/blob/master/HotelReservationSystem/Dashboard.png" alt="Dashboard">
</p>

#### Reservation Management
Manage bookings by entering customer details and confirming reservations.

<p align="center">
  <img src="https://raw.githubusercontent.com/Kushanz7/Hotel-Reservation-System/refs/heads/master/HotelReservationSystem/Revervation.png" alt="Reservation Management">
</p>

#### Event Management
Organize and manage hotel events, including sending information to customers.

<p align="center">
  <img src="https://raw.githubusercontent.com/Kushanz7/Hotel-Reservation-System/refs/heads/master/HotelReservationSystem/Events.png" alt="Event Management">
</p>

## Generating Reports
Use the provided buttons to generate detailed PDF reports on bookings, room prices, and events.

## Acknowledgements
- PdfSharp
- Microsoft .NET
