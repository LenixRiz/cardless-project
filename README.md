# Cardless Project

**Cardless Project** adalah proyek game **Top-Down 2D** yang dibuat menggunakan **Unity dan C#**, dengan fokus utama pada pengembangan sistem **combat, enemy AI, modular architecture, dan reusable gameplay systems**.

Project ini dibuat sebagai latihan untuk menerapkan konsep pemrograman yang lebih lanjut dalam Unity, termasuk **Finite State Machine (FSM), ScriptableObject, Interface, asynchronous programming, object pooling, serta pemisahan tanggung jawab antar komponen**.

> **Fokus utama proyek:** Membangun sistem combat dan enemy AI yang modular, terstruktur, dan mudah dikembangkan.

---

##  Fitur Utama

*  Enemy AI berbasis **Finite State Machine (FSM)**
*  State AI seperti **Idle, Chase, dan Attack**
*  Sistem combat modular
*  Sistem damage menggunakan Interface
*  Enemy configuration menggunakan **ScriptableObject**
*  Support untuk berbagai tipe enemy dan attack
*  Sistem projectile
*  Object Pooling untuk projectile
*  Sistem health dan damage pemain
*  Sistem invulnerability / i-frame
*  Sistem invisibility
*  Asynchronous gameplay menggunakan **Unity Awaitable**
*  Sistem animasi 2D dengan **Blend Tree**
*  Sistem animasi 4 arah
*  UI Management
*  Sound Management
*  Struktur script berdasarkan tanggung jawab sistem

---

##  Konsep yang Dipelajari

Project ini dibuat untuk memperdalam pemahaman mengenai bagaimana sistem gameplay dapat dirancang agar **modular, loosely coupled, dan mudah dikembangkan**.

Beberapa konsep utama yang diterapkan:

* Finite State Machine
* Object-Oriented Programming
* Interface
* ScriptableObject
* Asynchronous Programming
* Object Pooling
* Separation of Concerns
* Modular Architecture
* Unity Animation System
* Dependency Management antar komponen

---

##  Enemy AI

Salah satu bagian utama dari project ini adalah sistem **Enemy AI berbasis Finite State Machine (FSM)**.

Enemy dapat memiliki beberapa state berdasarkan kondisi permainan:

```text
             ┌──────────┐
             │   Idle   │
             └────┬─────┘
                  │
            Player Detected
                  │
                  ▼
             ┌──────────┐
             │  Chase   │
             └────┬─────┘
                  │
           Attack Range
                  │
                  ▼
             ┌──────────┐
             │  Attack  │
             └────┬─────┘
                  │
            Player Escapes
                  │
                  ▼
             ┌──────────┐
             │   Idle   │
             └──────────┘
```

FSM memungkinkan setiap perilaku enemy dipisahkan berdasarkan state sehingga logic AI lebih mudah dikembangkan dan dipelihara.

Project ini juga mendukung beberapa variasi enemy berdasarkan:

*  Tipe mobilitas
*  Tipe serangan
*  Close-range attack
*  Ranged attack
*  Kamikaze behavior
*  Static enemy

Struktur AI dipisahkan ke dalam `EnemyAI`, `EnemyCombat`, dan `EnemyController` agar masing-masing komponen memiliki tanggung jawab yang lebih spesifik.

---

##  ScriptableObject-Driven Enemy

Data enemy dipisahkan menggunakan **ScriptableObject** melalui `EnemyData`.

Dengan pendekatan ini, data seperti konfigurasi enemy dapat disimpan sebagai asset secara terpisah dari logic behavior.

```text
EnemyData
   │
   ├── Enemy Stats
   ├── Movement Type
   ├── Attack Type
   └── Other Configuration
            │
            ▼
       EnemyController
            │
            ├── EnemyAI
            └── EnemyCombat
```

Pendekatan ini membuat data enemy lebih mudah dikonfigurasi tanpa harus mengubah kode behavior secara langsung.

Project juga menggunakan validasi di Unity Editor untuk membantu mendeteksi masalah seperti ID enemy yang duplikat.

---

##  Modular Damage System

Sistem damage menggunakan interface:

```csharp
IDamageable
```

Interface ini digunakan sebagai kontrak bagi object yang dapat menerima damage.

Konsep sederhananya:

```text
┌───────────────┐
│   Attacker    │
└───────┬───────┘
        │
        │ Deal Damage
        ▼
┌───────────────────┐
│    IDamageable    │
└─────────┬─────────┘
          │
     ┌────┴─────┐
     ▼          ▼
┌─────────┐ ┌─────────┐
│ Player  │ │  Enemy  │
└─────────┘ └─────────┘
```

Dengan menggunakan interface, sistem yang memberikan damage tidak perlu mengetahui secara spesifik object apa yang menerima damage.

Hal ini membantu menciptakan komunikasi yang lebih **decoupled dan reusable**.

Project menyediakan `IDamageable.cs` sebagai kontrak untuk sistem damage tersebut.

---

##  Player Health & Defensive System

Sistem pemain mencakup mekanisme seperti:

*  Health
*  Damage
*  Invulnerability
*  Invisibility
*  Temporary defensive state
*  Hurt animation
*  Death state

Salah satu pendekatan yang digunakan dalam project ini adalah **asynchronous operation menggunakan Unity Awaitable**.

Alih-alih menggunakan Coroutine untuk beberapa temporary state, project memanfaatkan:

```csharp
Awaitable.WaitForSecondsAsync()
```

serta:

```csharp
destroyCancellationToken
```

untuk menangani timer dan cancellation secara lebih modern.

Pendekatan ini digunakan pada sistem health pemain, khususnya untuk menangani mekanisme seperti **i-frame dan cooldown**.

---

##  Projectile System

Project juga memiliki sistem projectile yang dipisahkan ke dalam:

```text
Features/
└── Projectile/

Enemy/
└── Projectile.cs
```

Pemisahan ini memungkinkan projectile digunakan sebagai sistem gameplay tersendiri tanpa harus terikat langsung dengan logic enemy tertentu.

---

##  Object Pooling

Projectile menggunakan konsep **Object Pooling** melalui:

```csharp
UnityEngine.Pool.ObjectPool
```

Daripada terus-menerus melakukan:

```text
Instantiate
      ↓
Projectile
      ↓
Destroy
```

object dapat digunakan kembali:

```text
        ┌─────────────┐
        │ Object Pool │
        └──────┬──────┘
               │
        ┌──────▼──────┐
        │  Projectile │
        └──────┬──────┘
               │
          Used in Game
               │
               ▼
        Return to Pool
               │
               └───────────┐
                           │
                    Reuse Object
```

Pendekatan ini membantu mengurangi pembuatan dan penghancuran object secara berulang, terutama ketika terdapat banyak projectile dalam waktu yang bersamaan.

---

##  Sistem Animasi 2D

Project menggunakan sistem animasi 2D dengan **Blend Tree** untuk menangani pergerakan karakter.

Parameter yang digunakan mencakup:

* `Horizontal`
* `Vertical`
* `LastHorizontal`

Sistem ini memungkinkan karakter memiliki animasi pergerakan berdasarkan arah.

Selain movement, terdapat state untuk:

*  Hurt
*  Dead
*  Movement

Sprite juga dapat melakukan flipping untuk menyesuaikan arah karakter.

Pendekatan ini membuat sistem animasi lebih responsif terhadap perubahan arah dan state karakter.

---

##  Struktur Project

```text
Assets/
└── Cardless/
    └── Scripts/
        │
        ├── Enemy/
        │   ├── EnemyData/
        │   ├── EnemyAI.cs
        │   ├── EnemyCombat.cs
        │   ├── EnemyController.cs
        │   └── Projectile.cs
        │
        ├── Features/
        │   └── Projectile/
        │
        ├── Managers/
        │   ├── PlayerManager.cs
        │   ├── SoundManager.cs
        │   └── UIManager.cs
        │
        ├── Player/
        │   ├── PlayerAnimations/
        │   └── PlayerSprite/
        │
        ├── IDamageable.cs
        └── IExperience.cs
```

Struktur tersebut memisahkan script berdasarkan fungsi dan tanggung jawabnya, sehingga sistem seperti player, enemy, manager, dan feature dapat dikembangkan secara terpisah.

---

##  Manager System

Project memiliki beberapa manager untuk menangani sistem yang bersifat global atau digunakan oleh beberapa komponen:

| Manager         | Tanggung Jawab                                |
| --------------- | --------------------------------------------- |
| `PlayerManager` | Mengelola sistem yang berkaitan dengan pemain |
| `UIManager`     | Mengelola UI                                  |
| `SoundManager`  | Mengelola audio                               |

Manager ditempatkan di folder terpisah agar tanggung jawabnya tidak bercampur dengan gameplay component.

---

##  Teknologi yang Digunakan

* **Unity**
* **C#**
* Unity 2D
* Unity Physics
* Unity Animator
* Animation Blend Tree
* ScriptableObject
* C# Interface
* Unity Awaitable
* C# Async Programming
* Unity Object Pool
* Git
* GitHub

---

##  Tujuan Project

Project ini dibuat sebagai latihan untuk meningkatkan kemampuan dalam **C# Programming dan Unity Game Development**, khususnya dalam membangun sistem gameplay yang lebih modular dan scalable.

Berbeda dengan project sederhana yang hanya berfokus pada implementasi fitur, project ini lebih menekankan pada **bagaimana sistem tersebut dirancang dan berkomunikasi satu sama lain**.

Beberapa aspek yang menjadi fokus pembelajaran adalah:

> **"Bagaimana membuat sistem yang tidak hanya bekerja, tetapi juga mudah dikembangkan dan dipelihara."**

---

##  Hal yang Dipelajari

Melalui project ini, saya memperdalam pemahaman mengenai:

*  Finite State Machine
*  ScriptableObject Architecture
*  Interface-based Programming
*  Modular Combat System
*  Enemy AI
*  Object Pooling
*  Asynchronous Programming
*  Unity Animation System
*  Separation of Concerns
*  Modular Architecture
*  Loose Coupling antar sistem
