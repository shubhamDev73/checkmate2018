using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeGame : MonoBehaviour {
    public float points; 
    public Vector2 blockDims;
    public GameObject player;
    public float delta;

    public Vector2 cost;
    private Vector2 playerPostion;
    private Vector2 lastPosition;
    private float tempCostInY;
    private float tempCostInX;
    void Start (){
        
        playerPostion = new Vector2((float)player.transform.position.x,(float)player.transform.position.z);
        lastPosition = playerPostion;
        tempCostInY = 0 ;
        tempCostInX = 0 ; 
    }
    void Update(){
        playerPostion = new Vector2((float)player.transform.position.x,(float)player.transform.position.z);
        if((playerPostion-lastPosition).magnitude >=delta)
        {
            updateCost(playerPostion-lastPosition);
            lastPosition = playerPostion;
        }
        Debug.Log(points);
    }
    void updateCost(Vector2 diff){
        tempCostInX += (diff.x);
        if(Math.Abs(tempCostInX)>=blockDims.x){
            points += (tempCostInX/blockDims.x)*cost.x;
            if(tempCostInX>0)
            {
                tempCostInX -= blockDims.x;
            }else
            {
                tempCostInX += blockDims.x;
            }
        }
        tempCostInY += (diff.y);
        if(Math.Abs(tempCostInY)>=blockDims.y)
        {
            points *=(float)Math.Pow(cost.y,tempCostInY/blockDims.y);
            tempCostInY -= 0;
            if(tempCostInY>0)
            {
                tempCostInY -= blockDims.y;
            }else
            {
                tempCostInY += blockDims.y;
            }
        }

    }
}
