using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Vector2 dimension = new(4, 3);
    [SerializeField] private float offset = 0.25f;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject p1Prefab;
    [SerializeField] private GameObject p2Prefab;

    private List<GameObject> slots = new();
    private int[,] board;

    private int currentPlayer = 1;

    // Start is called before the first frame update
    private void Start()
    {
        board = new int[(int)dimension.x, (int)dimension.y];

        Vector3 prefabSize = slotPrefab.GetComponent<Renderer>().bounds.size;

        Vector3 decal = new(
            (prefabSize.x  + offset ) * (dimension.x - 1) * 0.5f, 
            0, 
            (prefabSize.z  + offset ) * (dimension.y - 1) * 0.5f);

        Vector3 origin = transform.position - decal;

        for(int x = 0; x < dimension.x; x++){
            for(int y = 0; y < dimension.y; y++){
                Vector3 position = new(
                    x * (prefabSize.x + offset), transform.position.y, y * (prefabSize.z + offset));
                position = position + origin;
                GameObject newSlot = Instantiate(slotPrefab, position, slotPrefab.transform.rotation);
                board[x, y] = 0;
                newSlot.GetComponent<Slot>().AssignManager(this, x, y);
                slots.Add(newSlot);
            }
        }
    }

    public void AddToken(Slot slot)
    {   
        if(board[slot.x, slot.y] != 0) return;

        GameObject token = (currentPlayer == 1) ? p1Prefab : p2Prefab;
        board[slot.x, slot.y] = currentPlayer;
        currentPlayer = currentPlayer==1 ? 2 : 1;
        token = Instantiate(token);
        slot.AddToken(token);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 prefabSize = slotPrefab.GetComponent<Renderer>().bounds.size;

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
