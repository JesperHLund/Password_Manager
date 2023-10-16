# Password_Manager
Launch the program
fill in the username and password field and press create.
After creating the user you can click the log-in button to log in

![login](https://github.com/JesperHLund/Password_Manager/assets/26818544/14107fdb-a895-41a3-99ff-8a4f3f9c25be)

After the user is made and you have logged in, you will have access to the credential manager.
where each field will have to be filled when you want to save a new credential
fill in Platform, Username and password (Can use "Generate" button to get a random password)
finally you can save the information by pressing the add button


![Credential manager](https://github.com/JesperHLund/Password_Manager/assets/26818544/138f91e4-6ef1-4ce5-97d6-8b612a7a1aa3)

![image](https://github.com/JesperHLund/Password_Manager/assets/26818544/9b28dd1e-6d24-4aad-b73f-82466a2b30a9)


when you are done simply close the application.

The security could most certainly be better with a cloud solutions and more secure storage of the information, as it's currently stored in a JSON file
furthermore the password could be hidden during password creation and log-in, which I sadly forgot in my rush to finish the project.
Currently Bcrpypt is used for password hashing with a factor of 16, this could be increased to make it harder to crack the code
However for expediency it was kept at 16, as my laptop took a minute to hash the password, at that point.
for decrypting the credentials it uses an initialization factor which is stored in the JSON and an encryption key which isn't stored, as it's created based on the username and password
this could be made more secure by making use of the password hash or a secret phrase

another future feature could be the addition of the option to edit and delete old credentials
