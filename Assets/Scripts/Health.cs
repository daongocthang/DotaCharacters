using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider playerSlider3D;
    [SerializeField] private int health;
    private Slider _playerSlider2D;

    // Start is called before the first frame update
    void Start()
    {
        _playerSlider2D = GetComponent<Slider>();

        _playerSlider2D.maxValue = health;
        playerSlider3D.maxValue = health;

    }

    // Update is called once per frame
    void Update()
    {
        _playerSlider2D.value = health;
        playerSlider3D.value = _playerSlider2D.value;
    }
}