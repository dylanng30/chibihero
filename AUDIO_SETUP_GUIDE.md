# Audio System Setup Guide

## Tổng quan
Hệ thống audio đã được triển khai hoàn chỉnh với các tính năng:
- Quản lý âm thanh player (nhảy, đi bộ, tấn công, bị thương, chết)
- Quản lý âm thanh enemy (tấn công các loại enemy khác nhau)
- Quản lý âm thanh boss (King, Pirate với các hành động đặc biệt)
- Quản lý nhạc nền (menu, gameplay)
- Quản lý âm thanh UI (click, menu)
- Quản lý âm thanh môi trường (nước, cửa, etc.)

## Cách Setup

### 1. Tạo AudioSystem GameObject
1. Tạo empty GameObject và đặt tên "AudioSystem"
2. Attach script `AudioSystem` 
3. Script sẽ tự động tạo các AudioSource con:
   - Music Source (loop background music)
   - SFX Source (sound effects) 
   - UI Source (UI sounds)
   - Ambient Source (ambient sounds)

### 2. Tạo AudioDatabase (Optional)
1. Right-click trong Project → Create → Audio → Audio Database
2. Cấu hình các AudioClipData với tên và settings
3. Assign vào AudioSystem để có control tốt hơn

### 3. Tạo GameAudioManager
1. Tạo empty GameObject và đặt tên "GameAudioManager"
2. Attach script `GameAudioManager`
3. Cấu hình music cho các scene khác nhau

### 4. Setup Audio Files
Audio files đã được copy vào `Assets/Resources/Audio/` với cấu trúc:
```
Assets/Resources/Audio/
├── BACKGROUND/
│   └── Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered.mp3
├── MENU_SOUND/
│   └── menu.wav
└── SOUND_EFFECT/
    ├── BOSS/
    │   ├── KING/ (CLOSE_DOOR, HAMMER, KING_LAUGH, OPEN_DOOR)
    │   └── PIRATE/ (ARR_PIRATE, SLASH1, SLASH2)
    ├── ENEMY/
    │   ├── ARCHER/ (ARROW)
    │   ├── PAWN/ (AXE)
    │   ├── TNT_GOBLIN/ (TNT)
    │   └── TORCH_GOBLIN/ (TORCH)
    ├── OTHER/ (FALL-IN-TO-WATER, Mouse-Click-Sound-Effect)
    └── PLAYER+KNIGHT/ (attack1, attack2, attack3, JUMP, Walking)
```

## Cách sử dụng

### Player Audio (Đã tích hợp)
```csharp
// Các âm thanh player đã được tự động tích hợp vào:
// - MovementPlayer: nhảy, đi bộ
// - AbilityNormalATK: tấn công (có biến thể 1,2,3)
// - DamageManagerPlayer: bị thương, chết

// Hoặc gọi trực tiếp:
AudioManager.PlayPlayerJump(transform.position);
AudioManager.PlayPlayerAttack(1, transform.position);
```

### Enemy Audio (Đã tích hợp)
```csharp
// Đã tích hợp vào AbilityEnemyNormalATK
// Hoặc gọi trực tiếp:
AudioManager.PlayEnemyAttack("archer", transform.position);
AudioManager.PlayEnemyAttack("pawn", transform.position);
```

### Boss Audio
```csharp
// Attach BossAudioController vào boss GameObject
// Rồi gọi trong animation events hoặc code:
bossAudio.PlayBossLaugh();
bossAudio.PlayKingHammer();
bossAudio.PlayPirateSlash(1);
```

### Background Music
```csharp
// Tự động phát theo scene với GameAudioManager
// Hoặc control thủ công:
AudioManager.PlayBackgroundMusic("Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered");
AudioManager.StopBackgroundMusic();
```

### UI Audio (Đã tích hợp)
```csharp
// UIManager đã có âm thanh click
// Hoặc attach UIButtonAudio vào Button để tự động
// Hoặc gọi trực tiếp:
AudioManager.PlayUIClick();
AudioManager.PlayMenuSound();
```

### Environment Audio
```csharp
// Attach EnvironmentAudioTrigger vào trigger objects
// Hoặc gọi trực tiếp:
AudioManager.PlayWaterSplash(transform.position);
AudioManager.PlayDoorOpen(transform.position);
```

### Volume Control
```csharp
// Updated default settings:
// - Master Volume: 100%
// - Music Volume: 50% (tăng từ 30%)
// - SFX Volume: 120% (tăng để walking/jumping to hơn)
// - UI Volume: 90%
// - Walking Sound: 180% volume multiplier (tăng từ 150%)
// - Jumping Sound: 150% volume multiplier (mới thêm)

AudioManager.SetMasterVolume(1f);
AudioManager.SetMusicVolume(0.5f); // Nhạc nền vừa phải
AudioManager.SetSFXVolume(1.2f); // SFX to hơn
AudioManager.SetUIVolume(0.9f);

// CÓ THỂ CHỈNH TRONG INSPECTOR:
// AudioSystem GameObject -> Inspector -> Audio Settings - Editable in Inspector
```

## 🎮 Cách chỉnh volume trong Inspector:
1. Tìm AudioSystem GameObject trong scene
2. Trong Inspector, mở section "Audio Settings - Editable in Inspector"
3. Điều chỉnh các slider:
   - Inspector Master Volume
   - Inspector Music Volume  
   - Inspector Sfx Volume
   - Inspector Ui Volume
4. Thay đổi sẽ áp dụng realtime ngay khi chạy game!

## 🎵 Âm thanh nền vs SFX:
- **Background music**: Chạy liên tục, không bị gián đoạn
- **SFX sounds**: Phát đồng thời với nhạc nền (PlayOneShot)
- **Walking/Jumping**: Volume cao để nghe rõ hơn
- **Attack sounds**: Không làm dừng nhạc nền

## Volume Optimization (Updated)
- **Nhạc nền**: Tăng lên 50% để nghe rõ hơn
- **Walking sound**: Tăng volume multiplier lên 1.8x 
- **Jumping sound**: Thêm volume multiplier 1.5x
- **SFX base**: Tăng lên 120% cho tất cả sound effects

## Các Script Chính

1. **AudioSystem**: Core audio management
2. **AudioManager**: Static wrapper để dễ gọi
3. **AudioDatabase**: ScriptableObject để config
4. **GameAudioManager**: Scene music management
5. **BossAudioController**: Boss-specific audio
6. **UIButtonAudio**: Auto UI button sounds
7. **EnvironmentAudioTrigger**: Environment sound triggers

## Lưu ý
- Tất cả audio files đã được load từ Resources để dễ access
- AudioSystem sử dụng Singleton pattern
- Âm thanh player/enemy đã được tích hợp sẵn vào movement/combat
- Volume control hoạt động với tất cả loại âm thanh
- Hỗ trợ cả 2D và 3D positional audio
