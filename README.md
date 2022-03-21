# AJJDHotel
CSCI 3600 Project

This is a web app for a notional hotel which allows customers and administrators to:
- create an account
- search and filter rooms
- view local area info
- book a reservation
- edit a reservation

Additionally, administrators may:
- alter room details
- manage customers' reservations
- generate revenue and occupancy reports

_______________________________________________________________________________________________________________________________________________

Assuming you have .NET Core 5.0.0 or higher installed in your environment, you can run the following command from the project folder to run the app: dotnet run

If you want to test the site with existing credentials, these allow you to access the two roles:

Administrator
username: uxfre@brillionhistoricalsociety.com
pw: P@ssword1

Customer 
username: tyler@example.com
pw: P@ssword1

Otherwise, you can can register a new user, and that user will be added as a customer. To create a new administrator, you have to cross-reference the ASPNetUserRoles, 
ASPNetRoles, and ASPNetUsers tables to manually assign the ADMIN role to the desired user. 
