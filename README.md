# Landscaping Time Reporting
## Getting Started

The following items must be completed before the code can be compiled and run successfully.

## Visual Studio[]

-   Visual Studio 2022

## Extensions and Updates

-   Install .NET Core 6 SDK  [https://dotnet.microsoft.com/en-us/download/dotnet/6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
-   Install Node 18.16.0.
-   Install the latest Angular Cli "npm i -g @angular/cli@latest"

## Database

-   SQL Server Express (Latest version)
    -   Install using default instance name of SQLExpress
-   Launching the application will run a set of migrations to create the database.

## Adding Yourself as a User
- The default user has the following credentials:
	- Username: admin
	- password: admin
- If you would like to add your own user, either through testing or otherwise, it must be done either through the UI, or by using the Swagger Web API. 
### Adding an employee through Swapper Web API
- In the Employee's Accordion, select the Post to /api/Employees/Employee:
- Click "Try it out"
- Remove the "id: 0" field
- Change information Note: employeeTypeId must be valid Id in EmployeeType table
- Click "Execute"



# Running the Application

1.  Set Startup Projects
    -   Right click on LandscapingTR solution and go to Properties -> Common Properties -> Startup Project and choose  LandscapingTR.Web and  LandscapingTR.Web.API as the startup projects.
2.  Launch the application in debug mode (F5) to begin test the application.  
    NOTE: If needed From the   LandscapingTRWeb/ClientApp folder run the following
    -   `npm install --force`
    -   NOTE: Run  `ng build`  to view and correct any compilation errors.
3. There are a lot of problems with npm
   npm npm-check-updates -u
   npm install typescript@">=4.8.2 and <4.9.0" --save-dev

4. DO NOT INSTALL ANY MORE NODE PACKAGES:
THIS THING WORKS ON MY MACHINE AND TAKES AN HOUR TO FIX 
   

