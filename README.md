Potty Map Back-End

Controllers / folder
    - UserController / file
        - Create user / endpoint        | C
        - Login user / endpoint         | R
        - Update (Edit) user / endpoint | U
        - Delete user / endpoint        | D

    - BathroomController / file
        - Create Bathroom / endpoint | C
        - Get Bathroom / endpoint    | R
        - Update Bathroom / endpoint | U
        - Delete Bathroom / endpoint | D


Services / folder
    - Context / folder
        - DataContext / file (responsible for defining the structure of our database and the tables it will hold)
    - UserService / file
        - Create user / function        | C
        - Login user / function         | R
        - Update (Edit) user / function | U
        - Delete user / function        | D
    - BathroomService / file
        - Create Bathroom / function   | C
        - Get Bathroom / function      | R
        - Update Bathroom / function   | U
        - Delete Bathroom / function   | D
    - PasswordService / file
        - Hash Password
        - Verify HashPassword


Models / folder
    - UserModel / file
        - Every user will need:
            - int ID
            - string Username
            - string Salt
            - string Hash
            - bool IsModerator
            - string favorite potty spots? 
                - maybe an object?

    - BathroomModel / file (a model for each one of our bathrooms)
        - Every bathroom will need:
            - int ID
            - int UserID (for the user who posted the bathroom) ?????
            - string Location
            - string Title ?????
            - string Gender
            - string Type
            - string Number of Stalls
            - string Wheelchair Accessibility
            - string Hours of Operation
            - string Open to Public
            - string Key required
            - string Baby changing station
            - string Cleanliness
            - string Safety
            - string Rating?
            - bool IsDeleted (soft delete, still in database. can be recoverable. this is how you mark something as deleted so it won't be displayed, but the data remains just in case the user wants to restore it later or you need to provide that information to law enforcement)

    - DTOs / folder (Data Transfer Object - works as an intermediary between models)
        - LoginDTO / file
            - string Username
            - string Password
        - CreateAccountDTO / file
            - int ID = 0 (we begin making accounts starting with ID of 0 and increment by one each time)
            - string Username
            - string Password
        - PasswordDTO / file
            - string Salt
            - string Hash