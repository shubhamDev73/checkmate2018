using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MonkeyDoor : MonoBehaviour{
//    public GameObject []doors;
    
    private static int[] monkey = {1,1,1,1,1,1};
    private static int size = 6;
    private static int numDoors = 0;
    public int doorId;

    public void Start(){
    }
    
    private bool winCheck(int choice)
    {
//        Debug.Log("Wincheck enter");
        int index = -1;
        int onlyOneLeft = 2;
        for(int data = 0; data<size; data++)
        {
            if(monkey[data] == 1)
            {
                index = data;
                --onlyOneLeft;
            }
        }
//        Debug.Log("Wincheck exit");
        if(onlyOneLeft == 1 && index == choice )
        {
            return true;
        }else
        {
            return false;
        }
    }

    private void monkeyUpdate(int guess)
    {
//        Debug.Log("update enter "+guess);
        int[] temp = new int[size];
        
        monkey[guess] = 0;
        if(monkey[0]==1)
        {
            temp[1]=1;
            temp[0]=0;
        }
        if(monkey[size-1]==1)
        {
            temp[size-2]=1;
            temp[size-1]=0;
        }
        for(int data = 1; data<(size-1); data++)
        {
            if(monkey[data] == 1)
            {
                temp[data-1]=1;
                temp[data+1]=1;
                temp[data]=0;
            }
        }
//        Debug.Log("update exit");
    }
    public void OnMouseDown()
    {
        if(winCheck(doorId))
            Debug.Log("Wow You Found Me!!!");
        else{
            monkeyUpdate(doorId);
            for(int i = 0; i<size; i++)
            {
                Debug.Log(monkey[i]);
            }
        }
                
    }

    public void Update()
    {
        
    }
}
