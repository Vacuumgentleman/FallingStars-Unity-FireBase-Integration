## **FallingStars**
**FallingStars** is a simple arcade-style Unity game with a toon aesthetic.  
The player controls a cube that moves left and right using the keyboard to avoid falling white flaming stars.  
Bonus items increase the player's score. The game integrates Firebase for saving scores and custom analytics.

---

## Gameplay

- Move the cube left and right using the keyboard.
- Avoid falling stars; collision ends the game immediately.
- Collect bonus items to increase your score.
- Gameplay is infinite until the player collides with a star.
- Score and bonuses are displayed in real time during gameplay.

---

## Firebase Integration

- **Collection name:** `PlayerScores`
- **Fields saved:**
  - Player Name
  - Score
  - Bonus Points
  - Date & Time
- **Top ranking:** Top 3 scores displayed after game over.
- Analytics tracked:
  - Total time played
  - Bonuses collected
  - Number of attempts
  - Session date & time

---

## Scenes

1. **Menu**
   - Main menu with Start button.
   - No name input yet; name is entered at the end of the game.
2. **Game**
   - Gameplay scene where stars and bonus items fall.
   - Score is tracked in real time.
   - Collision with stars ends the game and triggers the Game Over UI for name input and data submission.

---

## UI Features

- Score displayed in real time.
- Bonus count displayed in real time.
- Game Over UI allows player to input name.
- Top 3 leaderboard displayed after submission.
- Visual feedback for collisions and bonuses.

---

## Project Structure

- **Folders:**
  - Scripts
  - Scenes
  - Prefabs
  - UI
  - Materials
  - Fonts
- **Best Practices:**
  - Clear and descriptive variable and method names.
  - Proper commenting.
  - Logical flow between scenes: Menu → Game.

---

## Visual Style

- Toon style with mostly flat colors.
- Minimal shading.
- Optional outlines and particle effects for stars.

---

## Controls

- **Move Left:** Left Arrow / A
- **Move Right:** Right Arrow / D

---
screenshots: |
  ## Screenshots

  ### Menu & Settings
  | Menu | Settings |
  |------|---------|
  | <img width="450" alt="Menu" src="https://github.com/user-attachments/assets/6230b8a7-d65d-4732-b41d-6b11b37d7e7b" /> | <img width="450" alt="Settings" src="https://github.com/user-attachments/assets/b56b1a5c-fea2-45c5-ab88-dc9f90a93fd1" /> |

  ### Gameplay
  | Play | Bonus | Enemy Star |
  |------|-------|------------|
  | <img width="450" alt="Play" src="https://github.com/user-attachments/assets/57aeca31-db38-457c-8299-a643dc22e93b" /> | <img width="450" alt="Bonus" src="https://github.com/user-attachments/assets/83a06fac-d00c-4e92-b792-7406c4189182" /> | <img width="450" alt="Enemy Star" src="https://github.com/user-attachments/assets/2e720399-84cb-4139-82b4-b1bb8bb0ceb0" /> |

  ### Game Over & Leaderboard
  | Enter Name | Name Validation | Display Name | Top Scores |
  |------------|----------------|-------------|------------|
  | <img width="450" alt="Game Over Enter Name" src="https://github.com/user-attachments/assets/d8cf84fb-5ea9-41f7-a077-420cd43514fe" /> | <img width="450" alt="Name Validation" src="https://github.com/user-attachments/assets/a6a420ae-32fa-4387-90f6-5b1fe69adde5" /> | <img width="450" alt="Display Name" src="https://github.com/user-attachments/assets/879fa05a-406f-409c-8712-f3ac3c432421" /> | <img width="450" alt="Top Scores" src="https://github.com/user-attachments/assets/3cd5fbeb-d0b9-4641-a077-a27c3ad20a97" /> |

  ### Unity Editor
  <img width="900" alt="Unity Editor" src="https://github.com/user-attachments/assets/adb5f1f6-59ab-4c7d-addd-1dd79c99b6d9" />

  ### Firebase Database
  | Database View | Test Data 1 | Test Data 2 |
  |---------------|------------|------------|
  | <img width="700" alt="Database View" src="https://github.com/user-attachments/assets/db0182a8-22d1-43e7-b09a-2694d09c1728" /> | <img width="200" alt="Test Data 1" src="https://github.com/user-attachments/assets/83f18017-4557-4209-95d6-d0b5076a4054" /> | <img width="200" alt="Test Data 2" src="https://github.com/user-attachments/assets/e59b4071-7222-4e61-b855-64fb322b5db6" /> |

