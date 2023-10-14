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
    private Rigidbody _rigidbody;

    private Vector3 _screenPoint;
    private Vector3 _offset;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        _screenPoint = _gameManager.GameCamera.WorldToScreenPoint(transform.position);
        _offset = transform.position - _gameManager.GameCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

    }

    private void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        Vector3 currentPosition = _gameManager.GameCamera.ScreenToWorldPoint(currentScreenPoint) + _offset;
        currentPosition.y = 8f;
        transform.position = currentPosition;
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
