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
        ComboText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerCombo() {
        
        if(ComboText.enabled == false) ComboText.enabled = true;

        comboAnimator.SetTrigger("Combo");
        ComboText.text = currentCombo + " Combo";
        StartCoroutine(ComboCounterCoroutine());
    }
    public void ResetCombo() {
        currentCombo = 0;
        StopCoroutine(ComboCounterCoroutine());
    }

    IEnumerator ComboCounterCoroutine() {
        elapsedTime = 0f;
        elapsedTime++;
        yield return null;
    }
}
