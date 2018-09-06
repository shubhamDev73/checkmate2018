using System;
using System.Collections;
using UnityEngine;

public class RodBalance : MonoBehaviour {
    private int _weight;
    private int _torque;
    public int pivot;
    public RodBalance []weights;
    public Vector3 primaryLocation;
    public bool equipable;
    public RodBalance this[int i]{
        get{return weights[i];}
        set{
            if(value)
                weights[i] = value;
            else
                weights[i] = null;
        }
    }

    public int weight{
        get{
            _weight = 0;
            for (int i =0; i < weights.Length; ++i) {
                if(weights[i]){
                    if(weights[i].CompareTag("Weight"))
                        _weight += weights[i].GetComponent<Weight>().finalWeight;
                    else
                        _weight += weights[i].weight;
                }
            }
            return _weight;
        }
    }

    public int torque{
        get{
            _torque = 0;
            for (int i =0; i < weights.Length; ++i) {
                if(weights[i])
                    _torque += (pivot-i)*weights[i].weight;
            }
            return _torque;
        }
    }
    public void calculateRotation()
    {
        Debug.Log(gameObject.name);
        transform.localEulerAngles = new Vector3(0,0,-5*Mathf.Atan(torque));
    }
    void Start()
    {
        primaryLocation = transform.position;
    }
}
