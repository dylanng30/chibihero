using UnityEngine;

public static class AudioManager
{
    private static AudioSystem audioSystem;
      private static AudioSystem AudioSystem
    {
        get
        {
            if (audioSystem == null)
            {
                audioSystem = AudioSystem.Instance;
            }
            return audioSystem;
        }
    }

    #region Music
    public static void PlayBackgroundMusic(string musicName, bool fadeIn = true)
    {
        AudioSystem?.PlayMusic(musicName, fadeIn);
    }
    
    public static void PlayDefaultBackgroundMusic(bool fadeIn = true)
    {
        AudioSystem?.PlayDefaultBackgroundMusic(fadeIn);
    }
    
    public static void PlayBackgroundMusicForScene(string sceneName, bool fadeIn = true)
    {
        AudioSystem?.PlayBackgroundMusicForScene(sceneName, fadeIn);
    }
    
    public static void StopBackgroundMusic(bool fadeOut = true)
    {
        AudioSystem?.StopMusic(fadeOut);
    }
    #endregion

    #region Player Sounds
    public static void PlayPlayerJump(Vector3 position = default)
    {
        AudioSystem?.PlayPlayerSound(PlayerSoundType.Jump, position);
    }

    public static void PlayPlayerWalk(Vector3 position = default)
    {
        AudioSystem?.PlayPlayerSound(PlayerSoundType.Walk, position);
    }

    public static void StopPlayerWalk()
    {
        AudioSystem?.StopWalkSound();
    }

    public static void PlayPlayerAttack(int attackIndex, Vector3 position = default)
    {
        PlayerSoundType attackType = attackIndex switch
        {
            1 => PlayerSoundType.Attack1,
            2 => PlayerSoundType.Attack2,
            3 => PlayerSoundType.Attack3,
            _ => PlayerSoundType.Attack1
        };
        AudioSystem?.PlayPlayerSound(attackType, position);
    }

    public static void StopPlayerAttack()
    {
        AudioSystem?.StopAttackSound();
    }

    public static void StopPlayerJump()
    {
        AudioSystem?.StopJumpSound();
    }

    public static void StopAllPlayerSounds()
    {
        AudioSystem?.StopAllPlayerSounds();
    }

    public static void PlayPlayerDeath(Vector3 position = default)
    {
        AudioSystem?.PlayPlayerSound(PlayerSoundType.Death, position);
    }

    public static void PlayPlayerHurt(Vector3 position = default)
    {
        AudioSystem?.PlayPlayerSound(PlayerSoundType.Hurt, position);
    }
    #endregion

    #region Enemy Sounds
    public static void PlayEnemyAttack(string enemyType, Vector3 position = default)
    {
        AudioSystem?.PlayEnemySound(enemyType, EnemySoundType.Attack, position);
    }

    public static void PlayEnemyDeath(string enemyType, Vector3 position = default)
    {
        AudioSystem?.PlayEnemySound(enemyType, EnemySoundType.Death, position);
    }

    public static void PlayEnemyHurt(string enemyType, Vector3 position = default)
    {
        AudioSystem?.PlayEnemySound(enemyType, EnemySoundType.Hurt, position);
    }
    #endregion

    #region Boss Sounds
    public static void PlayBossAttack(string bossType, Vector3 position = default)
    {
        AudioSystem?.PlayBossSound(bossType, "attack", position);
    }

    public static void PlayBossLaugh(string bossType, Vector3 position = default)
    {
        AudioSystem?.PlayBossSound(bossType, "laugh", position);
    }

    public static void PlayBossSpecial(string bossType, string specialAction, Vector3 position = default)
    {
        AudioSystem?.PlayBossSound(bossType, specialAction, position);
    }
    #endregion

    #region UI Sounds
    public static void PlayUIClick()
    {
        AudioSystem?.PlayUI("Mouse-Click-Sound-Effect");
    }

    public static void PlayMenuSound()
    {
        AudioSystem?.PlayUI("menu");
    }
    #endregion

    #region Environment Sounds
    public static void PlayWaterSplash(Vector3 position = default)
    {
        AudioSystem?.PlaySFX("FALL-IN-TO-WATER", position);
    }

    public static void PlayDoorOpen(Vector3 position = default)
    {
        AudioSystem?.PlaySFX("OPEN_DOOR", position);
    }

    public static void PlayDoorClose(Vector3 position = default)
    {
        AudioSystem?.PlaySFX("CLOSE_DOOR", position);
    }
    #endregion

    #region Volume Controls
    public static void SetMasterVolume(float volume)
    {
        AudioSystem?.SetMasterVolume(volume);
    }

    public static void SetMusicVolume(float volume)
    {
        AudioSystem?.SetMusicVolume(volume);
    }

    public static void SetSFXVolume(float volume)
    {
        AudioSystem?.SetSFXVolume(volume);
    }

    public static void SetUIVolume(float volume)
    {
        AudioSystem?.SetUIVolume(volume);
    }
    #endregion

    #region Generic Sound
    public static void PlaySound(string soundName, Vector3 position = default, float volumeMultiplier = 1f)
    {
        AudioSystem?.PlaySFX(soundName, position, volumeMultiplier);
    }
    #endregion
}
