using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider enemySlider3D;
    private Stats _stats;

    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<Stats>();

        enemySlider3D.maxValue = _stats.maxHealth;
        _stats.health = _stats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        enemySlider3D.value = _stats.health;
    }
}