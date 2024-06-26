using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private int _x;
    private int _y;

    private GameObject token;

    public int x{
        get{return _x;}
    }

    public int y{
        get{return _y;}
    }
    private BoardManager manager;

    public void AssignManager(BoardManager manager, int x, int y){
        this.manager = manager;
        this._x = x;
        this._y = y;
    }

    public void OnMouseUp(){
        manager.AddToken(this);
    }

    public void AddToken(GameObject token){
        Vector3 tokenSize = token.GetComponent<Renderer>().bounds.size;
        token.transform.position = transform.position + Vector3.up * tokenSize.y * 0.5f;
        this.token = token; 
    }

    public void Clear(){
        if(token) Destroy(token);
    }
}
