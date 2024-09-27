# WanderVibe
![image](https://github.com/user-attachments/assets/d8e69aed-aa2d-42df-8044-1a649eba0021)

![image](https://github.com/user-attachments/assets/e5d3f3f6-0f12-40fc-9723-45743d86cb1b)

![image](https://github.com/user-attachments/assets/2f9321c6-edd4-4e07-95a1-bafaa6eed796)
WanderVibe is a full-stack web application designed to help users explore and discover travel destinations. It offers a robust destination search system, media management, and an admin dashboard for easy content control.

## Table of Contents
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Folder Structure](#folder-structure)
- [Contributing](#contributing)
- [License](#license)

## Features
- **User Authentication**: Integrated identity management using ASP.NET Identity.
- **Destination Search & Filtering**: Search for destinations based on different criteria.
- **Admin Dashboard**: Admins can manage destinations, user profiles, and uploaded media.
- **File Upload**: Users can upload destination-related images and videos.
- **MVC Architecture**: Structured with the Model-View-Controller (MVC) pattern.
- **Responsive Design**: Optimized for various screen sizes using SCSS and responsive HTML.
- **Interactive Elements**: Utilizes JavaScript to enhance user interaction.

## Tech Stack
- **Backend**: ASP.NET Core MVC, C#
- **Frontend**: HTML5, SCSS (Sassy CSS), JavaScript (for client-side interactivity)
- **Database**: SQL Server or SQLite (can be configured based on environment)
- **Version Control**: Git
- **File Storage**: Local storage for uploaded images, with support for future cloud storage integration

## Installation

### Prerequisites
- .NET 6.0 SDK installed on your machine
- SQL Server or SQLite (as a database)


### Steps to Set Up
1. Clone the repository:
    ```bash
    git clone https://github.com/shane-abh/WanderVibe.git
    cd WanderVibe
    ```

2. Restore dependencies:
    ```bash
    dotnet restore
    ```

3. Set up the database:
    - Configure your connection string in `appsettings.json` for either SQL Server or SQLite.
    - Apply migrations to set up the database:
    ```bash
    dotnet ef database update
    ```

4. Run the application:
    ```bash
    dotnet run
    ```


## Usage

### For Users:
- **Sign Up**: Create an account to save your favorite destinations.
- **Search Destinations**: Use filters like location, price range, and type of activities to find travel ideas.
- **Upload Media**: Share your travel experiences by uploading photos and videos.

### For Admins:
- **Dashboard**: Access the admin panel to manage users, destinations, and media.
![image](https://github.com/user-attachments/assets/ba0b449d-92f1-4a59-a01f-13d405ab8303)
- **Edit Content**: Add, edit, or delete destination details, and manage user permissions.

## Folder Structure

- `Areas/Identity/`: Contains code for user authentication and authorization.
- `Controllers/`: Handles HTTP requests and interacts with the database.
- `Views/`: Razor views that dynamically generate HTML for the frontend.
- `wwwroot/`: Static files such as images, CSS, and JavaScript.
- `Models/`: Database models representing application data.
- `Migrations/`: Manages database schema and updates.

## Contributing
We welcome contributions! Follow these steps to contribute:
1. Fork the repository.
2. Create a new branch with your feature or bug fix.
3. Push your changes and open a pull request.

## License
WanderVibe is licensed under the MIT License. Feel free to modify and distribute the software as per the terms of the license.
Thi



