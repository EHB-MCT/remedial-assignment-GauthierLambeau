C# Coding Conventions – Unity Tycoon Project

1. Naming Conventions

Variables:
Use camelCase for all variable names.
Example: playerMoney, buildingCount, upgradeList

Constants:
Use UPPERCASE_SNAKE_CASE for all constants.
Example: MAX_BUILDINGS, DEFAULT_BUILDING_COST

Methods and Functions:
Use PascalCase (capitalize each word without underscores).
Example: PlaceBuilding(), SaveGameState(), RestoreUpgrade()

Classes and Structs:
Also use PascalCase.
Example: BuildingManager, UpgradeManager, MoneyManager

Namespaces:
Use PascalCase, matching folder structure if applicable.
Example: TycoonGame.Utils, TycoonGame.Data

2. Indentation & Formatting

Indentation:
Use 4 spaces per indentation level. Avoid tabs to ensure consistent formatting across different environments.

Line Length:
Limit lines to 100 characters maximum. Break long lines logically when needed (e.g., parameter lists).

3. Comments

Single-line comments:
Use // to explain specific lines or quick notes.

Documentation comments:
Use /** ... */ before classes or important methods to describe:

Their responsibility or purpose

Parameters and return values (if applicable)

Business logic details if non-trivial

Example:

csharp
/**
 * Applies all purchased upgrades to the given building instance.
 * @param building: The Building instance to update.
 */
public static void ApplyUpgradesToBuilding(Building building) { ... }


4. File Structure

One class per file:
The filename must match the public class it contains.
Example: UpgradeManager.cs contains the UpgradeManager class.

5. Additional Rules

Short methods with single responsibility:
Keep methods concise and focused. If a method grows too long or handles multiple tasks, refactor it into smaller reusable methods.

Error handling:
Use try-catch blocks for error-prone code (like database access or parsing) and log exceptions using Debug.LogException() or a centralized logger if available.

Debugging and Logs:
Use Debug.Log() only during development or debugging. Remove or disable debug logs in the production version.

6. Unity Tycoon Project Specifics

Managers (e.g., MoneyManager, UpgradeManager) must be accessible as Singletons (Instance property).

Persistent data methods (Save/Load) should be clearly documented and separated.

Naming conventions for database (e.g., Firestore) keys should be consistent, for example all lowercase snake_case or camelCase — just keep it uniform:
Example: "money", "buildings", "purchased_upgrades".

Where to place this document
Add these conventions in a CONVENTIONS.md file or at the beginning of your README.md so your reviewer knows you follow professional standards.

Practical tip
Perform regular code reviews to ensure new scripts comply with these conventions and mention this code of conduct in your README to demonstrate following course and professional standards.