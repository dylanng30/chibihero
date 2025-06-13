# AudioManager Nâng Cao - Hướng Dẫn Đầy Đủ

## 🎵 Tính năng mới:

### ✨ **Chọn nhiều âm thanh cho mỗi action**
- Walking: Có thể có nhiều sound khác nhau
- Jumping: Nhiều sound để đa dạng
- Attacking: 3 sound khác nhau, phát random
- UI: Button clicks, menu sounds riêng biệt

### 🎛️ **Điều chỉnh chi tiết từng âm thanh**
- **Volume**: 0-3x cho từng sound riêng biệt
- **Pitch**: Thay đổi cao độ âm thanh
- **Enable/Disable**: Bật/tắt từng sound
- **Randomization**: Random volume/pitch để tự nhiên hơn

### 🎯 **Playback Options**
- **Play First**: Phát sound đầu tiên enabled
- **Play Random**: Phát random 1 sound từ list
- **Play All**: Phát tất cả sounds cùng lúc

## 🚀 Setup trong Unity Inspector:

### Bước 1: Tạo AudioManager
1. **Tạo GameObject** tên "AudioManager"
2. **Add Component** → `AudioManager`
3. **Right-click component** → "Create Audio Sources"
4. **Right-click component** → "Initialize Default Sounds"

### Bước 2: Setup Background Music
```
Background Music
├── Clip: [Kéo file nhạc nền vào]
├── Volume: 0.8 (0-3)
├── Pitch: 1.0 (0-2)
├── Enabled: ✅
└── Randomization: (Tùy chọn)
```

### Bước 3: Setup Player Sounds

#### Walking Sounds:
```
Walking Sounds
├── Enabled: ✅
├── Sounds (Array)
│   ├── [0] Walking Sound 1
│   │   ├── Clip: Walking.MP3
│   │   ├── Volume: 1.0
│   │   ├── Pitch: 1.0
│   │   └── Enabled: ✅
│   └── [1] Walking Sound 2 (Tùy chọn)
├── Play Random Sound: ✅ (Nếu muốn random)
└── Global Volume Multiplier: 1.2
```

#### Jump Sounds:
```
Jump Sounds
├── Enabled: ✅
├── Sounds (Array)
│   ├── [0] Jump Sound 1
│   │   ├── Clip: JUMP.MP3
│   │   ├── Volume: 1.5
│   │   ├── Pitch: 1.0
│   │   └── Randomize Volume: ✅ (0.1 variation)
└── Global Volume Multiplier: 1.0
```

#### Attack Sounds:
```
Attack Sounds
├── Enabled: ✅
├── Sounds (Array)
│   ├── [0] Attack Sound 1
│   │   ├── Clip: attack1.wav
│   │   ├── Volume: 1.2
│   ├── [1] Attack Sound 2
│   │   ├── Clip: attack2.wav
│   │   ├── Volume: 1.1
│   └── [2] Attack Sound 3
│       ├── Clip: attack3.wav
│       ├── Volume: 1.3
├── Play Random Sound: ✅
└── Global Volume Multiplier: 1.5
```

### Bước 4: Master Controls
```
Master Controls
├── Master Volume: 1.0 (Âm lượng tổng)
├── Music Volume: 0.2 (Nhạc nền)
├── Sound Effect Volume: 1.5 (Sound effects)

Global Settings
├── Music Enabled: ✅
├── Sound Effects Enabled: ✅
└── Auto Play Background Music: ✅
```

## 🎯 Sử dụng nâng cao:

### Tạo sound variations:
1. **Tăng Array size** cho loại sound muốn thêm
2. **Kéo nhiều AudioClips** vào slots
3. **Bật "Play Random Sound"** cho đa dạng
4. **Chỉnh volume/pitch** khác nhau cho mỗi sound

### Randomization cho tự nhiên:
```
Randomize Volume: ✅
Volume Variation: 0.2 (±20%)

Randomize Pitch: ✅  
Pitch Variation: 0.1 (±10%)
```

### Multiple attack sounds:
```
Attack Sounds Array:
[0] Sword Slash - Volume: 1.0
[1] Heavy Hit - Volume: 1.5  
[2] Critical Strike - Volume: 1.8
Play Random Sound: ✅
```

## 🎮 Code Usage (Không thay đổi):
```csharp
// Vẫn dùng như cũ
AudioManager.Instance.PlayWalkingSound();
AudioManager.Instance.PlayJumpSound();
AudioManager.Instance.PlayAttackSound();

// Sounds mới
AudioManager.Instance.PlayButtonClickSound();
AudioManager.Instance.PlayPickupSound();
```

## 🔧 Context Menu Actions:
- **"Create Audio Sources"**: Tạo AudioSources
- **"Initialize Default Sounds"**: Tạo arrays mặc định
- **"Test All Sounds"**: Test tất cả sounds

## 💡 Tips & Tricks:

### Để tạo sound variations:
1. **Duplicate sound files** với tên khác nhau
2. **Import với settings khác nhau** (compression, quality)
3. **Chỉnh volume/pitch** trong AudioManager thay vì edit file

### Để optimize performance:
1. **Disable** sounds không dùng
2. **Sử dụng compression** cho audio files
3. **Limit array size** - không cần quá nhiều variations

### Để tạo dynamic audio:
1. **Enable randomization** cho volume/pitch
2. **Mix multiple sounds** với Play All Sounds
3. **Layer sounds** bằng cách có nhiều AudioSources

## ✅ Lợi ích AudioManager mới:

- ✅ **Hoàn toàn visual** - Setup trong Unity Inspector
- ✅ **Flexible** - Nhiều sounds, nhiều options
- ✅ **Professional** - Randomization, layering
- ✅ **Performance** - Enable/disable từng sound
- ✅ **Easy management** - Tất cả trong 1 component
- ✅ **No code changes** - API giữ nguyên

**Bây giờ bạn có full control về audio system mà không cần code gì!** 🎯🎵
