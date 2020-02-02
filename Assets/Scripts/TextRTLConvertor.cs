using UnityEngine;
using UnityEngine.UI;
 
 public class TextRTLConvertor : MonoBehaviour
{
    void Awake() {
        Text text = GetComponent<Text>();
        string str = text.text;
        text.text = string.Empty;
        for(int i = 0; i < str.Length; i++) {
            text.text += str[str.Length - 1 - i];
        }
        Destroy(this);
    }

}