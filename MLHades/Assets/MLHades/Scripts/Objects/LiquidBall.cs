using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidBall : MonoBehaviour
{
    private Material material;
    private int enemiesKilled = 0;
    [SerializeField] private RoomController roomController;

    private void Awake() {
        material = GetComponent<MeshRenderer>().material;
    }

    public void EmptyLiquid(){
        material.SetFloat("Vector1_Fill", 0);
    }

    public void SetFill(){
        if(roomController!=null){
            enemiesKilled++;
            var fill = (float)enemiesKilled/(roomController.GetEnemiesPerWave()*roomController.GetNumberOfWaves());
            material.SetFloat("Vector1_Fill", fill);
        }
    }
}
