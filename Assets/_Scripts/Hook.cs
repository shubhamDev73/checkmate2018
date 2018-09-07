using UnityEngine;

public class Hook : MonoBehaviour {
    public RodBalance parent;
    public int location;
    public bool occupied;
    public void attach(RodBalance weight)
    {
        parent[location] = weight;
        weight.myCol.enabled = false;
        weight.transform.position = transform.GetChild(0).position;
        weight.transform.SetParent(transform);
        occupied = true;
    }
    public RodBalance detach()
    {
        RodBalance temp = parent[location];
        temp.myCol.enabled = true;
        temp.transform.rotation = Quaternion.identity;
        temp.transform.SetParent(null);
        parent[location] = null;
        occupied = false;
        return temp;
    }
}
