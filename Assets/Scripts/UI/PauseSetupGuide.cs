/*
HƯỚNG DẪN THIẾT LẬP PAUSE MENU

1. TẠO PAUSE CANVAS:
   - Tạo một GameObject mới trong scene
   - Đặt tên là "PauseCanvas"
   - Thêm component Canvas
   - Thêm component CanvasScaler
   - Thêm component GraphicRaycaster
   - Thêm component IgnoreTimeScale (script này)

2. TẠO PAUSE MENU UI:
   - Tạo Panel con trong PauseCanvas
   - Đặt tên là "PausePanel"
   - Thiết kế UI với các button:
     * Resume Button
     * Settings Button (tùy chọn)
     * Main Menu Button
     * Quit Button

3. THIẾT LẬP PAUSE MENU SCRIPT:
   - Thêm component PauseMenu vào PausePanel
   - Kéo thả các button vào các field tương ứng trong PauseMenu script

4. SETUP MANAGERS:
   - Đảm bảo có PauseManager trong scene (tự động tạo khi cần)
   - Đảm bảo có InputManager trong scene
   - Đảm bảo có UIManager trong scene

5. THIẾT LẬP CANVAS SORT ORDER:
   - PauseCanvas nên có Sort Order cao (ví dụ: 100)
   - Để đảm bảo pause menu hiển thị trên tất cả UI khác

6. TEST:
   - Chạy game
   - Nhấn ESC để pause/unpause
   - Test các button trong pause menu

CONTROLS:
- ESC: Pause/Resume game
- Pause Menu buttons: Resume, Settings, Main Menu, Quit

FEATURES:
- Game time dừng hoàn toàn khi pause (Time.timeScale = 0)
- UI vẫn hoạt động bình thường
- Có thể pause trong Exploring và Fighting states
- Không thể pause trong Menu và GameOver states
- Tự động resume khi chuyển scene hoặc state
*/

using UnityEngine;

public class PauseSetupGuide : MonoBehaviour
{
    [Header("Setup Guide")]
    [TextArea(10, 20)]
    public string instructions = "Xem comment trong script này để biết cách setup pause menu";

    void Start()
    {
        Debug.Log("Pause System Setup Complete! Press ESC to pause during gameplay.");
    }
}
