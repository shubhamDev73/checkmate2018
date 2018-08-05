using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MonkeyDoor : MonoBehaviour{    
    private static bool[] monkeyIsIn = {true,true,true,true,true,true};
    private static int size = 6;
    public int doorId;
    void Start(){
    }
    
    bool winCheck(int choice)
    {
        int index = -1;
        int onlyOneLeft = 2;
        for(int data = 0; data<size; data++)
        {
            if(monkeyIsIn[data])
            {
                index = data;
                --onlyOneLeft;
            }
        }
        if(onlyOneLeft == 1 && index == choice )
        {
            return true;
        }else
        {
            return false;
        }
    }

    void monkeyUpdate(int guess)
    {
        bool[] temp = new bool[size];
        for(int i = 0; i < size; i++)
        {
            temp[i] = false;
        }
        
        monkeyIsIn[guess] = false;
        
        if(monkeyIsIn[0])
        {
            temp[1]=true;
            temp[0]=false;
        }
        if(monkeyIsIn[size-1])
        {
            temp[size-2]=true;
            temp[size-1]=false;
        }
        for(int data = 1; data<(size-1); data++)
        {
            if(monkeyIsIn[data])
            {
                temp[data-1]=true;
                temp[data+1]=true;
            }
        }
        monkeyIsIn = temp;
    }
    void OnTriggerStay(Collider col){
        if(Input.GetButtonDown("Fire1"))
        {
            openDoor();
        }
    }

    void openDoor(){
        if(winCheck(doorId)){
            Debug.Log("Wow You Found Me!!!");
            monkeyIsIn[doorId]= false;
        }
        else{
            monkeyUpdate(doorId);
        }
    }
    public void Update()
    {
        if(monkeyIsIn[doorId])
            GetComponent<Renderer>().material.color = Color.green;
        else
            GetComponent<Renderer>().material.color = Color.grey;
    }
}
