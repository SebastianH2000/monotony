using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class TutorialCard : MonoBehaviour
{
    private Animator AN;

    private void Awake() {
        AN = GetComponent<Animator>();
        Debug.Log(SceneManager.GetActiveScene().name + "Tutorial");
        AN.Play(SceneManager.GetActiveScene().name + "Tutorial");
        Time.timeScale = 0;
    }

    private void OnMouseDown()
    {
        AN.StopPlayback();
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
