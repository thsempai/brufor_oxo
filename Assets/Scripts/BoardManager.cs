using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Vector2 dimension = new(4, 3);
    [SerializeField] private float offset = 0.25f;
    [SerializeField] private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 prefabSize = prefab.GetComponent<Renderer>().bounds.size;

        Vector3 decal = new(
            (prefabSize.x  + offset ) * (dimension.x - 1) * 0.5f, 
            0, 
            (prefabSize.z  + offset ) * (dimension.y - 1) * 0.5f);

        Vector3 origin = transform.position - decal;

        for(float x=0f; x < dimension.x; x++){
            for(float y=0f; y < dimension.y; y++){
                Vector3 position = new(
                    x * (prefabSize.x + offset), transform.position.y, y * (prefabSize.z + offset));
                position = position + origin;
                Instantiate(prefab, position, prefab.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmosSelected()
    {
        Vector3 prefabSize = prefab.GetComponent<Renderer>().bounds.size;

        Vector3 decal = new(
            (prefabSize.x  + offset ) * (dimension.x - 1) * 0.5f, 
            0, 
            (prefabSize.z  + offset ) * (dimension.y - 1) * 0.5f);

        Vector3 origin = transform.position - decal;

        for(float x=0f; x < dimension.x; x++){
            for(float y=0f; y < dimension.y; y++){
                Vector3 position = new(
                    x * (prefabSize.x + offset), transform.position.y, y * (prefabSize.z + offset));
                position = position + origin;
                Gizmos.DrawCube(position, prefabSize);
                Gizmos.DrawWireCube(position, prefabSize);
            }
        }
 
    }
}
