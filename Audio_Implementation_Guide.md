# Audio System Implementation Guide

## Các tính năng đã được triển khai:

### 1. AudioSystem (Singleton)
- **Background Music**: Tự động phát nhạc nền khi game bắt đầu
- **Sound Effects**: Quản lý các hiệu ứng âm thanh
- **Footstep Sounds**: Riêng biệt cho tiếng bước chân khi di chuyển
- **Volume Controls**: Điều chỉnh âm lượng cho từng loại âm thanh

### 2. Player Movement Sounds
- **Walking Sound**: Phát khi player di chuyển và đang ở trên mặt đất
- **Jump Sound**: Phát khi player nhảy
- **Dash Sound**: Phát khi player dash (nếu có dash sound)

### 3. Attack Sounds
- **Random Attack Sounds**: Phát ngẫu nhiên 1 trong 3 âm thanh tấn công

## Cách sử dụng:

### Setup ban đầu:
1. **Chạy menu `Tools > Setup Audio System`** - Tự động tìm audio từ folder Assets/Sounds
2. **Hoặc tạo AudioSystem GameObject thủ công:**
   - Thêm `AudioSystemSetup` component vào bất kỳ GameObject nào
   - Hoặc tạo GameObject mới và thêm `AudioSystem` component

### Setup AudioSource & Audio Clips:

#### Cách 1: Tự động (Khuyến nghị) - Sử dụng Menu
1. Chạy **`Tools > Setup Audio System`** 
2. Sẽ tự động:
   - Tạo AudioSystem GameObject
   - Tạo các AudioSource
   - Tìm và gán tất cả audio clips từ `Assets/Sounds`

#### Cách 2: Tự động - Sử dụng Inspector
1. Chọn GameObject có `AudioSystem` component
2. Trong Inspector, nhấn button **"Complete Setup"**
3. Hoặc nhấn **"Auto Find Audio"** để chỉ tìm audio clips

#### Cách 3: Thủ công 
1. Tạo 3 GameObject con: MusicSource, SoundsSource, FootstepSource
2. Thêm AudioSource component vào mỗi GameObject
3. Kéo thả các AudioSource vào slots trong AudioSystem Inspector
4. Kéo thả các audio files từ `Assets/Sounds` vào audio clips slots

### Audio Files được tự động tìm từ:
- **Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/**
- **Assets/Sounds/BACKGROUND/**

### 🎛️ **Audio Controls trong PauseMenu:**
```csharp
// Bật/tắt nhạc nền
AudioSystem.Instance.ToggleMusic();

// Bật/tắt sound effects
AudioSystem.Instance.ToggleSoundEffects();

// Điều chỉnh âm lượng tổng
AudioSystem.Instance.SetMasterVolume(0.8f);

// Lưu/load settings
AudioSystem.Instance.SaveAudioSettings();
AudioSystem.Instance.LoadAudioSettings();
```

### 🎚️ **Âm lượng đã được điều chỉnh:**
- **Background Music**: 0.15 (giảm từ 0.3) - Nhẹ nhàng hơn
- **Sound Effects**: 1.8 (tăng từ 1.2) - To và rõ ràng hơn nhiều  
- **Footstep**: 1.5 (tăng từ 1.0) - Nghe rõ hơn
- **Individual sounds**: Jump/Attack/Dash có thêm multiplier 1.5x

### 🐛 **Debug & Troubleshooting:**

#### Nếu sound không hoạt động:
1. **Kiểm tra setup**: Right-click AudioSystem → "Debug Status"
2. **Test sounds**: Right-click AudioSystem → "Test All Sounds"  
3. **Kiểm tra Console**: Xem debug messages
4. **Kiểm tra Sound Effects**: Có thể bị tắt trong PauseMenu

#### Debug methods:
```csharp
// Test tất cả sounds
AudioSystem.Instance.TestAllSounds();

// Kiểm tra trạng thái
AudioSystem.Instance.DebugAudioStatus();

// Force enable sound effects
AudioSystem.Instance.SoundEffectsEnabled = true;
```

### Sử dụng trong code:
```csharp
// Phát âm thanh đi bộ
AudioSystem.Instance.PlayWalkingSound();

// Dừng âm thanh đi bộ  
AudioSystem.Instance.StopWalkingSound();

// Phát âm thanh nhảy
AudioSystem.Instance.PlayJumpSound();

// Phát âm thanh tấn công (1, 2, hoặc 3)
AudioSystem.Instance.PlayAttackSound(1);

// Phát âm thanh bất kỳ
AudioSystem.Instance.PlaySound(audioClip, volume);

// Điều chỉnh âm lượng
AudioSystem.Instance.SetMusicVolume(0.5f);
AudioSystem.Instance.SetSoundEffectVolume(0.7f);
AudioSystem.Instance.SetFootstepVolume(0.3f);
```

## Audio Files Location:
- **Player Sounds**: `Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/`
  - Walking.MP3
  - JUMP.MP3  
  - attack1.wav
  - attack2.wav
  - attack3.wav
  
- **Background Music**: `Assets/Sounds/BACKGROUND/`
  - Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered.mp3

**✅ Âm thanh được load trực tiếp từ Assets/Sounds - KHÔNG cần copy vào Resources!**

## Đã implement ở các file:
- ✅ `MovementPlayer.cs` - Âm thanh di chuyển và nhảy
- ✅ `AbilitySkill.cs` - Âm thanh tấn công (cố định attack sound 1)
- ✅ `AudioSystem.cs` - Hệ thống audio chính
- ✅ `MoveRandomly.cs` - Fix lỗi Animator bug

## Tính năng:
- ✅ Nhạc nền tự động phát (âm lượng giảm xuống 0.15)
- ✅ Sound effects cho player movement (âm lượng tăng lên 1.8)
- ✅ Sound effects cho jumping (âm lượng tăng lên 1.8)
- ✅ Sound effects cho attacking (âm lượng tăng lên 1.8) - **Cố định attack sound 1**
- ✅ Quản lý âm lượng riêng biệt
- ✅ Footstep sound loop khi di chuyển (âm lượng tăng lên 1.5)
- ✅ **Attack sound cố định** - Bỏ random để tránh bug
- ✅ **Bật/tắt âm thanh trong PauseMenu**
- ✅ **Điều chỉnh Master Volume**
- ✅ **Lưu/Load audio settings**
- ✅ **Fix Animator bugs** - Null checks cho AnimatorController

## PauseMenu Audio Controls:
- **Music Toggle Button**: Bật/tắt nhạc nền
- **Sound Toggle Button**: Bật/tắt sound effects
- **Master Volume Slider**: Điều chỉnh âm lượng tổng
- **Auto Save**: Tự động lưu settings khi thay đổi

## Notes:
- AudioSystem sử dụng pattern Singleton nên có thể truy cập từ bất kỳ đâu
- Footstep sound sẽ tự động loop khi player di chuyển và dừng khi không di chuyển
- Attack sounds được chọn ngẫu nhiên từ 3 file có sẵn
- Background music sẽ tự động phát khi scene bắt đầu
