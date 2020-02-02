using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class PlacementController : MonoBehaviour
{
    [Serializable]
    public struct Structure {
        public GameObject prefab;
        public float cost;
        public float comboResource;
        public float probablity;
    }

    public float currentResource;
    public Structure [] structures;
    public LayerMask placementMask;
    public float speedRotation;

    public AudioSource [] audioSpawnVariations;
    public AudioSource audioRotation;
    public AudioSource audioBlockedSpawn;
    public AudioSource audioComboSpawn;

    private List<int> audioSpawnVariationsIndexList;
    private float deltaTrail;
    private Quaternion prevRotation;
    private GameInput gi;
    private GameObject spawned;
    private PlacementEvaluator spawned_pe;
    private Ray ray;
    private RaycastHit hit;

    private void Awake() {
        audioSpawnVariationsIndexList = new List<int>();
        gi = GameManager.Instance.GetTool<GameInput>("GameInput");
    }

    private void Start() {
        Spawn(GenerateStructure());
    }

    private void Update() {
        if (spawned == null) return;
        ApplyPosition();
        ApplyRotation();
        if (gi.MouseButtonLeftDown) {
            if(spawned_pe.EvaluatePlacement()) {
                UpdateResources();
                Destroy(spawned_pe);
                if(audioSpawnVariationsIndexList.Count == 0) {
                    for (int i = 0; i < audioSpawnVariations.Length; i++)
                        audioSpawnVariationsIndexList.Add(i);
                }
                int index = UnityEngine.Random.Range(0, audioSpawnVariationsIndexList.Count - 1);
                audioSpawnVariations[audioSpawnVariationsIndexList[index]].Play();
                audioSpawnVariationsIndexList.RemoveAt(index);
                Spawn(GenerateStructure());
            } else {
                if (audioBlockedSpawn.isPlaying == false) audioBlockedSpawn.Play();
            }
        }
        currentResource = Mathf.Clamp(currentResource, 0f, Mathf.Infinity);
    }

    private void ApplyPosition() {
        ray = Camera.main.ScreenPointToRay(gi.MousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementMask, QueryTriggerInteraction.Ignore)) {
            spawned.transform.position = hit.point;
        }
    }

    private void ApplyRotation() {
        spawned.transform.Rotate(Vector3.up, gi.MouseScrollDelta.y * speedRotation);
        prevRotation = spawned.transform.rotation;
        if(gi.MouseScrollDelta.y == 0f) {
            deltaTrail = Mathf.Clamp01(deltaTrail - Time.deltaTime);
        } else {
            deltaTrail = 1f;
        }
        audioRotation.volume = deltaTrail;
    }

    private void Spawn(Structure structure) {
        spawned = Instantiate(structure.prefab, hit.point, prevRotation);
        spawned.name = structure.prefab.name;
        spawned_pe = spawned.GetComponent<PlacementEvaluator>();
        Utility.SetMaterialsColor(spawned_pe.clearColor, spawned.GetComponentsInChildren<Renderer>());
        currentResource -= structure.cost;
    }

    private void UpdateResources() {
        string [] names = spawned_pe.GetComboResourcesNames();
        //bool hasCombo = false;
        for(int i = 0; i < structures.Length; i++) {
            for(int j = 0; j < names.Length; j++) {
                if(structures[0].prefab.name == names[j]) {
                    currentResource += structures[0].comboResource;
                    //hasCombo = true;
                }
            }
        }
        //if(hasCombo) {
        //    if (audioComboSpawn.isPlaying == false) audioComboSpawn.Play();
        //}
        currentResource = Mathf.Clamp(currentResource, 0f, Mathf.Infinity);
    }

    private Structure GenerateStructure() {
        for(int i = structures.Length - 1; i > 0; i--) {
            if(structures[i].cost <= currentResource) {
                if(UnityEngine.Random.Range(0, 1f) <= structures[i].probablity) {
                    return structures[i];
                }
            }
        }
        return structures[0];
    }
}