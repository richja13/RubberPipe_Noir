using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GmScript : MonoBehaviour
{
    public static GmScript Instance;
    public TMP_Text KillCountText;
    public static int KillCount;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }
    void Start()
    {
        KillCount = 32;
        KillCountText = GameObject.Find("KillCount").GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        KillCountText.text = "KILL COUNT: " + KillCount;
    }
}
