using System.Collections;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    private GameInput gi;

    public AudioSource audioUIClick;
    public PlacementController pc;
    public CanvasGroup group1;
    public CanvasGroup group2;
    public CanvasGroup group3;
    public CanvasGroup background;

    private void Awake() {
        gi = GameManager.Instance.GetTool<GameInput>("GameInput");
        StartCoroutine(TitleSequence());
    }

    private IEnumerator TitleSequence() {
        yield return new WaitUntil(()=> gi.AnyKeyDown);
        audioUIClick.Play();
        while (group1.alpha != 0) {
            group1.alpha -= Time.deltaTime;
            yield return null;
        }
        while (group2.alpha != 1) {
            group2.alpha += Time.deltaTime;
            yield return null;
        }
        yield return new WaitUntil(() => gi.AnyKeyDown);
        audioUIClick.Play();
        while (group2.alpha != 0) {
            group2.alpha -= Time.deltaTime;
            yield return null;
        }
        while (group3.alpha != 1) {
            group3.alpha += Time.deltaTime;
            yield return null;
        }
        yield return new WaitUntil(() => gi.AnyKeyDown);
        audioUIClick.Play();
        while (group3.alpha != 0) {
            group3.alpha -= Time.deltaTime;
            background.alpha = group3.alpha;
            yield return null;
        }
        pc.Init();
    }
}
