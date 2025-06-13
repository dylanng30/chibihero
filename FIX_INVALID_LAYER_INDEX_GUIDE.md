# FIX "Invalid Layer Index '-1'" ERROR - HƯỚNG DẪN SỬA LỖI

## ❌ LỖI GÌ?
Lỗi "Invalid Layer Index '-1'" xảy ra khi:
- Animator Controller không có state animation với tên được yêu cầu
- Tên animation state trong Inspector không khớp với tên trong Animator Controller
- Animator Controller không được gán cho Player

## ✅ CÁCH SỬA NHANH

### 1. KIỂM TRA ANIMATOR CONTROLLER
```
1. Chọn Player object trong Scene
2. Kiểm tra Animator component có Controller không
3. Double-click Controller để mở Animator window
4. Xem các state có sẵn (ví dụ: Idle, Walk, Attack, etc.)
```

### 2. SỬ DỤNG AUTO-DETECT (KHUYẾN NGHỊ)
```
1. Chọn Player object
2. Tìm AbilityNormalATK component trong Inspector
3. Right-click component → "Auto Detect Attack Animation"
4. Script sẽ tự động tìm và set tên animation đúng
```

### 3. DEBUG ANIMATOR STATES
```
1. Chọn Player object
2. Tìm AnimationManager component
3. Right-click → "Debug Animator States"
4. Check Console để xem tất cả state có sẵn
```

### 4. CHỈNH TAY (NẾU CẦN)
```
1. Chọn Player object
2. Tìm AbilityNormalATK component
3. Trong "Animation Settings":
   - Enable Attack Animation: ✅ check
   - Attack Animation Name: nhập đúng tên state (ví dụ: "Attack", "attack", "Attack1")
```

## 🔧 CÁC CONTEXT MENU HỖ TRỢ

### AnimationManager:
- **Debug Animator States**: Hiển thị tất cả states và clips
- **Test Animation**: Test animation với tên trong Test Animation Name
- **Get Current Animation Info**: Xem thông tin animation hiện tại

### AbilityNormalATK:
- **Auto Detect Attack Animation**: Tự động tìm animation attack
- **Test Attack**: Test attack với settings hiện tại
- **Check Attack Setup**: Kiểm tra toàn bộ setup attack

## 📝 TÊN ANIMATION THƯỜNG GẶP
```
- Attack
- attack  
- Attack1
- BasicAttack
- NormalAttack
- Melee
- Hit
- Player_Attack
```

## ⚠️ LƯU Ý
1. **Tên state phải khớp CHÍNH XÁC** với tên trong Animator Controller
2. **Case-sensitive**: "Attack" ≠ "attack"
3. Nếu không có animation attack nào, có thể tắt "Enable Attack Animation"
4. Sound vẫn phát bình thường dù animation lỗi

## 🎯 KIỂM TRA CUỐI CÙNG
1. ✅ Player có Animator Controller
2. ✅ Controller có state Attack (hoặc tương tự)
3. ✅ Tên trong AbilityNormalATK khớp với tên state
4. ✅ Console không còn lỗi "Invalid Layer Index"
5. ✅ Attack sound phát bình thường
6. ✅ Animation attack chạy đúng

## 🚀 TỰ ĐỘNG HÓA
Script đã tự check và log warning rõ ràng:
- "Animation state 'XXX' not found" → Tên sai
- "No AnimatorController assigned" → Thiếu Controller
- "Available animation clips: ..." → List các clip có sẵn

Chỉ cần follow log để fix!
