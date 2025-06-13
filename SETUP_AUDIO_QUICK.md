# 🎵 HƯỚNG DẪN SETUP AUDIO NHANH

## Bước 1: Tạo AudioManager
1. Trong Unity Hierarchy: **Right-click** → **Create Empty** → Đặt tên **"AudioManager"**
2. Select AudioManager GameObject
3. Trong Inspector: **Add Component** → **AudioManager**

## Bước 2: Kéo thả Audio Clips
Từ folder `Assets/Sounds/`, kéo thả các file sau vào AudioManager component:

### Background Music:
- Kéo `BACKGROUND/Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered.mp3` → **Background Music**

### Player Sounds:
- Kéo `SOUND_EFFECT/PLAYER+KNIGHT/Walking.MP3` → **Walk Sound**
- Kéo `SOUND_EFFECT/PLAYER+KNIGHT/JUMP.MP3` → **Jump Sound**
- Mở **Attack Sounds** array → Size = 3
- Kéo `attack1.wav`, `attack2.wav`, `attack3.wav` vào 3 element

### UI Sounds:
- Kéo `MENU_SOUND/menu.wav` → **Menu Sound**

## Bước 3: Chỉnh Settings (tùy chọn)
- **Music Volume**: 0.5
- **Player Sound Volume**: 0.7
- **UI Sound Volume**: 0.6
- **Master Volume**: 1.0
- Tick ✅ **Enable Music**, **Enable Player Sounds**, **Enable UI Sounds**

## Bước 4: Test
- Right-click trên AudioManager component
- Chọn **"Test All Sounds"**
- Hoặc nhấn Play và test bằng cách di chuyển player

## ✅ XONG!
- **Đi bộ**: Sound walking tự động loop
- **Nhảy**: Sound jump khi nhấn Space
- **Attack**: Random attack sound khi click chuột
- **Background music**: Phát liên tục

**Tất cả sound sẽ hoạt động tự động, không cần code thêm!**
