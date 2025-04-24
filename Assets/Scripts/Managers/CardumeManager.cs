using UnityEngine;

public class CardumeManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject prebabPeixe;
    [SerializeField] private GameObject prefabPeixeTurbo;

    [Header("Número de Peixes")]
    [SerializeField] private int numFish = 20;

    private GameObject[] todosPeixes;


    [Header("Limites")]
    public Vector3 limitesNado = new Vector3(10f, 10f, 10f);
    public Vector3 posicaoAlvo;

    [Header("Pesos do Flocking")]
    public float pesoCoesao = 1.0f;
    public float pesoSeparacao = 1.0f;
    public float pesoAlinhamento = 1.0f;


    [Header("Configuração do Cardume")]
    public bool ePredador = false;
    public float velocidadeMin = 2f;
    public float velocidadeMax = 5f;
    public float distanciaVizinho = 3f;
    public float velocidadeRotacao = 2f;

    [Header("Alvo do Cardume")]
    public GameObject alvoDoGrupo;

    public GameObject[] getTodosPeixes() { return todosPeixes; }

    void Start()
    {
        todosPeixes = new GameObject[numFish];

        GameObject tipoPeixe = prebabPeixe;
        if (Random.Range(0f, 1f) > 0.5f) tipoPeixe = prefabPeixeTurbo;

        if (tipoPeixe == prefabPeixeTurbo) Debug.Log("Turbo");

        for (int i = 0; i < numFish; ++i)
        {
            Vector3 pos = transform.position + new Vector3(
                Random.Range(-limitesNado.x, limitesNado.x),
                Random.Range(-limitesNado.y, limitesNado.y),
                Random.Range(-limitesNado.z, limitesNado.z));

            todosPeixes[i] = Instantiate(tipoPeixe, pos, Quaternion.identity, transform);
        }

        posicaoAlvo = transform.position;
    }

    void Update()
    {

        if (ePredador)
        {
            alvoDoGrupo = ObterPresaMaisProxima();
        }

        if (Random.Range(0, 100) < 10)
        {
            posicaoAlvo = transform.position + new Vector3(
                Random.Range(-limitesNado.x, limitesNado.x),
                Random.Range(-limitesNado.y, limitesNado.y),
                Random.Range(-limitesNado.z, limitesNado.z));
        }
    }

    public GameObject ObterPresaMaisProxima()
    {
        if (ePredador)
        {
            CardumeManager[] todosOsCardumes = FindObjectsByType<CardumeManager>(FindObjectsSortMode.None);
            GameObject peixeMaisProximo = null;
            float distanciaMinima = Mathf.Infinity;

            foreach (CardumeManager outroCardume in todosOsCardumes)
            {
                if (outroCardume != this)
                {
                    GameObject[] peixes = outroCardume.getTodosPeixes();

                    if (peixes == null) continue;

                    foreach (GameObject peixe in peixes)
                    {
                        if (peixe == null) continue;

                        float distancia = Vector3.Distance(transform.position, peixe.transform.position);
                        if (distancia < distanciaMinima)
                        {
                            distanciaMinima = distancia;
                            peixeMaisProximo = peixe;
                        }
                    }
                }
            }
            return peixeMaisProximo;
        }
        return null;
    }
}
