# GringottsBank
Gringotts Bank is a bank that has an online branch for wizards to do some account transactions

# Access Instructions
1. Make clone of this repository by using command "git clone https://github.com/adityaBisht2304/GringottsBank.git". It will copy the code in a local folder.
2. Go to folder "ReositoryLocalFolderPath/GringottsBank/" and open "GringottsBank.sln" in Visual Studio 2019
3. Build the solution and run GringottsBank.exe from path "ReositoryLocalFolderPath/GringottsBank/bin/Debug/net5.0"
4. It will open up the Swagger UI with all the APIs available accross different REST Endpoints. Run the following APIs in sequence for authentication
   1. POST /v1/api/Customer/register 
      1. Provide Request Body correctly
   2. POST /v1/api/Customer/login 
      1. Provide Request Body correctly
      2. Copy the token generated - Let's say "abc"
   3. Go to Authorize Section at the top right of the Swagger UI
      1. Type "Bearer abc" or in other words "Bearer TokenNumberGenerated"
      2. No double quotes are to be put
   4. The authentication which is already done will ensure only the authorized user can access the APIs
5. We can also run the APis from Postman for registration and authentication
   1. POST https://localhost:5001/v1/api/Customer/register 
      1. Provide Request Body correctly
   2. POST https://localhost:5001/v1/api/Customer/login
      1. Provide Request Body correctly
      2. Copy the token generated - Let's say "abc"
   3. Go to Authorization Section and select the Bearer token type and paste the token
   4. Now we can use all those APIs which do not require admin access

# API Instructions

