using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    private Dictionary<string, Component> tools = new Dictionary<string, Component>();

    public static GameManager Instance {
        get { return GetInstance(); }
        set { _instance = value; }
    }

    private static GameManager _instance;

    private static GameManager GetInstance() {
        if (_instance == null) {
            var go = new GameObject("Toolbox");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<GameManager>();
        }
        return _instance;
    }

    private void Awake() {
        this.AddTool<GameInput>("GameInput");
    }

    public ObjType GetTool<ObjType>(string objName) where ObjType : Component {
        return tools[objName] as ObjType;
    }

    public ObjType AddTool<ObjType>(string objName) where ObjType : Component {
        var tool = new GameObject(objName);
        tool.transform.SetParent(this.transform);
        ObjType obj = tool.AddComponent<ObjType>();
        this.tools.Add(objName, obj);
        return obj;
    }
}