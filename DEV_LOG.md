# Habit Tracker Log

## My Project structure 

- Models/Habit.cs : Includes the data for a single habit. Doesn't include any logic or validation or I/O

- Services / HabitMAnager : responsible for all the logic. No Input or output is allowed in there. Jst pure logic 
- UI/ HabitInput and HabitOutput : resposible for all user interactions. Get readings from user and print to console.
- Utilities/ InputValidator : contains generic validation logic
- HabitApp.cs : the controller class, the coordinator or the glue. it controls the flow of all these layers together. Takes input from UI, delegates to services for applying logic and finaly delivers to user through console.

        Input -> Logic -> Output

### HabitTracker/


├── HabitTracker.csproj

├── Program.cs

├── HabitApp.cs

├── Models/                  
│   └── Habit.cs


├── Services/                
│   └── HabitManager.cs
 

├── Utilities/               
│   └── InputValidator.cs


├── UI/                 
│   └── HabitInput.cs

│   └── HabitOutput.cs


## TO DO
### Essential Functionality
- [x] Revisit OOP Concepts

- [x] intial Project Structure
- [x] Take input from the user 
- [x] Make id automatic increment and not visible for the user to use it for deletion
- [x] Name shouldn't accept digits or special characters
- [x] Validation and null string thing
- [x] Continue on HabitFrequency User input (on HabitFrequency branch)
- [x] Work on Delete Habit Functionality
- [x] Work on Edit Habit Functionality

### Daily Operation on habit
- [x] Mark habit as complete

- [x] Undo completion
- [x] view all habits for a specific day
- [x] get habits from a JSON file
- [x] improve and edit all viewHabit/s functions and reduce any redundancy

### Refactoring from static to instance-based using controller pattern
- [x] Understand MVC/controller pattern a bit more , Add notes to Leanring Log

- [x] finish refactoring the code to new pattern
- [x] Error Handeling in HabitManager for print Statements in HabitApp
- [x] Notify user with all every action takes with print statements
- [x] Console.WriteLine Still in Business Logic - In HabitManager.TodayHabits()
- [x] EditHabit Has UI Code in Business Logic
- [x] Static Methods Mixed with Instance Methods
- [x] Nullable Return with Wrong Pattern
- [x] Error Handling Missing (if!Exist) - LoadHabitsFromJSON()
- [x] Convert.ToInt32 Without Validation - use try parse
- [x] ref Parameters Still Present - better return not ref
- [x] still an issue in how Edit function flows

### Final Refinments
- [x] add all neccesary comments

- [x] handle any magic numbers
- [x] finish DevLog
- [x] Make A readme


## Daily Log
### [ WednesDay 8-10-2025  03:00 ] :
- Revisit OOP Concepts from your Notes
- Accept user input from Main() for now
- Make id automatic increment and not visible for the user
- improve project structure for Habit Data and Habit manager for services

### [ Thursday 9-10-2025  03:30 ] :
- Moved input from main to a function 
- Validate functions for strings (IsContainNullOrWhiteSpace, IsContainDigitsOrChar)
- Improve structure for validation and user inputs
- Initial work on HabitFrequency User inputs

### [ Friday 10-10-2025  00:45 ] :
- Done with HabitFrequency input

### [ Saturday 11-10-2025  01:36 ] :
- Habit name and Habit Description can accept sentences including spaces.
    (Maybe allow characters like ',', ':',...etc in the description)
- Improve Habit class, remove all private fields and introduce properties for a better visibility
- Implement Delete Habit and sync the ID after deletion

### [ Sunday 12-10-2025  02:30 ] :
- implement Edit Habit functionality
- remove any unnecessary code in functions implementation
- validate user input from menu
- view habits by current day

### [ Monday 13-10-2025  04:15 ] :
- separate all display function in a separate class under UI namespace
- implement mark habit as done as well as undo functionality
- some clean up and improvements
- Initial refactoring using controller pattern

### [ Tuesday 14-10-2025  03:00 ] :
- Done most of refactoring to instance-based approach using contoller pattern
- started working on return status of HabitManager methods

### [ Wednesday 15-10-2025  05:00 ] :
- Error Handeling and status return in HabitManager
- Notify user with all actions taken
- improve UI with color for tables and print statements
- improve and organize HabitOutput a bit more 
- a whole lot refactoring and readjusting return types of getUserChoice methods
- some more separating UI from business logic

### [ Thursday 16-10-2025  03:20 ] :
- handeling nullable and non-nullable warnings
- fix EditHabit logic erorr
- add all neccesary comments
- handle any magic numbers
- finish DevLog 
- Make A readme
