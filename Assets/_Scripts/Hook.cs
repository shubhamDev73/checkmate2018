using UnityEngine;

public class Hook : MonoBehaviour {
    public RodBalance parent;
    public int location;
    public bool occupied;
    public void attach(RodBalance weight)
    {
        parent[location] = weight;
        weight.transform.position = transform.GetChild(0).position;
        occupied = true;
    }
    public RodBalance detach()
    {
        RodBalance temp = parent[location];
        parent[location] = null;
        occupied = false;
        return temp;
    }
}
