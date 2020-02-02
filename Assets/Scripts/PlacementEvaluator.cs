using System.Collections.Generic;
using UnityEngine;

public class PlacementEvaluator : MonoBehaviour
{
    public Trigger triggerResource;
    public Trigger triggerPlacement;

    public Color blockColor;
    public Color comboColor;
    public Color clearColor;

    private List<Collider> comboList;
    private List<Collider> blockerList;

    private void Awake() {
        blockerList = new List<Collider>();
        comboList = new List<Collider>();
        triggerPlacement.onEnter = AddToBlocker;
        triggerPlacement.onExit = RemoveFromBlocker;
        triggerResource.onEnter = AddToCombo;
        triggerResource.onExit = RemoveFromCombo;
    }

    public bool EvaluatePlacement() {
        if (blockerList.Count != 0) return false;
        return true;
    }

    public string [] GetComboResourcesNames() {
        string[] names = new string[comboList.Count];
        for (int i = 0; i < comboList.Count; i++)
            names[i] = comboList[i].transform.parent.name;
        return names;
    }

    private void AddToBlocker(Collider col) {
        if (blockerList.Contains(col)) return;
        blockerList.Add(col);
        Utility.SetMaterialsColor(blockColor, col.transform.parent.GetComponentsInChildren<Renderer>());
        Utility.SetMaterialsColor(blockColor, GetComponentsInChildren<Renderer>());
    }

    private void RemoveFromBlocker(Collider col) {
        if (blockerList.Contains(col) == false) return;
        blockerList.Remove(col);
        Utility.SetMaterialsColor((comboList.Contains(col)) ? comboColor : clearColor,
            col.transform.parent.GetComponentsInChildren<Renderer>());
        if (blockerList.Count == 0) {
            Utility.SetMaterialsColor((comboList.Count == 0) ? clearColor : comboColor,
                GetComponentsInChildren<Renderer>());
        }
    }

    private void AddToCombo(Collider col) {
        if (comboList.Contains(col)) return;
        comboList.Add(col);
        if (blockerList.Count == 0) {
            Utility.SetMaterialsColor(comboColor, col.transform.parent.GetComponentsInChildren<Renderer>());
            Utility.SetMaterialsColor(comboColor, GetComponentsInChildren<Renderer>());
        }
    }

    private void RemoveFromCombo(Collider col) {
        if (comboList.Contains(col) == false) return;
        comboList.Remove(col);
        Utility.SetMaterialsColor((blockerList.Contains(col)) ? blockColor : clearColor,
            col.transform.parent.GetComponentsInChildren<Renderer>());
        if (blockerList.Count == 0) {
            Utility.SetMaterialsColor((comboList.Count == 0) ? clearColor : comboColor,
                GetComponentsInChildren<Renderer>());
        }
    }

    private void OnDestroy() {
        Destroy(triggerResource.gameObject);
        Destroy(triggerPlacement.gameObject);
        GetComponentInChildren<Collider>().enabled = true;
        comboList.Clear();
        blockerList.Clear();
    }
}
