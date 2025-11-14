using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class CharacterSwitchedEvent : UnityEvent<GameObject> {}

    [SerializeField] private GameObject[] characters;
    [SerializeField] private InputActionReference switchAction;
    [SerializeField] private bool sharePosition = true;

    public UnityEvent onSwitch;

    public CharacterSwitchedEvent onCharacterSwitched;

    private int currentIndex = 0;

    private void Start()
    {
        if (characters == null || characters.Length == 0)
        {
            enabled = false;
            return;
        }

        for (var i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null)
                characters[i].SetActive(i == currentIndex);
        }
    }

    private void OnEnable()
    {
        if (switchAction == null) return;
        switchAction.action.performed += OnSwitchPerformed;
        switchAction.action.Enable();
    }

    private void OnDisable()
    {
        if (switchAction == null) return;
        switchAction.action.performed -= OnSwitchPerformed;
        switchAction.action.Disable();
    }

    public void OnSwitchPerformed(InputAction.CallbackContext ctx)
    {
        SwitchCharacter();
    }

    private void SwitchCharacter()
    {
        if (characters is not { Length: > 1 })
            return;

        var nextIndex = (currentIndex + 1) % characters.Length;

        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        if (sharePosition && characters[currentIndex] != null)
        {
            var from = characters[currentIndex].transform;
            pos = from.position;
            rot = from.rotation;
        }

        if (characters[currentIndex] != null)
            characters[currentIndex].SetActive(false);

        var next = characters[nextIndex];
        if (next != null)
        {
            if (sharePosition)
                next.transform.SetPositionAndRotation(pos, rot);

            next.SetActive(true);
        }

        currentIndex = nextIndex;

        onSwitch?.Invoke();
        if (next != null)
            onCharacterSwitched?.Invoke(next);
    }
}
