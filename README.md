# Project Glass: NPC AI Architecture

[![Unity](https://img.shields.io/badge/Unity-6-black?style=flat&logo=unity)](https://unity.com/)
[![Language](https://img.shields.io/badge/C%23-10.0-blue)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Pattern](https://img.shields.io/badge/Pattern-Finite%20State%20Machine-orange)]()
[![Status](https://img.shields.io/badge/Status-Playable%20Alpha-green)]()

<div align="center">
  </div>

<br>

<details>
<summary><strong>🇯🇵 日本語の説明（クリックして展開）</strong></summary>

## NPC AI アーキテクチャ

**概要**
未公開アクションゲーム『Project Glass』における、NPCのAIアーキテクチャとゲームプレイロジックのコードサンプルです。
複雑なNPCのライフサイクル（スポーン → 注文 → 移動 → リアクション → 退店）を管理するため、**Finite State Machine (FSM)** パターンを採用しています。これにより、巨大な `Update()` ループによるスパゲッティコードを回避し、高い保守性を実現しました。

> **注:** 本コードはポートフォリオ用に抽出されたものであり、単体で動作するライブラリではありません。

**アーキテクチャ設計**
`Dictionary<Enum, Delegate>` を使用したステートディスパッチャーを採用し、`if-else` や `switch` 文の乱用を防ぎ、O(1) の計算量でステート遷移を管理しています。

**主なスクリプト構成**
* **NPCBehaviourManager.cs:** NPCの「脳」。Delegateを使用してステート遷移を管理。
* **NpcMoveState.cs:** NavMeshを使用した移動ロジック。
* **NpcOrderState.cs:** タバン（酒場）環境内での注文処理ロジック。
* **NPCEmotionManager.cs:** 待ち時間に基づいた動的な感情リアクションシステム。

**今後の最適化計画 (Refactoring Plan)**
*現在はアルファ段階のコードであるため、以下の最適化を予定しています：*
1. **参照のキャッシュ:** プロトタイピング速度優先で `FindObjectOfType` を使用している箇所を、DIコンテナまたはシングルトンマネージャー経由に変更。
2. **String Hash:** アニメーションパラメータ（例: `"isSit"`）を `Animator.StringToHash` に置き換え、パフォーマンスを向上。
3. **イベント駆動:** UI更新 (`NpcUIManager`) とロジッククラスを C# Event (`Action` / `UnityEvent`) で疎結合化。

</details>
---

## 🇬🇧 Overview
This repository serves as a **Code Showcase** demonstrating the AI architecture and gameplay logic for NPCs in "Project Glass" (an unreleased Game).

The goal of this module is to manage complex NPC lifecycles (Spawn → Order → Move → React → Leave) using a modular **Finite State Machine (FSM)** pattern, avoiding the "Spaghetti Code" often found in large `Update()` loops.

> **Note:** This code is extracted from a larger project for portfolio demonstration purposes. It is **not** intended to be a plug-and-play library or a standalone executable.

---

## 🧠 Architecture Design


The system uses a `Dictionary<Enum, Delegate>` approach to dispatch states, ensuring high modularity and clean separation of concerns.

### Key Scripts Breakdown

| Script Name | Responsibility |
| :--- | :--- |
| **[NPCBehaviourManager.cs](./NPC_Architecture/NPCBehaviourManager.cs)** | The "Brain" of the NPC. Manages state transitions using Delegates. |
| **[NpcMoveState.cs](./NPC_Architecture/NpcState/NpcMoveState.cs)** | Handles NavMesh navigation and movement parameters. |
| **[NpcOrderState.cs](./NPC_Architecture/NpcState/NpcOrderState.cs)** | Manages the logic for ordering interactions within the tavern environment. |
| **[NPCEmotionManager.cs](./NPC_Architecture/NPCEmotionManager.cs)** | Dynamic reaction system based on waiting time thresholds. |

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
