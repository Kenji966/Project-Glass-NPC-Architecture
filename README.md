# Project Glass: NPC AI Architecture

![Unity](https://img.shields.io/badge/Unity-2022.3%2B-black?style=flat&logo=unity)
![C#](https://img.shields.io/badge/Language-C%23-blue)
![Pattern](https://img.shields.io/badge/Pattern-Finite%20State%20Machine-orange)
![Status](https://img.shields.io/badge/Status-Playable%20Alpha-green)

## 📂 Overview
This repository serves as a **Code Showcase** demonstrating the AI architecture and gameplay logic for NPCs in "Project Glass" (an unreleased Game).

The goal of this module is to manage complex NPC lifecycles (Spawn → Order → Move → React → Leave) using a modular **Finite State Machine (FSM)** pattern, avoiding the "Spaghetti Code" often found in large `Update()` loops.

> **Note:** This code is extracted from a larger project for portfolio demonstration purposes. It is **not** intended to be a plug-and-play library or a standalone executable.

---

## 🧠 Architecture Design

The system uses a `Dictionary<Enum, Delegate>` approach to dispatch states, ensuring high modularity and clean separation of concerns.

### Key Scripts Breakdown

| Script Name | Responsibility |
| :--- | :--- |
| **[NPCBehaviourManager.cs](./Assets/Scripts/NPCBehaviourManager.cs)** | The "Brain" of the NPC. Manages state transitions using Delegates. |
| **[NpcMoveState.cs](./Assets/Scripts/NpcMoveState.cs)** | Handles NavMesh navigation and movement parameters. |
| **[NpcOrderState.cs](./Assets/Scripts/NpcOrderState.cs)** | Manages the logic for ordering interactions within the tavern environment. |
| **[NPCEmotionManager.cs](./Assets/Scripts/NPCEmotionManager.cs)** | Dynamic reaction system based on waiting time thresholds. |

---

## 📝 Technical Highlights

* **Finite State Machine (FSM):**
    * States are decoupled into separate classes (MonoBehaviours).
    * `NPCBehaviourManager` acts as the central hub, switching states cleanly without complex `if-else` chains.
* **Delegate-Based State Dispatcher:**
    * Uses `Dictionary<NPCBehaviourState, StateHandler>` for O(1) complexity state lookups.
* **Modular Component Design:**
    * Animation, Movement (NavMesh), and Logic are separated to adhere to the Single Responsibility Principle.

---

## ⚠️ Optimization & Future Refactoring
*As this code represents the Alpha Development phase, the following optimizations are planned for the Beta/Refactoring phase:*

1.  **Caching References:** Currently, some states use `FindObjectOfType` within logic loops for prototyping speed. In production, these will be moved to a Dependency Injection system or Singleton Manager.
2.  **String Hashing:** Hardcoded animation strings (e.g., `"isSit"`) will be replaced with `Animator.StringToHash` for better performance and safety.
3.  **Event System:** Decoupling the UI updates (`NpcUIManager`) from the Logic classes using C# Events (`Action` / `UnityEvent`).

---

## 👤 Author
**Kenji**
* Game Developer / Programmer
* Focus: Gameplay Logic & System Architecture
