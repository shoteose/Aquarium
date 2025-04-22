using UnityEngine;
using System.Collections;

public class Predador : MonoBehaviour
{
   
    [SerializeField] private Vector3 limitesNado = new Vector3(10f, 10f, 10f);
    [SerializeField] private float digestao;
    [SerializeField] private EstadoFome estadoFome;

    public float Velocidade { get; set; } = 8f;
    public string Nome { get; set; } = "Predador";

    private GameObject alvo;
    private Vector3 destinoAleatorio;
    private bool emDigestao = false;

    void Start()
    {
        estadoFome = EstadoFome.Fome;
        digestao = Random.Range(5,20);
    }

    void Update()
    {
        Nadar();
    }

    public void Nadar()
    {
        if (estadoFome == EstadoFome.Cheio)
        {
            if (!emDigestao)
            {
                EscolherNovoDestinoAleatorio();
                emDigestao = true;
                StartCoroutine(EsperarDigestao());
            }

            if (Vector3.Distance(transform.position, destinoAleatorio) < 0.5f)
            {
                EscolherNovoDestinoAleatorio();
            }

            
            transform.position = Vector3.MoveTowards(transform.position, destinoAleatorio, Velocidade * Time.deltaTime);

            Vector3 direcao = destinoAleatorio - transform.position;
            if (direcao != Vector3.zero)
            {
                Quaternion novaRotacao = Quaternion.LookRotation(direcao);
                transform.rotation = Quaternion.Lerp(transform.rotation, novaRotacao, Time.deltaTime * 2f);
            }

            return;
        }

        
        if (estadoFome == EstadoFome.Fome && alvo == null)
        {
            alvo = ProcurarPresa();
        }

        
        if (alvo != null && TemLinhaDeVisao(alvo))
        {
            Vector3 direcao = (alvo.transform.position - transform.position).normalized;
            transform.position += direcao * Velocidade * Time.deltaTime;

            if (direcao != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direcao), Time.deltaTime * 5f);
            }

            if (Vector3.Distance(transform.position, alvo.transform.position) < 1.5f)
            {
                Debug.Log("comi");
                Destroy(alvo);
                alvo = null;
                estadoFome = EstadoFome.Cheio;
            }
        }
    }

    private bool TemLinhaDeVisao(GameObject alvo)
    {
        Vector3 direcao = alvo.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direcao);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, direcao.magnitude))
        {
            return hit.collider.gameObject == alvo;
        }

        return false;
    }

    private void EscolherNovoDestinoAleatorio()
    {
        destinoAleatorio = transform.position + new Vector3(
            Random.Range(-limitesNado.x, limitesNado.x),
            Random.Range(-limitesNado.y, limitesNado.y),
            Random.Range(-limitesNado.z, limitesNado.z)
        );
        Debug.Log("Novo destino escolhido durante digestão.");
    }


    private GameObject ProcurarPresa()
    {
        PeixeBase[] todosOsPeixes = FindObjectsByType<PeixeBase>(FindObjectsSortMode.None);

        GameObject peixeMaisProximo = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (PeixeBase peixe in todosOsPeixes)
        {
            float distancia = Vector3.Distance(transform.position, peixe.transform.position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                peixeMaisProximo = peixe.gameObject;
            }
        }

        return peixeMaisProximo;
    }

    private IEnumerator EsperarDigestao()
    {
        yield return new WaitForSeconds(digestao);
        estadoFome = EstadoFome.Fome;
        emDigestao = false; 
        Debug.Log("Acabou a digestão e voltou a ter fome.");
    }
}
