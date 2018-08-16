
using UnityEngine;
using System.Collections;
using System;

public class MazePlate : MonoBehaviour
{
    public MazeGame player;
    void OnTriggerEnter(Collider other)
    {
        //Make Wall
        if(true) // add condition to check that this is a player
        {
            player.reached(new Vector2(transform.position.x,transform.position.z));
        }
    }
    void OnTriggerExit (Collider other)
    {

        if(other.gameObject == player.gameObject) // Add condition to check that this is a player
        {
            Debug.Log("enter!!");
            player.left(new Vector2(transform.position.x,transform.position.z));
            // spawnWall();
            // float deltaX = other.transform.position.x - transform.position.x;
            // float deltaY = other.transform.position.z - transform.position.z;
            // deltaX = Math.Abs(deltaX);
            // deltaY = Math.Abs(deltaY);
            // if(deltaX > deltaY)
            // {
            //     //player.changeMode(true,false,transform.position);
                
               
            // }
            // else
            // {
            //     //player.changeMode(false,true,new Vector2 (transform.position.x, transform.position.z));
            // }
            // // Make Wall
            
        }
    }

}
