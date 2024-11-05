using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaXbullet : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Text _bullet;

    // Start is called before the first frame update
    void Start()
    {
        UpdateBulletUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBulletUI();
    }
    void UpdateBulletUI()
    {
        if (_player != null)
        {
            _bullet.text = "Ammo:   " + _player.GetCurrentAmmo().ToString();
        }
    }
}
