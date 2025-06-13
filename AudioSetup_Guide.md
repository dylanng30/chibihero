# Audio System Setup Guide - ChibiHero Game

## Hướng dẫn setup hệ thống audio đơn giản

### Bước 1: Tạo AudioManager GameObject
1. Tạo Empty GameObject trong scene và đặt tên là "AudioManager"
2. Add component AudioManager script vào GameObject này

### Bước 2: Setup Audio Clips
Kéo thả các file audio từ folder Assets/Sounds vào AudioManager component:

#### Background Music:
- Kéo file `Assets/Sounds/BACKGROUND/Phoenix-Wright-Ace-Attorney-OST-Pressing-Pursuit-_-Cornered.mp3` vào field **Background Music**

#### Player Sounds:
- **Walk Sound**: Kéo `Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/Walking.MP3`
- **Jump Sound**: Kéo `Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/JUMP.MP3`
- **Attack Sounds**: Kéo tất cả 3 file attack vào array:
  - `Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/attack1.wav`
  - `Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/attack2.wav`
  - `Assets/Sounds/SOUND_EFFECT/PLAYER+KNIGHT/attack3.wav`

#### UI Sounds:
- **Menu Sound**: Kéo `Assets/Sounds/MENU_SOUND/menu.wav`

### Bước 3: Chỉnh Volume
- **Music Volume**: 0.5 (50%)
- **Player Sound Volume**: 0.7 (70%)
- **UI Sound Volume**: 0.6 (60%)
- **Master Volume**: 1.0 (100%)

### Bước 4: Enable/Disable Sounds
Tick/Untick các checkbox để bật/tắt:
- ✅ Enable Music
- ✅ Enable Player Sounds  
- ✅ Enable UI Sounds

### Bước 5: Test Sounds
1. Right-click trên AudioManager component
2. Chọn "Test All Sounds" để test tất cả sound
3. Chọn "Auto Setup Audio Clips" để xem hướng dẫn chi tiết

## Cách hoạt động:

### Player Movement:
- **Đi bộ**: Sound walking sẽ loop khi player di chuyển trên mặt đất
- **Nhảy**: Sound jump sẽ phát khi nhấn nút jump
- **Dash**: Sound jump sẽ phát khi dash
- **Attack**: Random 1 trong 3 attack sound khi attack

### Background Music:
- Phát tự động khi game start
- Loop liên tục trong suốt game

### UI Sounds:
- Menu sound khi click button trong UI

## Lưu ý:
- Không cần code gì thêm, chỉ cần kéo thả trong Inspector
- Sound sẽ tự động phát khi player thực hiện action
- Background music và sound effects phát đồng thời không conflict
- Có thể chỉnh volume và bật/tắt từng loại sound riêng biệt

## Troubleshooting:
- Nếu không có sound: Kiểm tra AudioManager GameObject có trong scene không
- Nếu sound không phát: Kiểm tra AudioClip đã được assign và Enable = true
- Nếu volume nhỏ: Kiểm tra Master Volume và volume của từng loại sound
