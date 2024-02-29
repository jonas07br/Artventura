using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class barravida : MonoBehaviour
{
    public float vidas;
    public static Text texto;
    public Image vidafill;
    public static bool mostrarinfo;
    public GameObject infoprefab;
    public Transform infopos;
    // Start is called before the first frame update
    void Start()
    {
        mostrarinfo = false;
    }

    // Update is called once per frame
    void Update()
    {
        updatevida();
        
    }
    private void updatevida()
    {
       
    }
}
