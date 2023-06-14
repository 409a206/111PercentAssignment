using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : DraggableButton
{
    private void Update() {
        canDrag = uIController.gameManager.playerController.CanOverdrive;
        gaugeMeter.SetActive(canDrag);
        fillImage.GetComponent<Image>().fillAmount = (float)uIController.gameManager.playerController.CurrentHitStacks 
                                                    / (float)uIController.gameManager.playerController.RequiredHitStacksForOverdrive;

        if(dragDist >= maxDist - 10f && !_buttonPressed) TryActivate();
        //Debug.Log(dragDist);
    }

    public override void TryActivate()
    {
        Overdrive();
        base.TryActivate();
    }

    private void Overdrive() {
        uIController.gameManager.playerController.Overdrive(); 
    }
}
