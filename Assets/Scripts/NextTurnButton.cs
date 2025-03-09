using UnityEngine;

public class NextTurnButton : MonoBehaviour
{
    [SerializeField] private Controller controller;

    public enum SpriteSourceType
    {
        Sprite,
        Aseprite
    }
    
    [System.Serializable]
    public class SpriteSource
    {
        public SpriteSourceType type;
        public Sprite sprite;
        public string asepritePath;
    }

    [SerializeField] private SpriteSource defaultSpriteSource;
    [SerializeField] private SpriteSource pressedSpriteSource;

    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;
    private Sprite _pressedSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("NextTurnButton: SpriteRenderer component not found!");
        }
        LoadSprites();
        if (_defaultSprite == null)
        {
            Debug.LogError("NextTurnButton: defaultSprite not loaded!");
        }
        if (_pressedSprite == null)
        {
            Debug.LogError("NextTurnButton: pressedSprite not loaded!");
        }
        _spriteRenderer.sprite = _defaultSprite;
    }

    private void LoadSprites()
    {
        Debug.Log($"NextTurnButton.LoadSprites: Loading sprites");
        _defaultSprite = LoadSprite(defaultSpriteSource);
        _pressedSprite = LoadSprite(pressedSpriteSource);
    }

    private Sprite LoadSprite(SpriteSource spriteSource)
    {
        if (spriteSource == null)
        {
            Debug.LogError("NextTurnButton: Sprite Source not assigned!");
            return null;
        }

        switch (spriteSource.type)
        {
            case SpriteSourceType.Sprite:
                Debug.Log($"NextTurnButton.LoadSprite: Loading Sprite");
                if (spriteSource.sprite == null)
                {
                    Debug.LogError("NextTurnButton: Sprite is null");
                    return null;
                }
                return spriteSource.sprite;

            case SpriteSourceType.Aseprite:
                Debug.Log($"NextTurnButton.LoadSprite: Loading Aseprite");
                if (string.IsNullOrEmpty(spriteSource.asepritePath))
                {
                    Debug.LogError("NextTurnButton: Aseprite path is null or empty");
                    return null;
                }
                Sprite asepriteSprite = Resources.Load<Sprite>(spriteSource.asepritePath);
                if (asepriteSprite == null)
                {
                    Debug.LogError($"NextTurnButton: Failed to load Aseprite sprite at path {spriteSource.asepritePath}");
                    return null;
                }
                return asepriteSprite;

            default:
                Debug.LogError("NextTurnButton: Invalid SpriteSourceType");
                return null;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log($"NextTurnButton.OnMouseDown: Button was clicked.");
        if (controller != null)
        {
            controller.NextTurnButtonClicked();
        }
        else
        {
            Debug.LogError("NextTurnButton.OnMouseDown: Controller not assigned!");
        }
        if (_spriteRenderer != null && _pressedSprite != null)
        {
            _spriteRenderer.sprite = _pressedSprite;
        }
        Debug.Log($"NextTurnButton.OnMouseDown: Finished.");
    }

    private void OnMouseUp()
    {
        if (_spriteRenderer != null && _defaultSprite != null)
        {
            _spriteRenderer.sprite = _defaultSprite;
        }
    }
}