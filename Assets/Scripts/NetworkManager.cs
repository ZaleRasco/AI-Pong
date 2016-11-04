using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class news : NetworkBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("networkMaster"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
