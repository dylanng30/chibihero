using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string abc;
    public void LoadScene()
    {
        SceneManager.LoadScene(abc);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(abc);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RedPawn"))
            abc = "RedPawn";
        if (collision.gameObject.CompareTag("RedArcher"))
            abc = "RedArcher";

    }
}
