using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InteractionButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public bool IsCliked { get; private set; }

    private void Awake()
    {
        _button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnInteractionButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnInteractionButtonClick);
    }

    private void OnInteractionButtonClick()
    {
        IsCliked = true;
        StartCoroutine(DisableInteractionButtonClick());
    }

    private IEnumerator DisableInteractionButtonClick()
    {
        yield return new WaitForSeconds(0.2f);

        IsCliked = false;
        StopCoroutine(DisableInteractionButtonClick());
    }

}
