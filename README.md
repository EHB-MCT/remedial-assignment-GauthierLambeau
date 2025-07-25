[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/BhMy8Rjk)
# Tycoon Simulator

## Project Overview

This Unity project is a simple tycoon-style game where players place buildings that generate money over time. The player uses the generated money to purchase more buildings and various upgrades that boost income. Players start by entering a username which links their progress to an online database (Firebase Firestore). If the username exists, the saved session is loaded so players can continue their previous game with saved money, buildings, and upgrades.

## How to Run
1.Open the project in Unity (version 2021.3 LTS or later recommended).

2.Open the scene Scenes/Game.unity.

3.Press "Play" to start the game.

4.Enter a username to load or start a new session.

5.Place buildings, earn money, and purchase upgrades to progress.

## How to Develop
### Prerequisites
Unity 2021.3 LTS or later

Basic C# and Unity knowledge

Firebase account and set up for Firestore database

### Development Setup
Clone the repository:

text
git clone <https://github.com/EHB-MCT/remedial-assignment-GauthierLambeau>
Open the project with Unity Hub.

Configure Firebase in the project by adding google-services.json (Android) or GoogleService-Info.plist (iOS) inside Assets/.

Key Development Tasks
Building System: Manage building prefabs under Assets/Level/Prefabs.

Money & Upgrade System: Scripts located in Assets/Scripts, manage the economic simulation.

Firebase Integration: Located in Assets/Scripts/FirebaseSaver.cs and Assets/Scripts/FirebaseInitializer.cs. Responsible for saving and loading player data with Firestore.

AutoSave & Session Management: Auto-save player progress on quit and load data on session start.

Testing
Run the game in Unity editor and simulate gameplay.

Verify that placing buildings increases income per second.

Purchase upgrades and verify income change and UI update.

Check Firestore database for saved player data accuracy.

Exit and relaunch using the same username to verify that the saved session loads correctly.

## Folder Structure
text
Assets/
├── Art/ 
│   ├── Materials
│   ├── Model
│   └── textures 
├── Code/
│   └── Scripts.cs
│       ├── AutoSaveManager.cs
│       ├── Building.cs
│       ├── BuildingPlacer.cs
│       ├── BuildingUIElements.cs
│       ├── FirebaseInitializer.cs
│       ├── FirebaseSaver.cs
│       ├── IncomeDisplay.cs
│       ├── Moneymanager.cs
│       ├── PlayerData.cs
│       ├── PlayerLogin.cs
│       └── UpgradeManager.cs
├── Editor Default Resources
├── ExternalDependencyManager
├── Firebase
├── Level/
│   ├── Prefabs/         
│   └── Scnenes/         
│       ├── Game.unity
│       └── Previous save version.unity  
├── Plugins
├── StreamingAssets
├── TextMesh Pro 
├── docs    
└── 
Packages/                 # Other needed packages

## Documentation
The project follows a modular structure with singleton managers for core logic (MoneyManager, UpgradeManager, FirebaseSaver).

Persistent data is stored in Firebase Firestore. Data structure includes:

money: Player's current money.

buildings: Dictionary of building types and their counts.

upgrades: List of purchased upgrade names.

Upon entering a username, the system queries Firestore to load existing data or starts a fresh session if none exists.

AutoSaveManager triggers data save when the application closes, ensuring progress persistence.

## Key Scripts
- `PlayerLogin.cs`: Manages username input and session loading.

- `MoneyManager.cs`: Tracks player's money and updates the UI.

- `Building.cs`: Defines behavior of buildings generating money per second.

- `BuildingPlacer.cs`: Handles user building placement and costs.

- `UpgradeManager.cs`: Manages upgrades purchase and application to buildings.

- `FirebaseSaver.cs`: Responsible for interfacing with Firebase Firestore to save/load player data.

 - `AutoSaveManager.cs`: Auto-saves progress on application quit.

- `FirebaseInitializer.cs`: Initializes Firebase SDK and checks dependencies.

 ## References
- AI Assistance: Used [ChatGPT](https://chatgpt.com/share/688257af-2470-8005-b71c-607ccf34a983) for coding assistance and debugging during development.

- Firebase Implementation: Followed official [Firebase](https://firebase.google.com/docs/) Unity tutorials and documentation.

- Project Inspiration: Tycoon game concept for a educational purposes.

## Attribution & License
This project was developed by Gauthier Lambeau.
Licensed under the MIT License — see LICENSE.md file for details.
