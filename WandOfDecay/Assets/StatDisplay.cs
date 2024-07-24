using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text area;
    [SerializeField]
    private TMPro.TMP_Text tree;
    [SerializeField]
    private TMPro.TMP_Text bush;
    [SerializeField]
    private TMPro.TMP_Text plant;
    // Start is called before the first frame update
    void Start()
    {
        var stat = ServiceManager.Instance.Get<Stats>();
        area.text = "" + stat[DecayableType.Soil];
        tree.text = "" + stat[DecayableType.Tree];
        bush.text = "" + stat[DecayableType.Bush];
        plant.text = "" + stat[DecayableType.Plant];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
