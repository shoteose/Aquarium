using UnityEngine;

public class PeixeTurbo : PeixeBase
{
    protected override void Start()
    {
        base.Start(); 
        Velocidade *= 2f;
        Nome = "Peixe Turbo";
    }

}
