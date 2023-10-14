using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Banana,
    Burger,
    Broccoli,
    Carrot,
    Cabbage

}
public class SelectObj : MonoBehaviour
{
    public ItemType Type;

    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MatchBox")
            _gameManager.PlaceItemInsideBox(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MatchBox")
            _gameManager.RemoveItemFromBox(this);
    }
}
