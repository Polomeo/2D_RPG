using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoundCameraPos : CinemachineExtension
{
    public float PixelsPerUnit = 32;

    // Llamado por Cinemachine cuando el Confiner termin� su procesamiento
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        // Chequeamos en qu� Stage del post-procesamiento estamos
        // (estando en el Stage de Body podemos posicionar la Cam)
        if (stage == CinemachineCore.Stage.Body)
        {
            // Chequeamos la �ltima posici�n de la VC (Virtual Camera)
            Vector3 finalPos = state.FinalPosition;
            
            // Definimos una nueva posici�n redondeada (con el m�todo Round que definimos m�s abajo)
            Vector3 newPos = new Vector3(Round(finalPos.x), Round(finalPos.y), finalPos.z);

            // Seteamos la posici�n de la VC a la diferencia entre la vieja pos
            // y la nueva redondeada que acabamos de calcular
            state.PositionCorrection += newPos - finalPos;
        }
    }

    float Round(float x)
    {
        // Aplicamos el factor de PixelsPerUnit y luego lo sacamos del resultado final dividiendo.
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
