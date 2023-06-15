using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : DraggableButton
{
    private void Update() {
        
        canDrag = uIController.gameManager.playerController.CanDash;
        gaugeMeter.SetActive(canDrag);
        fillImage.GetComponent<Image>().fillAmount = (float)uIController.gameManager.playerController.CurrentJumpStacks 
                                                    / (float)uIController.gameManager.playerController.RequiredJumpStacksForDash;

        if(dragDist >= maxDist - 10f && !_buttonPressed) TryActivate();
        //Debug.Log(dragDist);
    }
    public override void TryActivate()
    {
        Dash();
        base.TryActivate();
    }

    private void Dash()
    {
        uIController.gameManager.playerController.Dash(); 
    }
}
