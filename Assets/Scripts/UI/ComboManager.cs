using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text ComboText;
    private int currentCombo;

    [SerializeField]
    private float comboDuration;
    [SerializeField]
    private Animator comboAnimator;
    private float elapsedTime;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerCombo() {
        comboAnimator.SetTrigger("Combo");
        ComboText.text = currentCombo + " Combo";
    }
    public void ResetCombo() {
        currentCombo = 0;
    }
}
