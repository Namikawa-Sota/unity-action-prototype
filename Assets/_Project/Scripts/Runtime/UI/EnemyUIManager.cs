using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private GameObject _enemyUIPrefab;

    [SerializeField] private List<EnemyTransformData> _enemies = new();

    [SerializeField] private GameObject _testObj;

    public void RegisterEnemy(Transform EnemyTransform)
    {
        var uiObject = Instantiate(_enemyUIPrefab, _canvasRect);
        _enemies.Add(new EnemyTransformData(EnemyTransform, uiObject.GetComponent<RectTransform>()));
    }

    private void Start()
    {
        RegisterEnemy(_testObj.transform);
    }

    private void Update()
    {
        foreach (var data in _enemies)
        {
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(data.EnemyTransform.position + Vector3.up * 2f);
            bool isVisible = screenPos.z > 0;

            if (isVisible)
            {
                data.UiRectTransform.position = screenPos;
                UpdateHp(data.EnemyTransform.gameObject, data.UiRectTransform.gameObject);
            }
        }
    }

    private void UpdateHp(GameObject enemy, GameObject uiObject)
    {
        CharacterStatus status = enemy.GetComponent<CharacterStatus>();
        HpBar hpBar = uiObject.GetComponent<HpBar>();
        hpBar.SetHp(status.Hp, status.MaxHp);
    }
}

public struct EnemyTransformData
{
    public Transform EnemyTransform;
    public RectTransform UiRectTransform;

    public EnemyTransformData(Transform enemyTransform, RectTransform uiRectTransform)
    {
        this.EnemyTransform = enemyTransform;
        this.UiRectTransform = uiRectTransform;
    }
}