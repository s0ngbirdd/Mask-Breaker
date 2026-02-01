using System.Collections;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance;
    
    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private Texture2D _leftSlapCursor;
    [SerializeField] private Texture2D _rightSlapCursor;
    [SerializeField] private Texture2D _damagedCursor;
    [SerializeField] private Texture2D _forwardCursor;
    [SerializeField] private float _defaultCursorWaitTime = 0.5f;
    [SerializeField] private Vector2 _lastCursorPosition;

    private Coroutine _setDefaultCursorCoroutine;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        SetDefaultCursor();

        StartCoroutine(CheckCursorDirection());
    }

    public void SetDefaultCursor()
    {
        Vector2 hotspot = new Vector2(64, 64);
        Cursor.SetCursor(_defaultCursor, hotspot, CursorMode.Auto);
    }

    public void SetSlapCursor()
    {
        Vector2 hotspot = new Vector2(64, 64);
        
        /*if (Input.mousePosition.x - _lastCursorPosition.x > 0)
            Cursor.SetCursor(_rightSlapCursor, hotspot, CursorMode.Auto);
        else
            Cursor.SetCursor(_leftSlapCursor, hotspot, CursorMode.Auto);*/
        
        if (Input.mousePosition.x - _lastCursorPosition.x > 5)
            Cursor.SetCursor(_rightSlapCursor, hotspot, CursorMode.Auto);
        else if (Input.mousePosition.x - _lastCursorPosition.x < -5)
            Cursor.SetCursor(_leftSlapCursor, hotspot, CursorMode.Auto);
        else
            Cursor.SetCursor(_forwardCursor, hotspot, CursorMode.Auto);
        
        if (_setDefaultCursorCoroutine != null)
            StopCoroutine(_setDefaultCursorCoroutine);
        
        _setDefaultCursorCoroutine = StartCoroutine(SetDefaultCursorCoroutine());
    }

    public void SetDamagedCursor()
    {
        Vector2 hotspot = new Vector2(64, 64);
        
        Cursor.SetCursor(_damagedCursor, hotspot, CursorMode.Auto);
        
        if (_setDefaultCursorCoroutine != null)
            StopCoroutine(_setDefaultCursorCoroutine);
        
        _setDefaultCursorCoroutine = StartCoroutine(SetDefaultCursorCoroutine());
    }

    private IEnumerator SetDefaultCursorCoroutine()
    {
        yield return new WaitForSeconds(_defaultCursorWaitTime);
        
        SetDefaultCursor();
    }

    private IEnumerator CheckCursorDirection()
    {
        while (true)
        {
            _lastCursorPosition = Input.mousePosition;
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
