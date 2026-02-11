using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image _frontBarImage;
    [SerializeField] private Image _backBarImage;
    [SerializeField] private float _smoothSpeed = 0.5f; // 赤バーの追従速度（秒）

    private float _currentHpRatio = 1f;

    private void Start()
    {
        _frontBarImage.fillAmount = 1f;
        _backBarImage.fillAmount = 1f;
    }

    public void SetHp(float currentHP, float maxHP)
    {
        _currentHpRatio = Mathf.Clamp01(currentHP / maxHP);
        _frontBarImage.fillAmount = _currentHpRatio;
    }

    private void Update()
    {
        if (_backBarImage.fillAmount > _currentHpRatio)
        {
            _backBarImage.fillAmount = Mathf.MoveTowards(
                _backBarImage.fillAmount,
                _currentHpRatio,
                _smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            // 即時追従（回復時など）
            _backBarImage.fillAmount = _currentHpRatio;
        }
    }
}
