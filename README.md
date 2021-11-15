# BlazorHiddenVilla
Complete Blazor hotel Room Management and booking system

SQL Server DB Code First .Net 5

HotelServer: Hosted at: https://hiddenvillaserver.azurewebsites.net/
- Manage rooms CRUD, upload room images CRUD, manage hotel amenities CRUD, Manage and confirm bookings cancelations and no show. 
- Admin email: admin@123.com pw: Admin123@

HotelAPI
- Swagger
- JWT Token
- SignUp / SignIn
- Get and Post HotelRooms
- Stripe payment Integration 

HotelClient - WASM Hosted at: https://hiddenvillaclientfb.azurewebsites.net/
- Check in date selector with number of days - checkout date calculated
- Display available rooms only 
- Booking functionality - pre populating registered user information on booking form - local starage
- Checkout route to stripe payment gateway and back to booking confirmation screen with confirmation email confirming booking email via mailjet.
- Toastr Alerts

