using UnityEngine;

public class Utility : MonoBehaviour
{
    public static void SetMaterialsColor(Color color, Renderer[] rends) {
        for (int j = 0; j < rends.Length; j++)
            for (int i = 0; i < rends[j].materials.Length; i++)
                rends[j].materials[i].color = color;
    }
}
