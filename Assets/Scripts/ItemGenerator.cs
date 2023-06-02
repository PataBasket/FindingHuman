using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] _items;
    private int itemType;
    // Start is called before the first frame update
    void Start()
    {
        itemType = Random.Range(0, 3);
        Instantiate(_items[itemType], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
