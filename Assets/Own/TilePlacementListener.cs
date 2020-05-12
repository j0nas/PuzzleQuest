using UnityEngine.XR.Interaction.Toolkit;

public class TilePlacementListener : XRSocketInteractor
{
    public JigsawPieceSpawner JigsawPieces;

    protected override void OnSelectEnter(XRBaseInteractable interactable)
    {
        JigsawPieces.OnTilePlace(interactable, transform.parent.parent.name);
        base.OnSelectEnter(interactable);
    }
}
