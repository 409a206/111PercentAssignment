using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text ComboText;
    private int currentCombo;

    [Tooltip("콤보 지속 시간")]
    [SerializeField]
    private float comboDuration = 10.0f;
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
        StopCoroutine(ComboCounterCoroutine());
        currentCombo++;
        comboAnimator.SetTrigger("Combo");
        ComboText.text = currentCombo + " Combo";
        StartCoroutine(ComboCounterCoroutine());
    }
    private void ResetCombo() {
        currentCombo = 0;
        //StopCoroutine(ComboCounterCoroutine());
    }

    IEnumerator ComboCounterCoroutine() {

        elapsedTime = 0f;
        
        while(elapsedTime < comboDuration) {
            elapsedTime+= Time.deltaTime;
            if(elapsedTime >= comboDuration) elapsedTime = comboDuration;
            yield return null;
        }
        ResetCombo();
    }
}
