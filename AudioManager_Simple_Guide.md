# AudioManager - Hướng Dẫn Đơn Giản

## 🎵 Setup nhanh trong 3 bước:

### Bước 1: Tạo AudioManager
1. **Tạo Empty GameObject** tên "AudioManager"  
2. **Add Component** → `AudioManager`
3. **Right-click component** → "Create Audio Sources"

### Bước 2: Kéo thả Audio Clips
Kéo các file từ `Assets/Sounds/` vào các slot:

- **Walking Sound**: `SOUND_EFFECT/PLAYER+KNIGHT/Walking.MP3`
- **Jump Sound**: `SOUND_EFFECT/PLAYER+KNIGHT/JUMP.MP3`  
- **Attack Sound**: `SOUND_EFFECT/PLAYER+KNIGHT/attack1.wav`
- **Background Music**: `BACKGROUND/Phoenix-Wright-Ace-Attorney-OST...mp3`

### Bước 3: Chạy game
✅ Âm thanh sẽ tự động hoạt động!

## 📁 Cấu trúc tự động tạo:
```
AudioManager
├── MusicSource (AudioSource)
├── SoundEffectsSource (AudioSource)  
└── FootstepSource (AudioSource)
```

## 🎛️ Settings trong Inspector:

### Audio Sources (Tự động tạo):
- **Music Source**: Nhạc nền
- **Sound Effects Source**: Jump, Attack
- **Footstep Source**: Walking

### Player Sounds (Kéo vào):
- **Walking Sound**: Âm thanh đi bộ
- **Jump Sound**: Âm thanh nhảy
- **Attack Sound**: Âm thanh tấn công

### Volume Settings (Điều chỉnh được):
- **Music Volume**: 0.2 (20%)
- **Sound Effect Volume**: 1.5 (150%)
- **Footstep Volume**: 1.0 (100%)

### Control Settings:
- **Music Enabled**: ✅ Bật nhạc nền
- **Sound Effects Enabled**: ✅ Bật sound effects
- **Auto Play Background Music**: ✅ Tự phát nhạc nền

## 🔧 Context Menu Actions:
- **Right-click AudioManager** → "Setup Audio Manager"
- **Right-click AudioManager** → "Create Audio Sources"  
- **Right-click AudioManager** → "Test All Sounds"

## 🎮 Sử dụng trong Code (Đã tự động):
```csharp
// Đã được tự động kết nối trong:
AudioManager.Instance.PlayWalkingSound();  // MovementPlayer.cs
AudioManager.Instance.PlayJumpSound();     // MovementPlayer.cs  
AudioManager.Instance.PlayAttackSound();   // AbilitySkill.cs
```

## 🎛️ PauseMenu Controls (Đã hoạt động):
- **Music Toggle**: Bật/tắt nhạc nền
- **Sound Toggle**: Bật/tắt sound effects
- **Master Volume**: Điều chỉnh âm lượng tổng

## ✅ Lợi ích AudioManager mới:
- ✅ **Cực kỳ đơn giản** - Chỉ 1 file, không Editor
- ✅ **Kéo thả** - Visual setup trong Inspector
- ✅ **Context Menu** - Right-click để setup nhanh
- ✅ **Singleton** - Truy cập từ bất kỳ đâu
- ✅ **Auto Save** - Lưu settings tự động
- ✅ **Volume Sliders** - Điều chỉnh realtime

**Chỉ cần kéo AudioManager vào scene và kéo audio files vào là xong!** 🎯
