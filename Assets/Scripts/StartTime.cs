using UnityEngine;
using UnityEngine.Events;

public class StartTime : MonoBehaviour
{
    public UnityEvent OnActivate;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        OnActivate?.Invoke();

    }
    public void ST()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
