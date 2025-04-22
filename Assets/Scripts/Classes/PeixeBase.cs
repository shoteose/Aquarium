using UnityEngine;

public class PeixeBase : MonoBehaviour
{
    public float Velocidade { get; set; }
    public string Nome { get; set; } = "Peixe Base";

    private CardumeManager _myManager;
    private bool retornando = false;
    private GameObject alvo;

    protected virtual void Start()
    {
        _myManager = GetComponentInParent<CardumeManager>();
        Velocidade = Random.Range(_myManager.velocidadeMin, _myManager.velocidadeMax);
    }

    protected virtual void Update()
    {
        Nadar();
    }

    public void Nadar()
    {
        Bounds b = new Bounds(_myManager.transform.position, _myManager.limitesNado * 2f);
        retornando = !b.Contains(transform.position);

        if (retornando)
        {
            Vector3 direction = _myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                _myManager.velocidadeRotacao * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                Velocidade = Random.Range(_myManager.velocidadeMin, _myManager.velocidadeMax);

            if (_myManager.ePredador)
            {
                ComportamentoPredador();
            }
            else
            {
                if (Random.Range(0, 100) < 10)
                    ComportamentoFlocking();
            }
        }

        transform.Translate(0.0f, 0.0f, Velocidade * Time.deltaTime);
    }

    private void ComportamentoPredador()
    {
        if (alvo == null || !alvo.activeInHierarchy)
        {
            alvo = _myManager.ObterPresaMaisProxima();
        }

        if (alvo != null)
        {
            Vector3 direcao = (alvo.transform.position - transform.position).normalized;
            transform.position += direcao * Velocidade * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direcao),
                _myManager.velocidadeRotacao * Time.deltaTime);

            if (Vector3.Distance(transform.position, alvo.transform.position) < 1.0f)
            {
                Destroy(alvo);
                alvo = null;
            }
        }
    }

    private void ComportamentoFlocking()
    {
        GameObject[] vizinhos = _myManager.getTodosPeixes();

        Vector3 centro = CalcularCoesao(vizinhos) * _myManager.pesoCoesao;
        Vector3 evitar = CalcularSeparacao(vizinhos) * _myManager.pesoSeparacao;
        float novaVelocidade = CalcularAlinhamento(vizinhos) * _myManager.pesoAlinhamento;

        Vector3 direcaoFinal = (centro + evitar) - transform.position;

        Velocidade = Mathf.Min(novaVelocidade, _myManager.velocidadeMax);

        if (direcaoFinal != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direcaoFinal),
                _myManager.velocidadeRotacao * Time.deltaTime);
        }
    }

    private Vector3 CalcularCoesao(GameObject[] vizinhos)
    {
        Vector3 centro = Vector3.zero;
        int tamanhoDoGrupo = 0;

        foreach (GameObject peixe in vizinhos)
        {
            if (peixe != this.gameObject && peixe != null)
            {
                float distancia = Vector3.Distance(peixe.transform.position, transform.position);
                if (distancia <= _myManager.distanciaVizinho)
                {
                    centro += peixe.transform.position;
                    tamanhoDoGrupo++;
                }
            }
        }

        if (tamanhoDoGrupo > 0)
        {
            centro = centro / tamanhoDoGrupo + (_myManager.posicaoAlvo - transform.position);
        }

        return centro;
    }

    private Vector3 CalcularSeparacao(GameObject[] vizinhos)
    {
        Vector3 evitar = Vector3.zero;

        foreach (GameObject go in vizinhos)
        {
            if (go != this.gameObject && go != null)
            {
                float distancia = Vector3.Distance(go.transform.position, transform.position);
                if (distancia < 1.0f)
                {
                    evitar += transform.position - go.transform.position;
                }
            }
        }

        return evitar;
    }

    private float CalcularAlinhamento(GameObject[] vizinhos)
    {
        float velocidadeTotal = 0.01f;
        int tamanhoDoGrupo = 0;

        foreach (GameObject go in vizinhos)
        {
            if (go != this.gameObject && go != null)
            {
                float distancia = Vector3.Distance(go.transform.position, transform.position);
                if (distancia <= _myManager.distanciaVizinho)
                {
                    PeixeBase outroPeixe = go.GetComponent<PeixeBase>();
                    velocidadeTotal += outroPeixe.Velocidade;
                    tamanhoDoGrupo++;
                }
            }
        }

        if (tamanhoDoGrupo > 0)
        {
            return velocidadeTotal / tamanhoDoGrupo;
        }

        return Velocidade;
    }


}
