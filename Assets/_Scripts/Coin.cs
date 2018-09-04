using UnityEngine;

public class Coin : MonoBehaviour {

    private Vector3 finalPos;
    private Transform _scale;
	public Transform scale{
        get {return _scale;}
        set {
            _scale = value;
            float random = Random.value * 0.1f;
            Vector2 randomPos = Random.insideUnitCircle * random * 3;
            finalPos = _scale.position - new Vector3(0, 1.3f - random, 0) + new Vector3(randomPos.x, 0, randomPos.y);
        }
    }

    void Awake()
    {
        finalPos = transform.position;
    }

    void Update(){
        if((finalPos-transform.position).sqrMagnitude >= 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position,finalPos,0.1f);
        }else
        {
            transform.position = finalPos;
        }
    }
}
