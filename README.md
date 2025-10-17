
# ✅ Habit Tracker

A console-based habit tracking application built with C# and .NET 9.

![alt text](image-4.png)![alt text](image-3.png)

## 📜 Features
- Create, read, update, and delete habits
- Track habits by day of the week
- Mark habits as complete
- View today's habits
- Persistent storage (JSON)

## 🏰 Architecture


- **Models**: Data structures (Habit class)
- **Services**: Business logic (HabitManager)
- **UI**: User interface (HabitInput, HabitOutput)
- **Utilities**: Helper functions (InputValidator)

Clean separation of concerns with **controller pattern** through HabitApp class that act as the coordinator between different parts of the project

<br>

HabitTracker/


├── HabitTracker.csproj

├── Program.cs

├── **HabitApp.cs**

├── **Models**/      

│   └── Habit.cs


├── **Services**/

│   └── HabitManager.cs
 

├── **UI**/    

│   └── HabitInput.cs

│   └── HabitOutput.cs

├──**Utilities**/   

│   └── InputValidator.cs

## 🚀 What I Learned
My main goal was to revisit OOP concepts but i learned much more on this simple project :
- Writing a clean code with a well-thought layered achitecture in mind
- How to spot achitectural issues and How to refactor
- Separation of Concerns (UI, Business Logic, Data)
- JSON serialization
- Status pattern for operation feedback
- Input validation and error handling

## 🔆 Future Improvements
- Add Database as storage
- Add GUI using WPF 
- add more features like monthly and yearly goals and statistical operations

