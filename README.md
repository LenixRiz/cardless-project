# Top-Down Mechanoid Combat & Enemy AI System (Unity)
Repositori ini berisi implementasi sistem pertempuran top-down 2D di Unity yang berfokus pada arsitektur modular, AI musuh berbasis Finite State Machine (FSM), ScriptableObject-driven data, mekanisme pertahanan asinkron (modern C# Awaitable), serta object pooling proyektil.

## Fitur Utama
1. ScriptableObject-Driven Enemies: Konfigurasi musuh terisolasi rapi pada data asset (EnemyData). Dilengkapi validasi otomatis di Unity Editor (OnValidate) untuk mendeteksi ID duplikat via AssetDatabase2..

2. State Machine AI Musuh (EnemyAI): FSM terstruktur (Idle, Chase, Attack) yang adaptif terhadap tipe mobilitas musuh (Mobile vs Static) dan tipe serangan (Close, Ranged, Kamikaze).

3. Sistem Damage Modular via Interface: Komunikasi damage menggunakan kontrak IDamageable, memastikan komponen penyerang dan penerima serangan terisolasi (decoupled).

4. Mekanisme Invisibility & Invulnerability Asinkron: Menggunakan fitur modern C# Unity Awaitable.WaitForSecondsAsync dengan destroyCancellationToken pada PlayerHealth untuk menangani i-frame / cooldown tanpa coroutine tradisional.

5. Sistem Animasi 2D Responsif: Menggunakan BlendTree arah gerak 4 arah (Horizontal, Vertical, LastHorizontal), sprite flipping, dan transisi state isHurt/isDead.

6. Proyektil & Object Pooling: Kerangka pooling peluru efisien menggunakan Unity UnityEngine.Pool.ObjectPool<T> untuk mencegah alokasi memori berlebih saat pertarungan intensif.
