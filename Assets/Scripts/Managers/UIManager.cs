using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider sliderCoesao;
    public Slider sliderSeparacao;
    public Slider sliderAlinhamento;

    [Header("Botoes")]
    public Button botaoAddPredador;
    public Button botaoAddCardume;
    public Button botaoAddCardumePredador;

    [Header("Managers")]
    public GameManager gameManager;

    void Start()
    {
    
        sliderCoesao.onValueChanged.AddListener(OnSliderCoesaoChanged);
        sliderSeparacao.onValueChanged.AddListener(OnSliderSeparacaoChanged);
        sliderAlinhamento.onValueChanged.AddListener(OnSliderAlinhamentoChanged);

        botaoAddPredador.onClick.AddListener(gameManager.addPredador);
        botaoAddCardume.onClick.AddListener(gameManager.addCardume);
        botaoAddCardumePredador.onClick.AddListener(gameManager.addCardumePredador);

    }

    void OnSliderCoesaoChanged(float valor)
    {
        foreach (var cardume in gameManager.cardumes)
        {
            cardume.pesoCoesao = valor;
        }
    }

    void OnSliderSeparacaoChanged(float valor)
    {
        foreach (var cardume in gameManager.cardumes)
        {
            cardume.pesoSeparacao = valor;
        }
    }

    void OnSliderAlinhamentoChanged(float valor)
    {
        foreach (var cardume in gameManager.cardumes)
        {
            cardume.pesoAlinhamento = valor;
        }
    }
}
