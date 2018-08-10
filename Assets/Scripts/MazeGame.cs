
using System.Collections;
using UnityEngine;
using System;

public class MazeGame : MonoBehaviour {
    public float cost;
    public Vector2 blockDims;
    public GameObject player;
    public Vector2 coefficents;
    
    private float lastCost;
    private Vector2 lastPlateLocation;
    private bool updateXMode;
    private bool updateYMode; // Considers whether to update only in y or x
    void Start()
    {
        updateXMode = false;
        updateYMode = false;
    }
    void Update()
    {
        //Update the points
        if(updateXMode)
        {
            cost = lastCost-coefficents.x*(transform.position.x - lastPlateLocation.x)/blockDims.x;
        }
        if(updateYMode)
        {
            cost = lastCost*(float)Math.Pow(coefficents.y,(transform.position.z - lastPlateLocation.y)/blockDims.y);
        }
    }
    public void reached(Vector2 here)
    {
        if(updateXMode)
            if(here.x > lastPlateLocation.x)
                cost = lastCost-coefficents.x;
            else
                cost = lastCost+coefficents.x;
        
        if(updateYMode)
            if(here.y > lastPlateLocation.y)
                cost = lastCost*coefficents.y;
            else
                cost = lastCost/coefficents.y;
        
        updateXMode = false;
        updateYMode = false;
        
    }
    public void changeMode(bool inX, bool inY, Vector2 fromHere)
    {
        this.updateXMode = inX;
        this.updateYMode = inY;
        lastPlateLocation = fromHere;
        lastCost = cost;
    }

}
