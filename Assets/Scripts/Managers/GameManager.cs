using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefabCardumeManager;
    public GameObject prefabPredador;

    public int numeroCardumes = 3;
    public int numeroPredadores = 2;
    public List<CardumeManager> cardumes = new List<CardumeManager>();

    public void Start()
    {
        CriarCardumes();
        CriarPredadores();
    }

    public void CriarCardumes()
    {
        for (int i = 0; i < numeroCardumes; i++)
        {
            Vector3 posicao = new Vector3(Random.Range(-20f, 20f), Random.Range(-5f, 5f), Random.Range(-20f, 20f));
            GameObject novoCardume = Instantiate(prefabCardumeManager, posicao, Quaternion.identity);

            CardumeManager cardume = novoCardume.GetComponent<CardumeManager>();
            if (cardume != null)
            {
                cardume.ePredador = (i == 0);
                cardumes.Add(cardume);
            }
        }
    }

    public void CriarPredadores()
    {
        for (int i = 0; i < numeroPredadores; i++)
        {
            Instantiate(prefabPredador, new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f)), Quaternion.identity);
        }
    }

    public void addPredador()
    {
        Instantiate(prefabPredador, new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f)), Quaternion.identity);

    }

    public void addCardume()
    {
        Vector3 posicao = new Vector3(Random.Range(-20f, 20f), Random.Range(-5f, 5f), Random.Range(-20f, 20f));
        GameObject novoCardume = Instantiate(prefabCardumeManager, posicao, Quaternion.identity);

        CardumeManager cardume = novoCardume.GetComponent<CardumeManager>();
        if (cardume != null)
        {
            cardume.ePredador = false;
            cardumes.Add(cardume);
        }
    }

    public void addCardumePredador()
    {
        Vector3 posicao = new Vector3(Random.Range(-20f, 20f), Random.Range(-5f, 5f), Random.Range(-20f, 20f));
        GameObject novoCardume = Instantiate(prefabCardumeManager, posicao, Quaternion.identity);

        CardumeManager cardume = novoCardume.GetComponent<CardumeManager>();
        if (cardume != null)
        {
            cardume.ePredador = true;
            cardumes.Add(cardume);
        }
    }
}
