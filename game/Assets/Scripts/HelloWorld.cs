using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    [SerializeField]
    private string worldName;
    
    void Start()
    {
        Debug.Log($"Hello, {worldName}!");
    }
}