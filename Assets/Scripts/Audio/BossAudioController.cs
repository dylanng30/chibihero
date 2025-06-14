using UnityEngine;

public class BossAudioController : MonoBehaviour
{
    [Header("Boss Audio Settings")]
    [SerializeField] private string bossType = "King"; // "King", "Pirate", etc.
    [SerializeField] private float audioRange = 20f; // Range within which boss audio can be heard

    private Transform player;

    private void Start()
    {
        // Find player reference
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    public void PlayBossLaugh()
    {
        if (IsPlayerInRange())
        {
            AudioManager.PlayBossLaugh(bossType, transform.position);
        }
    }

    public void PlayBossAttack()
    {
        if (IsPlayerInRange())
        {
            AudioManager.PlayBossAttack(bossType, transform.position);
        }
    }

    public void PlayBossSpecialAction(string actionName)
    {
        if (IsPlayerInRange())
        {
            AudioManager.PlayBossSpecial(bossType, actionName, transform.position);
        }
    }

    public void PlayKingHammer()
    {
        if (bossType.ToLower() == "king" && IsPlayerInRange())
        {
            AudioManager.PlaySound("HAMMER", transform.position);
        }
    }

    public void PlayDoorOpen()
    {
        if (IsPlayerInRange())
        {
            AudioManager.PlayDoorOpen(transform.position);
        }
    }

    public void PlayDoorClose()
    {
        if (IsPlayerInRange())
        {
            AudioManager.PlayDoorClose(transform.position);
        }
    }

    public void PlayPirateSlash(int slashNumber = 1)
    {
        if (bossType.ToLower() == "pirate" && IsPlayerInRange())
        {
            string soundName = $"SLASH{slashNumber}";
            AudioManager.PlaySound(soundName, transform.position);
        }
    }

    public void PlayPirateArr()
    {
        if (bossType.ToLower() == "pirate" && IsPlayerInRange())
        {
            AudioManager.PlaySound("ARR_PIRATE", transform.position);
        }
    }

    private bool IsPlayerInRange()
    {
        if (player == null) return true; // Play anyway if no player found
        
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= audioRange;
    }

    public void SetBossType(string newBossType)
    {
        bossType = newBossType;
    }

    // This method can be called from animation events
    public void PlayAudioEvent(string eventName)
    {
        switch (eventName.ToLower())
        {
            case "laugh":
                PlayBossLaugh();
                break;
            case "attack":
                PlayBossAttack();
                break;
            case "hammer":
                PlayKingHammer();
                break;
            case "door_open":
                PlayDoorOpen();
                break;
            case "door_close":
                PlayDoorClose();
                break;
            case "slash1":
                PlayPirateSlash(1);
                break;
            case "slash2":
                PlayPirateSlash(2);
                break;
            case "arr":
                PlayPirateArr();
                break;
            default:
                AudioManager.PlaySound(eventName, transform.position);
                break;
        }
    }
}
