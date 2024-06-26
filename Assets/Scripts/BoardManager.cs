using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Vector2 dimension = new(4, 3);
    [SerializeField] private float offset = 0.25f;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject p1Prefab;
    [SerializeField] private GameObject p2Prefab;

    [SerializeField] private UnityEvent<string> whenGamesEnd;

    private List<Slot> slots = new();
    private int[,] board;

    private bool gameIsOver = false;

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
                slots.Add(newSlot.GetComponent<Slot>());
            }
        }
    }

    public void Clear(){

        foreach(Slot slot in slots){
            board[slot.x, slot.y] = 0;
            slot.Clear();
        }
    }

    public void AddToken(Slot slot)
    {   
        if(gameIsOver || board[slot.x, slot.y] != 0) return;

        GameObject token = (currentPlayer == 1) ? p1Prefab : p2Prefab;
        board[slot.x, slot.y] = currentPlayer;
        token = Instantiate(token);
        slot.AddToken(token);
        if(CheckWinner(slot.x, slot.y)){
            whenGamesEnd?.Invoke($"Player {currentPlayer} wins!");
            gameIsOver = true;
        }
        currentPlayer = currentPlayer==1 ? 2 : 1;
    }

    private bool CheckWinner(int x, int y){
        if(CheckHorizontal(x, y)){
            return true;
        }

        if(CheckVertical(x, y)){
            return true;
        }

        if(CheckDiagonal(x, y)){
            return true;
        }
        return false;
    }

    private bool CheckHorizontal(int x, int y){

        if(x < dimension.x - 1 && x > 0){
            if(board[x+1, y] == currentPlayer && board[x-1, y]==currentPlayer){
                return true;
            }
        }
        if(x > 1){
            if(board[x-1, y] == currentPlayer && board[x-2, y]==currentPlayer){
                return true;
            }
        }
        if(x < dimension.x - 2){
            if(board[x+1, y] == currentPlayer && board[x+2, y]==currentPlayer){
                return true;
            }
        }
        return false;
    }

    private bool CheckVertical(int x, int y){

        if(y < dimension.y - 1 && y > 0){
            if(board[x, y+1] == currentPlayer && board[x, y+1]==currentPlayer){
                return true;
            }
        }
        if(y > 1){
            if(board[x, y-1] == currentPlayer && board[x, y-2]==currentPlayer){
                return true;
            }
        }
        if(y < dimension.y - 2){
            if(board[x, y+1] == currentPlayer && board[x, y+2]==currentPlayer){
                return true;
            }
        }
        return false;
    }
    
    private bool CheckDiagonal(int x, int y){

        if(x < dimension.x - 1 && x > 0 && y <dimension.y -1 && y > 0){
            if(board[x+1, y+1] == currentPlayer && board[x-1, y-1]==currentPlayer){
                return true;
            }
            if(board[x+1, y-1] == currentPlayer && board[x-1, y+1]==currentPlayer){
                return true;
            }
        }
        if(x > 1 & y > 1){
            if(board[x-1, y-1] == currentPlayer && board[x-2, y-2]==currentPlayer){
                return true;
            }
        }

        if(x > 1 & y < dimension.y - 2){
            if(board[x-1, y+1] == currentPlayer && board[x-2, y+2]==currentPlayer){
                return true;
            }
        }

        if(x < dimension.x - 2 && y < dimension.y - 2){
            if(board[x+1, y+1] == currentPlayer && board[x+2, y+2]==currentPlayer){
                return true;
            }
        }

        if(x < dimension.x - 2 && y > 1){
            if(board[x+1, y-1] == currentPlayer && board[x+2, y-2]==currentPlayer){
                return true;
            }
        }
        return false;
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
