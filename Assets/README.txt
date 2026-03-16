PROTOTYPE – PROJECT OVERVIEW

Unity Version:
Unity 2021.3.21f1 LTS

Project Summary:
This project is a prototype implementation of a card-matching memory game developed as part of the test assignment. The focus of the implementation is on clean architecture, modular systems, and gameplay functionality rather than visual polish.

Core Gameplay:
The player selects cards from a grid. When two cards are selected, the system checks whether they match. Matching cards are removed from play and award points, while mismatches apply a penalty. The objective is to match all pairs with the highest possible score.
The Gamne Supports multiple grid layouts (2x2, 3x2, 4x3, 5x4 and 6x5.)

---

## SYSTEM ARCHITECTURE

The project follows a modular structure separating data, gameplay systems, UI, and utilities.

1. GameManager
   Controls the main gameplay flow including Game session initialization, Grid setup, Card selection handling, Match checking logic, Game completion detection

2. EventManager
   A static event-based system used to decouple gameplay systems. It allows different systems (UI, Audio, Scoring) to respond to gameplay events without direct dependencies. Key events include: Game Start, Game End, Card Selected, Card Match Result, Game Data Update

3. GridLayoutSpawnerUtil
   Handles dynamic generation of the card grid. Based on the selected difficulty level (Selected layout), the system Calculates card size based on available UI space, Instantiates card prefabs in the Grid

4. GridCardView
   Represents the visual and interactive component of each card in the grid. It Manages the Front/Back card display, hgandles clicks, Manages card Animations and Card states

5. ScoringMechanism
   Manages the scoring system including Points for successful matches, Penalty for mismatches and bonus points for continuous matches

6. Timer
   Tracks gameplay duration and exposes a formatted time string for UI updates. The timer emits a per-second event used to update the gameplay timer display.

7. AudioManager
   Handles all gameplay sound effects using event-driven triggers. Plays background Audio along with sound effects for crad flip/click,Cardx match, card mismatch and GameOver.

8. ProgressManager
   Responsible for saving and loading player progress. Game session data is serialized using JSON and stored using PlayerPrefs. Stored data includes Total completed sessions, Highest score (across all difficulty levele/layouts) and all the Game sessions with their Level, Time, Attemps and Score. (Highest Score can be based on each level because highest across levels/layouts does not make much sense with the fact that highest scorer will be finally, anyways with the biggest layouts)

9. UIManager
   Handles screen transitions and UI interactions including Home screen, Gameplay screen, Pause screen, Result screen, Game progress display and Popup panel

---

## DATA STRUCTURES

Card
Stores card identity and display properties.

GameData
Represents a single game session including level, score, time and attempts to complete game session

ProgressData
Stores persistent progress across multiple sessions.

---

## SUPPORTED FEATURES

✔ Dynamic grid layouts based on difficulty
✔ Card flip animations
✔ Continuous card selection during comparison
✔ Scoring system with combo bonus
✔ Timer system
✔ Save / Load game progress
✔ Audio feedback for gameplay events
✔ Progress history display

---

## NOTES

The focus of this prototype was on gameplay systems, code structure, and modular design rather than visual assets. The implementation prioritizes requirement completion, code clarity, and system separation.

---
