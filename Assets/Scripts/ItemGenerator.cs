using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] _items;
    private int itemType;
    private int randomGenerator;
    private GameObject _item;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        randomGenerator = Random.Range(0, 1);

        if(randomGenerator == 0)
        {
            itemType = Random.Range(0, 3);
            _item = Instantiate(_items[itemType], transform.position, Quaternion.identity);

            originalPosition = _item.transform.position;

            _item.transform.DOMoveY(originalPosition.y + 0.5f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetLink(_item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
