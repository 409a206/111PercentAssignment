using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : DraggableButton
{
    private void Update() {
        canDrag = uIController.gameManager.playerController.CanDash;
    }
}
