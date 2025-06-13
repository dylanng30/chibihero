# 🎚️ PITCH & LOOP CONTROL - AudioManager

## ✅ Đã thêm tính năng mới:

### 🎵 Pitch Control (Điều chỉnh cao độ âm thanh):
- **Music Pitch**: 0.5x → 2x (chậm → nhanh)
- **Walk Pitch**: 0.5x → 2x (bước chậm → bước nhanh)
- **Jump Pitch**: 0.5x → 2x (nhảy trầm → nhảy cao)
- **Attack Pitch**: 0.5x → 2x + Random Variation
- **Menu Pitch**: 0.5x → 2x (UI sounds)

### 🔄 Loop Control:
- **Music Loop**: Bật/tắt loop nhạc nền
- **Walk Loop**: Bật/tắt loop âm thanh bước chân

## 🎛️ Inspector Controls:

### Background Music:
```
[Header("Background Music")]
📁 Background Music: (Kéo file nhạc vào)
🎚️ Music Pitch: 1.0 (0.5 - 2.0)
🔄 Music Loop: ✅ (tick để loop)
🔊 Music Volume: 0.5 (0.0 - 1.0)
✅ Enable Music: ✅
```

### Player Sounds:
```
[Header("Player Sounds")]
📁 Walk Sound: (Kéo file walking vào)
🎚️ Walk Pitch: 1.0 (0.5 - 2.0)
🔄 Walk Loop: ✅ (tick để loop khi đi)

📁 Jump Sound: (Kéo file jump vào)
🎚️ Jump Pitch: 1.0 (0.5 - 2.0)

📁 Attack Sounds: (Kéo array attack files vào)
🎚️ Attack Pitch: 1.0 (0.5 - 2.0)
🎲 Attack Pitch Variation: 0.1 (0.0 - 0.5) // Random ±
```

### UI Sounds:
```
[Header("UI Sounds")]
📁 Menu Sound: (Kéo file menu vào)
🎚️ Menu Pitch: 1.0 (0.5 - 2.0)
```

## 🎮 Effects Examples:

### Music:
- **Pitch 0.7**: Nhạc chậm, drama
- **Pitch 1.0**: Nhạc bình thường
- **Pitch 1.3**: Nhạc nhanh, hồi hộp

### Walking:
- **Pitch 0.8**: Bước chậm, sneaking
- **Pitch 1.0**: Bước bình thường
- **Pitch 1.5**: Bước nhanh, running

### Jump:
- **Pitch 0.6**: Nhảy heavy, robot
- **Pitch 1.0**: Nhảy bình thường
- **Pitch 1.8**: Nhảy light, fairy

### Attack:
- **Pitch 1.0 + Variation 0.1**: Attack sounds từ 0.9-1.1 (natural)
- **Pitch 1.2 + Variation 0.3**: Attack sounds từ 0.9-1.5 (diverse)

## 🧪 Testing:

### Context Menus:
1. **"Test All Sounds"** - Test với pitch hiện tại
2. **"Test Pitch Variations"** - Test các pitch khác nhau tự động
3. **"Check Audio Setup"** - Kiểm tra setup

### Manual Testing:
1. Chỉnh pitch trong Inspector
2. Nhấn Play
3. Test sounds realtime
4. Console sẽ show: `🦘 Playing jump sound: JUMP (Pitch: 1.3)`

## 🎯 Creative Uses:

### Game States:
- **Slow motion**: Tất cả pitch = 0.5
- **Speed boost**: Tất cả pitch = 1.5
- **Underwater**: Music pitch = 0.8, muffled effect

### Character Types:
- **Heavy character**: Lower pitch cho footsteps
- **Light character**: Higher pitch cho footsteps
- **Robot**: Fixed pitch, no variation

### Dynamic Audio:
- **Health low**: Pitch giảm dần
- **Power up**: Pitch tăng lên
- **Different weapons**: Khác pitch cho attack

## 🔧 Debug Info:

Console messages sẽ show pitch:
- `🚶 Playing walk sound: Walking (Pitch: 1.2, Loop: True)`
- `🦘 Playing jump sound: JUMP (Pitch: 0.8)`
- `⚔️ Playing attack sound: attack1 (Pitch: 1.15)` (with variation)

**Bây giờ bạn có full control về pitch và loop cho tất cả sounds!** 🎚️🎵
