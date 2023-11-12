using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{

    //DISTANCIA DE LA CAMARA AL JUGADOR
    public Vector3 offset;
    //la camara siempre a punte al target
    private Transform target;
    //como de rapido tiene que pasar de una posicion a otra
    [Range(0, 1)] public float lerpValue;
    public float sensibilidad;
    
    
    // Start is called before the first frame update
    void Start()
    {
       //buscar 
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    //lo ultimo que se va hacer o posicionar la camara
    void LateUpdate()
    {
        //mover la posicion de un objeto  desde su vector hasta otro de una manera suavizada
        transform.position = Vector3.Lerp(transform.position,target.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up)* offset;
        transform.LookAt(target);
    }
}
