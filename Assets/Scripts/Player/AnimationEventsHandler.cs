using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// (NOTICE!) This script is a component of Character GameObject, NOT Player!
/// This script is used to handle animation events (which can be handled only in script associated with the animated GameObject).
/// </summary>
public class AnimationEventsHandler : MonoBehaviour
{
    //called when Appear animation ends
    public void AppearingFinished()
    {
        GameManager_OLD.GetPlayer().mobilitySwitch(); //unlocks player movement
        GameManager_OLD.GetLabirynth().SwapTeleportStartTile(); //change start teleport's tile into OFF mode
    }

    //called during shooting animations, when the real shot has to be done
    public void Shoot()
    {
        GameManager_OLD.GetPlayer().getPlayerObject().GetComponent<Shooting>().Shoot();
    }

    //called after disappearing animation when the level is finished
    public void LevelCompleted()
    {
        SceneManager.LoadScene("LabyrinthScene"); //next level
    }

    //called when player is dying
    public void Die()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
