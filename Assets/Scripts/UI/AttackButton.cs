using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : DraggableButton
{
    private void Update() {
        canDrag = uIController.gameManager.playerController.CanOverdrive;
        gaugeMeter.SetActive(canDrag);
    }
}
