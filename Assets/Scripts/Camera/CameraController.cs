using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float velocidade = 10f;

    public float sensibilidade = 3f;

    public float sensibilidadeZoom = 10f;

    private bool bloqueado = false;

    void Update()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + (-transform.right * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + (transform.right * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + (transform.forward * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + (-transform.forward * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = transform.position + (transform.up * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.position = transform.position + (-transform.up * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            transform.position = transform.position + (Vector3.up * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        {
            transform.position = transform.position + (-Vector3.up * velocidade * Time.deltaTime);
        }

        if (bloqueado)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibilidade;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensibilidade;
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            transform.position = transform.position + transform.forward * axis * sensibilidadeZoom;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Desbloquear();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Bloquear();
        }
    }

    void OnDisable()
    {
        Bloquear();
    }

    public void Desbloquear()
    {
        bloqueado = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Bloquear()
    {
        bloqueado = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

