using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _pool;
    [SerializeField] private Transform _matchPool;

    [SerializeField] private GameObject[] _itemPrefabs;

    [SerializeField] private int _coupleCount;

    [SerializeField] private int _matchCount;

    [SerializeField] private Camera _gameCam;

    private SelectObj _selectObj;
    private Vector3 _screenPoint;
    private Vector3 _offset;

    public event Action<SelectObj> OnItemEntersBox;
    public event Action<SelectObj> OnItemLeavesBox;

    [SerializeField] private List<SelectObj> _itemsInsideBox;



    private void Start()
    {
        if(_gameCam == null)
            _gameCam = Camera.main;

        FillPool();

        _itemsInsideBox = new List<SelectObj>();

        OnItemEntersBox += ItemEntersBox;
        OnItemLeavesBox += ItemLeavesBox;
    }


    private void OnDestroy()
    {
        OnItemEntersBox -= ItemEntersBox;
        OnItemLeavesBox -= ItemLeavesBox;
    }

    private void FillPool()
    {
        for(int i = 0; i<_coupleCount; i++)
        {
            GameObject go1 = Instantiate(_itemPrefabs[i % _itemPrefabs.Length], RandomPositionOverPool(),Quaternion.identity);
            go1.transform.SetParent(transform);
            go1.GetComponent<SelectObj>().Init(this);

            GameObject go2 = Instantiate(_itemPrefabs[i % _itemPrefabs.Length], RandomPositionOverPool(), Quaternion.identity);
            go2.transform.SetParent(transform);
            go2.GetComponent<SelectObj>().Init(this);
        }
    }

    private Vector3 RandomPositionOverPool()
    {
        Vector3 position = _pool.position;
        position += new Vector3(UnityEngine.Random.Range(-8f, 10f), UnityEngine.Random.Range(4f, 7f), UnityEngine.Random.Range(-8f, 10f));

        return position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray mouseRay = _gameCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(mouseRay, out hitInfo))
            {
                SelectObj item = hitInfo.collider.GetComponent<SelectObj>();

                if (item != null)
                {
                    _selectObj = item;
                    _screenPoint = _gameCam.WorldToScreenPoint(_selectObj.transform.position);
                    _offset = _selectObj.transform.position - _gameCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
            _selectObj = null;

        if (Input.GetMouseButton(0) && _selectObj != null)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 currentPosition = _gameCam.ScreenToWorldPoint(currentScreenPoint) + _offset;
            currentPosition.y = 8f;
            _selectObj.transform.position = currentPosition;
        }
    }


    private void ItemLeavesBox(SelectObj item)
    {
        if (_itemsInsideBox.Contains(item))
            _itemsInsideBox.Remove(item);
        if (_itemsInsideBox.Count == 2)
            CheckIfMatch();
    }

    private void ItemEntersBox(SelectObj item)
    {
        _itemsInsideBox.Add(item);
        if (_itemsInsideBox.Count == 2)
            CheckIfMatch();
    }

    private void CheckIfMatch()
    {
        if (_itemsInsideBox[0].Type == _itemsInsideBox[1].Type)
        {
            foreach (SelectObj item in _itemsInsideBox)
                Destroy(item.gameObject);

            _itemsInsideBox.Clear();

            _matchCount += 1;
        }
        
    }

    public void PlaceItemInsideBox(SelectObj item)
    {
        OnItemEntersBox.Invoke(item);
    }
    public void RemoveItemFromBox(SelectObj item)
    {
        OnItemLeavesBox.Invoke(item);
    }
}

