
using System.Collections;
using UnityEngine;
using System;

public class MazeGame : MonoBehaviour {
    public float cost;
    public Vector2 blockDims;
    public GameObject player;
    public Vector2 coefficents;
    public float plateDims;
    public GameObject wall;
    public Vector2 axisY;
    public Vector2 axisX;

    private Vector2 invAxisY;
    private Vector2 invAxisX;
    private Vector2 position;    
    private float lastCost;
    private Vector2 lastPlateLocation;
    private bool updateXMode;
    private bool updateYMode; // Considers whether to update only in y or x
    void Start()
    {
        updateXMode = false;
        updateYMode = false;
        invAxisX = new Vector2(axisY.y,-axisX.y);
        invAxisY = new Vector2(-axisY.x, axisX.x);
    }
    void Update()
    {
        position = convert(new Vector2(transform.position.x,transform.position.z));
        //Update the points
        
        // if(updateXMode)
        // {
        //     cost = lastCost-coefficents.x*(position.x - lastPlateLocation.x)/blockDims.x;
        // }
        // if(updateYMode)
        // {
        //     cost = lastCost*(float)Math.Pow(coefficents.y,(position.x - lastPlateLocation.y)/blockDims.y);
        // }
    }

    public Vector2 convert(Vector2 x)
    {
        return new Vector2(Vector2.Dot(x,axisX),Vector2.Dot(x,axisY));
    }
    public Vector2 inverseConvert(Vector2 x)
    {
        
        return new Vector2(Vector2.Dot(x,invAxisX),Vector2.Dot(x,invAxisY));
    }
    public void reached(Vector2 here)
    {
        
        if(updateXMode)
            if(here.x > lastPlateLocation.x)//
                cost = lastCost-coefficents.x;
            else
                cost = lastCost+coefficents.x;
        
        if(updateYMode)
            if(here.y > lastPlateLocation.y)//
                cost = lastCost*coefficents.y;
            else
                cost = lastCost/coefficents.y;
        
        updateXMode = false;
        updateYMode = false;
        lastCost = cost;
    }

    public void left(Vector2 prevLoc)
    {
        prevLoc = convert(prevLoc);
        float deltaX = position.x - prevLoc.x;
        float deltaY = position.y - prevLoc.y;
        deltaX = Math.Abs(deltaX);
        deltaY = Math.Abs(deltaY);
        if(deltaX > deltaY)
        {
            // player.changeMode(true,false,position);
            this.updateXMode = true;
            this.updateYMode = false;

            Vector2 actualPos = inverseConvert(position);
            Vector3 wallPosition = new Vector3(actualPos.x,transform.position.y,actualPos.y);
//            wallPosition += new Vector3(plateDims*deltaX,transform.position.y,0);            
//            Instantiate(wall,wallPosition,Quaternion.identity);//replace quaternion.identity with a rotation
        }
        else
        {
            // player.changeMode(false,true,new Vector2 (position.x, position.z));
            this.updateXMode = false;
            this.updateYMode = true;

            Vector2 actualPos = inverseConvert(position);
            Vector3 wallPosition = new Vector3(actualPos.x,transform.position.y,actualPos.y);
//            wallPosition += new Vector3(0,transform.position.y,plateDims*deltaY);//replace quaternion.identity with a rotation
//            Instantiate(wall,wallPosition,Quaternion.identity);
        }
        lastPlateLocation = prevLoc;
        lastCost = cost;
    }

    

    // public void changeMode(bool inX, bool inY, Vector2 fromHere)
    // {
    //     this.updateXMode = inX;
    //     this.updateYMode = inY;
    //     lastPlateLocation = fromHere;
    //     lastCost = cost;
    // }

}
