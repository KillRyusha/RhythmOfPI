
# Rhythm of Pi

## Project Description
**Rhythm of Pi** is a fast-paced running game where the player overcomes obstacles and collects musical notes in a rhythm synchronized with the digits of π. The game’s uniqueness lies in blending mathematics, AI, and adaptive music to create a dynamic and engaging experience.

### Key Features:
- AI-Generated Music: Suno AI dynamically adapts melodies to gameplay.
- Level Generation: A custom neural network analyzes the music and generates levels based on the digits of π.
- Golden Ratio Design: Ensures a visually harmonious experience.
- π-Based Hashing Algorithm: Provides secure data handling.
- Web Integration: Communicates with the server to retrieve and parse JSON data for game elements.

---

## Project Structure
The project follows a clean architecture with modular design, allowing scalability and ease of maintenance. Key design patterns include State Machine, Singleton, Observer, Factory, Decorator, and Web Requests.

### Assets
Contains all game assets, including scripts, prefabs, and resources.

---

### Installers
- `GameInstaller.cs` – Configures and initializes game dependencies using Dependency Injection.

---

### Scripts

#### Core
Contains essential services and utilities that power the game.

##### Services
- `GameSpeedService.cs` – Manages game speed dynamically.
- `ICoroutineRunner.cs` – Interface for coroutine execution.
- `IObjectFactory.cs` – Interface for object factory pattern.
- `IObjectPool.cs` – Interface for object pooling.
- `IPHasher.cs` – Secure data hashing algorithm based on π.
- `JsonParser.cs` – Parses JSON files fetched from the server.
- `LevelSetUpper.cs` – Configures the level based on analyzed music data.
- `NotesPlacer.cs` – Places musical notes dynamically during gameplay.
- `ObjectPool.cs` – Manages object pool to optimize performance.
- `PlatformPlacer.cs` – Places platforms based on the level layout.
- `PlayerAnimationPlayer.cs` – Handles player animations.
- `PlayerInput.cs` – Manages player input and control.
- `PlayerMover.cs` – Controls player movement.
- `PrefabFactory.cs` – Implements the Factory pattern to instantiate prefabs.
- `SongManager.cs` – Controls music playback and synchronization.
- `WebService.cs` – Handles web requests and server communication.

---

#### States
Implements the State Machine pattern to manage game and player states.

##### Game
- `GameEndState.cs` – Handles game over and restart.
- `GameState.cs` – Main gameplay state.
- `GameStateInitializer.cs` – Initializes game states.
- `InitState.cs` – Initializes the game and loads initial data.

##### Player
- `PlayerEndState.cs` – Handles player end-game logic.
- `PlayerInGameState.cs` – Manages player behavior during gameplay.
- `PlayerInitState.cs` – Prepares player for entering the game.
- `PlayerStateInitializer.cs` – Initializes player states.

- `AState.cs` – Abstract base state.
- `IStateInitializer.cs` – Interface for initializing states.
- `StateMachine.cs` – Core state machine logic.

---

#### View
Contains all UI and game element visual components.

- `GenerateSongPanelView.cs` – Dynamically generates song selection panel.
- `LastPlatform.cs` – Handles the last platform during level transition.
- `LastScreenView.cs` – Displays end-screen summary.
- `LevelCardView.cs` – Displays level information.
- `LevelReloader.cs` – Reloads levels dynamically.
- `LineView.cs` – Manages the visual representation of the beat line.
- `LoadingView.cs` – Displays loading progress.
- `LoginPanelView.cs` – Handles login UI and interactions.
- `Note.cs` – Represents individual musical notes.
- `NoteUIController.cs` – Manages note UI updates.
- `Platform.cs` – Defines the platform behavior.
- `Player.cs` – Core player logic and attributes.
- `ProgressBar.cs` – Displays progress.
- `ResultPanelView.cs` – Shows game results.

---

#### Data
Handles data structures and communication with the server.

- `JsonUser.cs` – Manages user data parsed from JSON.
- `ServerJsonData.cs` – Retrieves and parses JSON data from the server.
- `SongElements.cs` – Defines music-related data structures.
- `WebFieldVariant.cs` – Defines variant types for web data.

---

#### Bootstrap
Handles the initial game setup and environment configuration.
- `ACompletableService.cs` – Abstract service for managing asynchronous tasks.
- `Bootstrap.cs` – Main entry point to initialize game services.
- `CurrentSongInfoSingleton.cs` – Implements Singleton Pattern to store current song information.
- `MenuBootstrap.cs` – Sets up the game menu.

---

## Design Patterns Used
### 1. State Machine
- Manages the game and player state transitions smoothly.
- Located in `Scripts/States/`.

### 2. Singleton
- Ensures single instances of services like `CurrentSongInfoSingleton`.
- Located in `Bootstrap/CurrentSongInfoSingleton.cs`.

### 3. Observer
- Notifies UI elements and game logic when changes occur.
- Applied in `PlayerInput` and `SongManager`.

### 4. Factory Pattern
- Used for creating various prefabs dynamically.
- `PrefabFactory.cs` handles this functionality.

### 5. Decorator Pattern
- Applied in `PlayerMover` to extend functionality with new features.

### 6. Web Requests & JSON Parsing
- Fetches game data from the server and parses it using `WebService.cs` and `JsonParser.cs`.

---

## Web and Server Integration
- Data is retrieved from a remote server via RESTful API.
- `WebService.cs` handles requests and responses.
- JSON responses are parsed into relevant data models in `Data/`.

---

## Game Mechanics
- Level Generation: Levels are dynamically generated using a neural network based on the digits of π.
- Adaptive Music: Suno AI analyzes in-game progress and adapts the music accordingly.
- Golden Ratio UI Design: All visual elements align with the golden ratio to ensure balance and beauty.

---

## Getting Started
### Prerequisites
- Unity 2021.3 or later
- .NET 4.x or higher
- Internet connection to access the server

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/RhythmOfPi.git
   ```
2. Open the project in Unity.
3. Configure server URL in `WebService.cs`.
4. Press Play to start the game!

---

## Customization
### Modifying Level Generation
- Update `LevelSetUpper.cs` to tweak generation rules.
- Modify π-based hashing logic in `IPHasher.cs` for encryption.

### Changing Music or UI
- Replace audio clips in `Resources/Songs/`.
- Modify UI prefabs in `Assets/Prefabs/UI/`.

---

## Contributing
1. Fork the project.
2. Create a new feature branch:
   ```bash
   git checkout -b feature/new-feature
   ```
3. Commit changes:
   ```bash
   git commit -m "Add new feature"
   ```
4. Push to branch:
   ```bash
   git push origin feature/new-feature
   ```
5. Open a pull request.

---

## Security
- All user data is securely hashed using a π-based algorithm.
- JSON data is validated and sanitized before use.

---

## Acknowledgments
- Suno AI for providing adaptive music technology.
- Mathematics and π-inspired design enthusiasts for inspiring the core mechanics.

