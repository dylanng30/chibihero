# 🔧 FIX AUDIO KHÔNG CHẠY - ChibiHero

## ❌ Vấn đề: Jump/Walk sound không chạy, không kéo được AudioSource

### 🔍 Nguyên nhân:
1. **MP3 files** cần import settings đúng trong Unity
2. **AudioSource** cần tạo manually 
3. **Audio Clips** chưa được assign đúng

## ✅ GIẢI PHÁP:

### Bước 1: Fix Import Settings cho MP3
1. Chọn file audio trong Project (VD: `Walking.MP3`, `JUMP.MP3`)
2. Trong Inspector, chỉnh:
   - **Load Type**: `Decompress On Load`
   - **Compression Format**: `PCM` (cho quality tốt) hoặc `Vorbis` (cho file nhỏ)
   - **Quality**: `100%`
3. Click **Apply**

### Bước 2: Setup AudioManager đúng cách
1. **Tạo AudioManager GameObject**:
   - Hierarchy → Right-click → Create Empty → Name: "AudioManager"
   - Add Component → AudioManager

2. **Tạo AudioSources**:
   - Right-click trên AudioManager component
   - Chọn **"Create AudioSources"**
   - Sẽ tự động tạo 3 AudioSources

3. **Assign Audio Clips**:
   - Kéo file audio từ Project vào field tương ứng:
     - `Walking.MP3` → **Walk Sound**
     - `JUMP.MP3` → **Jump Sound**
     - `attack1.wav`, `attack2.wav`, `attack3.wav` → **Attack Sounds** array
     - Background music → **Background Music**

### Bước 3: Test
1. Right-click AudioManager component → **"Test All Sounds"**
2. Check Console để xem debug messages:
   - ✅ `🚶 Playing walk sound: Walking`
   - ✅ `🦘 Playing jump sound: JUMP`
   - ❌ `Walk sound is null!` (nếu chưa assign)

### Bước 4: Nếu vẫn không hoạt động
**Thử convert MP3 sang WAV:**
1. Dùng tool online hoặc Audacity convert MP3 → WAV
2. Import WAV file vào Unity
3. Assign WAV file thay vì MP3

**Hoặc thử Vorbis format:**
1. Chọn MP3 file → Inspector
2. **Compression Format**: `Vorbis`
3. Click **Apply**

## 🎮 Test trong Game:
- **Di chuyển**: Console sẽ show `🚶 Playing walk sound`
- **Nhảy**: Console sẽ show `🦘 Playing jump sound`
- **Attack**: Console sẽ show `⚔️ Playing attack sound`

## 📝 Debug Checklist:
- [ ] AudioManager GameObject có trong scene
- [ ] AudioManager component được add
- [ ] 3 AudioSources được tạo (backgroundMusicSource, soundEffectSource, footstepSource)
- [ ] Audio clips được assign vào đúng field
- [ ] Enable Player Sounds = ✅
- [ ] Volume > 0
- [ ] Audio files được import đúng format

**Nếu làm theo đúng steps này, sound sẽ hoạt động 100%!** 🎵
