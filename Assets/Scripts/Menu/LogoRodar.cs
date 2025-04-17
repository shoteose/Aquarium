using UnityEngine;

public class LogoRodar : MonoBehaviour
{
    [SerializeField] private Transform logo;
    [SerializeField] private float velocidadeRotacao = 2;
    void Update()
    {
        logo.transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);
    }
}
