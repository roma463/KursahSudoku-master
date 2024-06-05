using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class Miltiplayer : MonoBehaviourPunCallbacks
{
    public static Miltiplayer Instance { get; private set; }
    private PhotonView _photonView;
    private CreateGridServer _createGrid;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _createGrid = CreateGridServer.Instance;
        if (_photonView.IsMine)
            Instance = this;
    }

    public void InitClient()
    {
        _createGrid = CreateGridServer.Instance;
        int[] flatMap = ArrayConverter.To1DArray(_createGrid.Map);
        bool[] flatHideCells = ArrayConverter.To1DBoolArray(_createGrid.CellActive);
        _photonView.RPC(nameof(DAVAIMap), RpcTarget.Others, flatMap, flatHideCells, _createGrid.Map.GetLength(0), _createGrid.Map.GetLength(1));
    }

    public PhotonView GetPhotonView() => _photonView;

    [PunRPC]
    public void DAVAIMap(int[] flatMap, bool[] flatHideCells, int rows, int cols)
    {
        //_createGrid = CreateGridServer.Instance;
        print("DAVAI");
        int[,] map = ArrayConverter.To2DArray(flatMap, rows, cols);
        bool[,] ActiveCell = ArrayConverter.To2DBoolArray(flatHideCells, rows, cols);
        _createGrid.SetGrid(map, ActiveCell);
        //_createGrid.SetGridPlayer();
    }
}