# FallingStars

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
