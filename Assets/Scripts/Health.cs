using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider playerSlider3D;
    private Slider _playerSlider2D;

    private Stats _stats;

    // Start is called before the first frame update
    void Start()
    {
        _playerSlider2D = GetComponentInChildren<Slider>();
        _stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        _playerSlider2D.maxValue = _stats.maxHealth;
        playerSlider3D.maxValue = _stats.maxHealth;

        _stats.health = _stats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _playerSlider2D.value = _stats.health;
        playerSlider3D.value = _playerSlider2D.value;
    }
}