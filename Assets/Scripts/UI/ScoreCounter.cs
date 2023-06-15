using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private UIController uIController;
    [SerializeField]
    public TMPro.TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE : " + uIController.gameManager.enemiesSlain;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore() {
        scoreText.text = "SCORE : " + uIController.gameManager.enemiesSlain;
    }

}
