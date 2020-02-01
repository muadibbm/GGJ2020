using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlacementController : MonoBehaviour
{
    public GameObject[] structures;
    public LayerMask placementMask;

    private GameInput gi;
    private GameObject spawned;
    private Ray ray;
    private RaycastHit hit;

    private void Awake() {
        gi = GameManager.Instance.GetTool<GameInput>("GameInput");
    }

    public void Spawn(string name) {
        if (spawned) Destroy(spawned);
        for(int i = 0; i < structures.Length; i++) {
            if(structures[i].name == name) {
                spawned = Instantiate(structures[i], hit.point, Quaternion.identity);
                // TODO : set material to transparent
            }
        }
    }

    private void Update() {
        ray = Camera.main.ScreenPointToRay(gi.MousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementMask, QueryTriggerInteraction.Ignore)) {
            if (spawned) spawned.transform.position = hit.point;
        }
        if(gi.MouseButtonLeftDown) {
            if (spawned) {
                // TODO : set material to solid
                spawned = null;
            }
        }
    }
}
